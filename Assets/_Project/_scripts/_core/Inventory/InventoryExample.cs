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
            EventBus.Instance?.Subscribe<HealPickedEvent>(OnHealPicked);
        }

        private void OnHealPicked(HealPickedEvent @event)
        {
            Debug.Log($"Игрок излечен на {@event.amount} очков здоровья");
        }

        private void OnDataCollected(DataCollectedEvent @event)
        {
            Debug.Log($"Collected Data: {@event.dataAmount}");
            Inventory.Add<DataItem>(@event.dataAmount);
        }

        private void OnHardCurrencyChanged(HardCurrencyChangedEvent @event)
        {
            Debug.Log($"Hard currency picked {@event.amount}");
            Inventory.Add<HardCurrencyItem>(@event.amount);
        }

        private void OnSoftCurrencyChanged(SoftCurrencyChangedEvent @event)
        {

            Debug.Log($"Soft currency picked {@event.amount}");
            Inventory.Add<SoftCurrencyItem>(@event.amount);
        }

        private void OnDestroy()
        {
            EventBus.Instance?.Unsubscribe<SoftCurrencyChangedEvent>(OnSoftCurrencyChanged);
            EventBus.Instance?.Unsubscribe<HardCurrencyChangedEvent>(OnHardCurrencyChanged);
            EventBus.Instance?.Unsubscribe<DataCollectedEvent>(OnDataCollected);
            EventBus.Instance?.Unsubscribe<HealPickedEvent>(OnHealPicked);
        }
    }
}
