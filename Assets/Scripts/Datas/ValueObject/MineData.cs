using System;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class MineData
    {
        public StackData SData;
        [Header("Worker")] 
        public int MinerCapacity;
        
   
    }
}