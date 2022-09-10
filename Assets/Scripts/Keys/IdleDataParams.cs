using System;
using System.Collections.Generic;
using ValueObject;

namespace Keys
{
    [Serializable]
    public struct IdleDataParams
    {
        public int CityLevel;
        public Dictionary<int, AreaData> AreaDictionary;
    }
}