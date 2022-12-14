using System;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class StackData
    {
        #region OffSet
        [Space(15),Header("Offset")] 
        [Range(0f, 1f)] public float OffsetX;
        [Range(0f, 1f)] public float OffsetY;
        [Range(0f, 1f)] public float OffsetZ;
        [Space(15)]
        #endregion

        #region Data

        [Header("Limit")] 
        public int LimitX;
        public int LimitY;
        public int LimitZ;
        [Space(15)]

        #endregion 
        public int StackLimit = 10;
        public float AnimationDurition = 1;
    }


   

 
}