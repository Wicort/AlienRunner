using Assets._Project._scripts._core.Events;
using UnityEngine;

namespace Assets._Project._scripts._core.PickableObjects
{
    [CreateAssetMenu(fileName = "NewHardCurrencyItem", menuName = "Items/HardCurrencyItem")]
    public class HardCurrencyItem : Item, IPickableItem
    {
        public int amount = 1;

        public void OnPickup()
        {
            EventBus.Instance?.Publish(new HardCurrencyChangedEvent
            {
                amount = amount
            });
        }
    }
}
