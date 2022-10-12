using System;
using System.Collections.Generic;
using Enums;
using Keys;


namespace Datas.ValueObject
{
    [Serializable]
    public class WorkerAreaData
    {
        public WorkerDataParams WorkerDatas;
        public int HarvesterCost;
        public int SupportCost;
    }
}