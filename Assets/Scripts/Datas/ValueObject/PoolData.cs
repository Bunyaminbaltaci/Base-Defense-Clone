using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public struct PoolData
    {
        public string ObjName;
        public GameObject Pref;
        public int ObjectCount;
    }
}