using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project._scripts._core.EventBus
{
    public class EventBus: MonoBehaviour
    {
        private Dictionary<Type, Delegate> eventListeners = new Dictionary<Type, Delegate>();

        public static EventBus Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public IDisposable Subscribe<T>(Action<T> listener) where T: struct
        {
            Type eventType = typeof(T);

            if (!eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType] = null;
            }

            Action<T> existingDelegate = eventListeners[eventType] as Action<T>;
            
            if (existingDelegate != null && existingDelegate.GetInvocationList().Contains(listener))
            {
                Debug.Log($"[EventBus] Попытка двойной подписки на {typeof(T)}. Подписчик проигнорирован");
                return null;
            }

            eventListeners[eventType] = existingDelegate + listener;

            return new EventUnsubscriber<T>(this, listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : struct 
        {
            Type eventType = typeof(T);
            
            if (eventListeners.TryGetValue(eventType, out Delegate? existing))
            {
                Action<T> existingAction = (Action<T>)existing;
                Action<T> newAction = existingAction - listener;

                if (newAction == null)
                {
                    eventListeners.Remove(eventType);
                }
                else
                {
                    eventListeners[eventType] = newAction;
                }
            }
        }

        public void Publish<T>(T @event) where T : struct
        {
            Type eventType = typeof(T);

            if (eventListeners.TryGetValue(eventType, out Delegate? delegates))
            {
                Delegate[] invokationList = delegates.GetInvocationList();

                foreach (var action in invokationList)
                {
                    try
                    {
                        ((Action<T>)action)?.Invoke(@event);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Ошибка при  вызове обработчика событий {typeof(T)} : {e.Message}\n{e.StackTrace}");
                    }
                }
            }
        }
    }
}
