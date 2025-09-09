using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._scripts.Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private LevelSegment _road;
        
        [SerializeField] private List<LevelBlock> _blockList = new();
        private int _levelLength;

        public int LevelLength => _levelLength;

        public void Initialize()
        {
            _levelLength = 0;
            foreach (LevelBlock block in _blockList)
            {
                _levelLength += block.Count;
            }
            Debug.Log($"Level length: {_levelLength}");
        }

        public LevelSegment GetSegment(int index)
        {
            Debug.Log($"max: {_levelLength}, index: {index}");
            if (index >= _levelLength) return null;
            LevelSegment next = null;

            foreach (LevelBlock block in _blockList) 
            {
                Debug.Log($"index: {index}, block.count: {block.Count}");
                if (index > block.Count)
                    index -= block.Count;
                else
                {
                    int x = Random.Range(0, block.LevelSegments.Count);
                    Debug.Log($"x={x}, block.LevelSegments.Count={block.LevelSegments.Count}");
                    next = block.LevelSegments[x];
                }
            }

            return next;
        }
    }
}
