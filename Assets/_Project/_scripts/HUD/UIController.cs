using Assets._Project._scripts._core;
using Assets._Project._scripts._core.Events;
using System;
using UnityEngine;

namespace Assets._Project._scripts.HUD
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Canvas _menuUI;
        [SerializeField] private Canvas _healthUI;
        [SerializeField] private Canvas _inventoryUI;
        [SerializeField] private Canvas _hubUI;

        public void Initialize(Canvas healthUI)
        {
            _healthUI = healthUI;
        }

        public enum UIMode
        {
            MenuMode,
            GameplayMode,
            HubMode,
        }

        public void SwitchTo(UIMode mode)
        {
            _menuUI.gameObject.SetActive(false);
            _healthUI.gameObject.SetActive(false);
            _inventoryUI.gameObject.SetActive(false);
            _hubUI?.gameObject.SetActive(false);

            switch (mode)
            {
                case UIMode.MenuMode: 
                    _menuUI.gameObject.SetActive(true); 
                    break;
                case UIMode.GameplayMode: 
                    _healthUI.gameObject.SetActive(true);
                    _inventoryUI.gameObject.SetActive(true);
                    break;
                case UIMode.HubMode:
                    _hubUI?.gameObject.SetActive(true);
                    break;
            }
        }

        public void OnStartButtonClicked()
        {
            EventBus.Instance?.Publish(new RunStartedEvent());
        }

        public void OnGoToLevelButtonClick()
        {
            EventBus.Instance?.Publish(new GoToLevelEvent { Level = 1 });
        }

        public void FadeIn(Action onComplete = null)
        {
            Debug.Log("Screen fade in");
            UIScreenFader.Instance.FadeIn(onComplete);
            UIScreenFader.Instance.gameObject.SetActive(false);
        }

        public void FadeOut(Action onComplete = null)
        {
            Debug.Log("Screen fade out");
            UIScreenFader.Instance.gameObject.SetActive(true);
            UIScreenFader.Instance.FadeOut(onComplete);
        }

        public void ShowHub() => SwitchTo(UIMode.HubMode);
        public void ShowMenu() => SwitchTo(UIMode.MenuMode);
        public void ShowGameplay() => SwitchTo(UIMode.GameplayMode);
    }
}
