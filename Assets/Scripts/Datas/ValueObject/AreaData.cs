using System;
using Enums;

namespace ValueObject
{
    [Serializable]
    public struct AreaData
    {
        public float AreaAddedValue;
        public AreaStageType Type;
    }
}