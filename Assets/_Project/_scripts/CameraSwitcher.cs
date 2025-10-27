using Assets._Project._scripts._core;
using Cinemachine;
using UnityEngine;

namespace Assets._Project._scripts
{
    public class CameraSwitcher : Singleton<CameraSwitcher>
    {
        [SerializeField] private CinemachineVirtualCamera _menuCamera;
        [SerializeField] private CinemachineVirtualCamera _gameplayCamera;
        [SerializeField] private CinemachineVirtualCamera _hubHeroCamera;

        public enum CameraMode
        {
            MenuCamera,
            GameplayCamera,
            HubHeroCamera,
        }

        private void Start()
        {
            SwitchTo(CameraMode.MenuCamera);
        }

        public void SwitchTo(CameraMode mode)
        {
            _menuCamera.Priority = 0;
            _gameplayCamera.Priority = 0;
            _hubHeroCamera.Priority = 0;

            switch (mode)
            {
                case CameraMode.MenuCamera     : SetPriority(_menuCamera, 10);     break;
                case CameraMode.GameplayCamera : SetPriority(_gameplayCamera, 10); break;
                case CameraMode.HubHeroCamera  : SetPriority(_hubHeroCamera, 10);  break;
            }
        }

        private void SetPriority(CinemachineVirtualCamera vcam, int priority)
        {
            if (vcam != null)
                vcam.Priority = priority;
        }
    }
}
