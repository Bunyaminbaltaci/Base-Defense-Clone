using Datas.ValueObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_WorkerAreaData", menuName = "BaseDefense/CD_WorkerAreaData", order = 0)]
    public class CD_WorkerAreaData : ScriptableObject
    {
        public WorkerAreaData Data;
    }
}