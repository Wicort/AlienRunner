using Assets._Project._scripts._core.Events;
using System;
using UnityEngine;

namespace Assets._Project._scripts._core.PickableObjects.Items
{
    [CreateAssetMenu(fileName = "NewBulletItem", menuName = "Items/BulletItem")]
    public class BulletItem : Item, IPickableItem
    {
        public int bulletAmount = 1;

        public void OnPickup()
        {
#if UNITY_EDITOR
            Debug.Log("Патроны получены");
#endif

            EventBus.Instance?.Publish(new BulletCollectedEvent
            {
                bulletAmount = bulletAmount
            });
        }
    }
}
