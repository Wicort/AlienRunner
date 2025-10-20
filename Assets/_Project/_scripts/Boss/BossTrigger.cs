using Assets._Project._scripts._core.Events;
using UnityEngine;

namespace Assets._Project._scripts.Boss
{
    public class BossTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EventBus.Instance?.Publish(new BossStartEvent());
        }
    }
}
