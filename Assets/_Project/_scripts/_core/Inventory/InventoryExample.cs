using Assets._Project._scripts._core.Events;
using Assets._Project._scripts._core.PickableObjects;
using System;
using UnityEngine;

namespace Assets._Project._scripts._core.Inventory
{
    public class InventoryExample : MonoBehaviour
    {
        public InventoryService Inventory = InventoryService.Instance;

        private void Start()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Instance?.Subscribe<SoftCurrencyChangedEvent>(OnSoftCurrencyChanged);
            EventBus.Instance?.Subscribe<HardCurrencyChangedEvent>(OnHardCurrencyChanged);
            EventBus.Instance?.Subscribe<DataCollectedEvent>(OnDataCollected);
        }

        private void OnDataCollected(DataCollectedEvent @event)
        {
            Inventory.Add<DataItem>(@event.dataAmount);
        }

        private void OnHardCurrencyChanged(HardCurrencyChangedEvent @event)
        {
            Inventory.Add<HardCurrencyItem>(@event.amount);
        }

        private void OnSoftCurrencyChanged(SoftCurrencyChangedEvent @event)
        {
            Inventory.Add<SoftCurrencyItem>(@event.amount);
        }

        private void OnDestroy()
        {
            EventBus.Instance?.Unsubscribe<SoftCurrencyChangedEvent>(OnSoftCurrencyChanged);
            EventBus.Instance?.Unsubscribe<HardCurrencyChangedEvent>(OnHardCurrencyChanged);
            EventBus.Instance?.Unsubscribe<DataCollectedEvent>(OnDataCollected);
        }
    }
}
