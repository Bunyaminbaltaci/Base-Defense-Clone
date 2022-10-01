using System;
using System.Collections.Generic;
using ValueObject;

namespace Keys
{
    [Serializable]
    public struct IdleDataParams
    {
        public int BaseLevel;
        public Dictionary<string, AreaData> AreaDictionary;
    }
}