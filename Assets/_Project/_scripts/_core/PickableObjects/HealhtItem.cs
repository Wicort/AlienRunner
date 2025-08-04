using UnityEngine;

namespace Assets._Project._scripts._core.PickableObjects
{
    [CreateAssetMenu(fileName = "NewHealthItem", menuName ="Items/HealthItem")]
    public class HealhtItem : Item, IPickableItem
    {
        public int HealAmount = 20;

        public void OnPickup()
        {
            Debug.Log("Health Item picked");
        }
    }
}
