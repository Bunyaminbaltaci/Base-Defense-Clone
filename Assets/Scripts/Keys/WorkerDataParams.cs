using System;
using System.Collections.Generic;
using Enums;

namespace Keys
{
    [Serializable]
    public struct WorkerDataParams
    {
      
        public int SpeedLevel;
        public int CapacityLevel;
        public Dictionary<WorkerType, bool> WorkerList;
    }
}