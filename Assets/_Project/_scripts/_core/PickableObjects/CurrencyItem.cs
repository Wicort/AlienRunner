using UnityEngine;

namespace Assets._Project._scripts._core.PickableObjects
{
    [CreateAssetMenu(fileName = "CurrencyItem", menuName = "Items/CurrencyItem")]
    public class CurrencyItem : Item, IPickableItem
    {
        public int amount = 1;

        public void OnPickup()
        {
            // CheckCurrencyType
            // GameEvents.OnCurrencyPicked(amount);
            Debug.Log("Currency picked");
        }
    }
}
