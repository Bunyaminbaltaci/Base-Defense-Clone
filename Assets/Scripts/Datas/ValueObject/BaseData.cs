using System;
using UnityEngine;

namespace ValueObject
{
    [Serializable]
    public struct BaseData
    {
        [Header("Data"), Space(15)] public int BuildCount;
    }
}