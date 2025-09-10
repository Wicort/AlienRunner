using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._scripts.Levels
{
    public class Level : MonoBehaviour
    {        
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
        }

        public LevelSegment GetSegment(int index)
        {
            if (index >= _levelLength) return null;

            foreach (LevelBlock block in _blockList)
            {
                if (block.LevelSegments.Count == 0)
                {
                    continue;
                }

                if (index >= block.Count)
                {
                    index -= block.Count;
                }
                else
                {
                    // Выбираем случайный сегмент из этого блока
                    int x = Random.Range(0, block.LevelSegments.Count);
                    return block.LevelSegments[x];
                }
            }

            return null; // never reached
        }
    }
}
