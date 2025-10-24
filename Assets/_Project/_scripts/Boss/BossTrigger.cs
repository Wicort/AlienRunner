using Assets._Project._scripts._core.Events;
using Assets._Project._scripts._core.Events.Structs;
using UnityEngine;

namespace Assets._Project._scripts.Boss
{
    public class BossTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EventBus.Instance?.Publish(new BossStartEvent());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EventBus.Instance.Publish(new BossKilledEvent());
            }
        }
    }
}
