using UnityEngine;

namespace Assets._Project._scripts.Levels
{
    public class LevelSegment : MonoBehaviour
    {
        [SerializeField] private float _length;

        public float Length => _length; 
    }
}
