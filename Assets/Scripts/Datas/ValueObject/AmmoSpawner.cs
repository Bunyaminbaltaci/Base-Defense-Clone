using System;

namespace Data.ValueObject
{
    [Serializable]
    public class AmmoSpawner
    {
        public int SpawnLimit = 10;
        public float SpawnTime = 1;
        public int VisibleLimit = 4;
    }
}