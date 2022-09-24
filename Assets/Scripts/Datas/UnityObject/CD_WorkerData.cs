using Datas.ValueObject;
using UnityEngine;

namespace Data
{    
    [CreateAssetMenu(fileName = "CD_WorkerData", menuName = "BaseDefense/CD_WorkerData", order = 0)]

    public class CD_WorkerData : ScriptableObject
    {
        public WorkerData HarvesterData;
        public WorkerData SupportData;

    }
}