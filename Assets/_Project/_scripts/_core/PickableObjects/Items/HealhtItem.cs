using Assets._Project._scripts._core.Events;
using UnityEngine;

namespace Assets._Project._scripts._core.PickableObjects
{
    [CreateAssetMenu(fileName = "NewHealthItem", menuName ="Items/HealthItem")]
    public class HealhtItem : Item, IPickableItem
    {
        public int HealAmount = 20;

        public void OnPickup()
        {
            EventBus.Instance?.Publish(new HealPickedEvent
            {
                amount = HealAmount
            });
        }
    }
}
