using System;
using Enums;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct PoolData
    {
        public PoolType Type;
        public GameObject Pref;
        public int ObjectCount;
    }
}