using System;
using System.Collections.Generic;

namespace Keys
{
    [Serializable]
    public struct BaseDataParams
    {
        public int MinerCount;
        public int SoldierCount;
        public Dictionary<string, bool> TurretIsAuto;



    }
}