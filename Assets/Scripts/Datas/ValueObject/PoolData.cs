using System;
using Enums;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public struct PoolData
    {
        public PoolType Type;
        public GameObject Pref;
        public int ObjectCount;
    }
}