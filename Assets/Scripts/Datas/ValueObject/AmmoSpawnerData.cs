using System;

namespace Data
{
    [Serializable]
    public class AmmoSpawnerData
    {
        public int SpawnLimit = 10;
        public float SpawnTime = 1;
        public int VisibleLimit = 4;
    }
}