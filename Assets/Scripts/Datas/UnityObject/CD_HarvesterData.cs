using Datas.ValueObject;
using UnityEngine;

namespace Data
{    
    [CreateAssetMenu(fileName = "CD_HarvesterData", menuName = "BaseDefense/CD_HarvesterData", order = 0)]

    public class CD_HarvesterData : ScriptableObject
    {
        public WorkerData WorkerData;

    }
}