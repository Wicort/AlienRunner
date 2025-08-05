using System;

namespace Assets._Project._scripts._core.Events
{
    public struct InventoryItemChangedEvent
    {
        public Type ItemType;
        public int OldCount;
        public int NewCount;
    }
}
