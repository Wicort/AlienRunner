using Assets._Project._scripts._core.PickableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._scripts._core.Inventory
{
    public class ItemIconLoader
    {
        private static readonly Dictionary<Type, Item> ItemCache = new();

        public static Item GetItemByType(Type type)
        {
            if (ItemCache.TryGetValue(type, out Item cached))
                return cached;

            // Загружаем все объекты из папки
            Item[] items = Resources.LoadAll<Item>("Configs/Items");
            foreach (var item in items)
            {
                if (item.GetType() == type)
                {
                    ItemCache[type] = item;
                    return item;
                }
            }

            Debug.LogWarning($"[ItemIconLoader] Предмет типа {type} не найден в Resources/Configs/Items");
            return null;
        }

        public static Sprite GetIconByType(Type type)
        {
            Item item = GetItemByType(type);
            return item?.Icon;
        }

        public static Sprite GetIconByType<T>() where T : Item => GetIconByType(typeof(T));
    }
}
