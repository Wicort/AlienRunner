#nullable enable

using Assets._Project._scripts._core.Events;
using Assets._Project._scripts._core.PickableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._scripts._core.Inventory
{
    public class InventoryService
    {
        private static InventoryService? _instance;
        private readonly Dictionary<Type, int> _itemCounts = new();

        public static InventoryService Instance
        {
            get
            {
                _instance ??= new InventoryService();
                return _instance;
            }
        }

        private InventoryService() { }

        public IEnumerable<Type> GetAllTypes() => _itemCounts.Keys;

        public void Clear() => _itemCounts.Clear();

        public int GetCount<T>() where T : Item => GetCount(typeof(T));

        public int GetCount(Type itemType)
        {
            if (itemType == null) throw new ArgumentNullException(nameof(itemType));

            return _itemCounts.TryGetValue(itemType, out int count) ? count : 0;
        }

        public void SetCount<T>(int value) where T : Item => SetCount(typeof(T), value);

        public void SetCount(Type itemType, int value)
        {
            if (itemType == null) throw new ArgumentNullException(nameof(itemType));
            if (value < 0) throw new ArgumentException("Количество не может быть отрицательным", nameof(value));

            int oldValue = GetCount(itemType);
            _itemCounts[itemType] = value;
            PublishChangeEvent(itemType, oldValue, value);
        }

        public int Add<T>(int value) where T : Item => Add(typeof(T), value);

        public int Add(Type itemType, int value)
        {
            if (itemType == null) throw new ArgumentNullException(nameof(itemType));
            if (value == 0) return 0;
            if (value < 0) return Remove(itemType, -value);

            int oldValue = GetCount(itemType);
            int newValue = oldValue + value;

            _itemCounts[itemType] = newValue;
            PublishChangeEvent(itemType, oldValue, newValue);

            return value;
        }

        public int Remove<T>(int value) where T : Item => Remove(typeof(T), value);

        public int Remove(Type itemType, int value)
        {
            if (itemType == null) throw new ArgumentNullException(nameof(itemType));
            if (value == 0) return 0;
            if (value < 0) return Add(itemType, -value);

            int oldValue = GetCount(itemType);
            int remove = Math.Min(oldValue, value);
            int newValue = oldValue - remove;

            if (remove > 0)
            {
                if (newValue == 0)
                {
                    _itemCounts.Remove(itemType);
                }
                else
                {
                    _itemCounts[itemType] = newValue;
                }
            }

            PublishChangeEvent(itemType, oldValue, newValue);

            return remove;
        }

        private void PublishChangeEvent(Type itemType, int oldCount, int newCount)
        {
            EventBus.Instance?.Publish(new InventoryItemChangedEvent
            {
                ItemType = itemType,
                OldCount = oldCount,
                NewCount = newCount
            });
        }
    }
}
