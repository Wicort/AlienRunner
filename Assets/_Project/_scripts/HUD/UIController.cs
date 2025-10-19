using Assets._Project._scripts._core;
using Assets._Project._scripts._core.Events;
using UnityEngine;

namespace Assets._Project._scripts.HUD
{
    public class UIController : MonoBehaviour  // Singleton<UIController>
    {
        [SerializeField] private Canvas _menuUI;
        [SerializeField] private Canvas _healthUI;
        [SerializeField] private Canvas _inventoryUI;

        public void Initialize(Canvas healthUI)
        {
            _healthUI = healthUI;
        }

        public enum UIMode
        {
            MenuMode,
            GameplayMode,
        }

        public void SwitchTo(UIMode mode)
        {
            _menuUI.gameObject.SetActive(false);
            _healthUI.gameObject.SetActive(false);
            _inventoryUI.gameObject.SetActive(false);

            switch (mode)
            {
                case UIMode.MenuMode: 
                    _menuUI.gameObject.SetActive(true); 
                    break;
                case UIMode.GameplayMode: 
                    _healthUI.gameObject.SetActive(true);
                    _inventoryUI.gameObject.SetActive(true);
                    break;
            }
        }

        public void OnStartButtonClicked()
        {
            Debug.Log("Start button clicked");
            EventBus.Instance?.Publish(new RunStartedEvent());
        }
    }
}
