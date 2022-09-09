using System;
using Enums;

namespace Datas.ValueObject
{
    [Serializable]
    public struct AreaData
    {
        public float AreaAddedValue;
        public AreaStageType Type;
    }
}