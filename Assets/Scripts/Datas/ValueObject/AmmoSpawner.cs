using System;

namespace Data.ValueObject
{
    [Serializable]
    public class AmmoSpawner
    {
        public int SpawnLimit = 10;
        public int SpawnTime = 1;
    }
}