using UnityEngine;

namespace Assets._Project._scripts._core.PickableObjects
{
    public class PickableItem : MonoBehaviour
    {
        public Item item;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (item is IPickableItem pickableItem)
                {
                    pickableItem.OnPickup();
                    gameObject.SetActive(false);
                    //GameEvents.OnItemPicked?.Invoke(item);
                }
            }
        }
    }
}
