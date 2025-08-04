using System;

namespace Assets._Project._scripts._core.EventBus
{
    public class EventUnsubscriber<T> : IDisposable where T : struct
    {
        private readonly EventBus _eventBus;
        private readonly Action<T> _listener;
        private bool _isUnsubscribed = false;

        public EventUnsubscriber(EventBus eventBus, Action<T> listener)
        {
            _eventBus = eventBus;
            _listener = listener;
        }

        public void Dispose()
        {
            if (!_isUnsubscribed && _eventBus != null)
            {
                _eventBus.Unsubscribe(_listener);
                _isUnsubscribed = true;
            }
        }
    }
}