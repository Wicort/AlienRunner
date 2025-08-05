using Assets._Project._scripts._core.Events;
using UnityEngine;

namespace Assets._Project._scripts._core.PickableObjects
{
    public class DataItem : Item, IPickableItem
    {
        public int dataAmount = 1;

        public void OnPickup()
        {

            EventBus.Instance?.Publish(new DataCollectedEvent
            {
                dataAmount = dataAmount
            });
        }
    }
}
