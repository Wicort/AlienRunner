using System;
using System.Collections.Generic;

namespace Assets._Project._scripts.Levels
{
    [Serializable]
    public class LevelBlock
    {
        public List<LevelSegment> LevelSegments = new();
        public int Count;
    }
}
