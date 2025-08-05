using Assets._Project._scripts._core.Events;
using Assets._Project._scripts._core.PickableObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project._scripts._core.Inventory
{
    public class InventoryUIExample : MonoBehaviour
    {
        [SerializeField] private Text _softText;
        [SerializeField] private Text _hardText;
        [SerializeField] private InventoryExample _inventoryExample;

        private InventoryService _inventory => _inventoryExample.Inventory;

        private void Start()
        {
            UpdateUI();
            EventBus.Instance.Subscribe<InventoryItemChangedEvent>(OnInventoryChanged);
        }

        private void UpdateUI()
        {
            _softText.text = _inventory.GetCount<SoftCurrencyItem>().ToString();
            _hardText.text = _inventory.GetCount<HardCurrencyItem>().ToString();
        }

        private void OnInventoryChanged(InventoryItemChangedEvent @event)
        {
            if (@event.ItemType == typeof(SoftCurrencyItem) ||
                @event.ItemType == typeof(HardCurrencyItem))
            {
                UpdateUI();
            }
        }
    }
}
