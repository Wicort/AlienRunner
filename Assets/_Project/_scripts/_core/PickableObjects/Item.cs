using UnityEngine;

namespace Assets._Project._scripts._core.PickableObjects
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
    public abstract class Item: ScriptableObject
    {
        public string DisplayName;
        public Sprite Icon;
        [TextArea] public string Description;
    }
}
