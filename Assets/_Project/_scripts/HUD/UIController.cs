using Assets._Project._scripts._core;
using UnityEngine;

namespace Assets._Project._scripts.HUD
{
    public class UIController : Singleton<UIController>
    {
        [SerializeField] private GameObject _menuUI;
        [SerializeField] private GameObject _healthUI;
        [SerializeField] private GameObject _inventoryUI;

        public enum UIMode
        {
            MenuMode,
            GameplayMode,
        }

        public void SwitchTo(UIMode mode)
        {
            _menuUI.SetActive(false);
            _healthUI.SetActive(false);
            _inventoryUI.SetActive(false);

            switch (mode)
            {
                case UIMode.MenuMode: 
                    _menuUI.SetActive(true); 
                    break;
                case UIMode.GameplayMode: 
                    _healthUI.SetActive(true);
                    _inventoryUI.SetActive(true);
                    break;
            }
        }
    }
}
