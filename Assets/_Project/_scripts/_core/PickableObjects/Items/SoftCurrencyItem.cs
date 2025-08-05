using UnityEngine;
using Assets._Project._scripts._core.Events;

namespace Assets._Project._scripts._core.PickableObjects
{
    [CreateAssetMenu(fileName = "NewSoftCurrencyItem", menuName = "Items/SoftCurrencyItem")]
    public class SoftCurrencyItem : Item, IPickableItem
    {
        public int amount = 1;

        public void OnPickup()
        {            
            EventBus.Instance?.Publish(new SoftCurrencyChangedEvent
            {
                amount = amount
            });
        }
    }
}
