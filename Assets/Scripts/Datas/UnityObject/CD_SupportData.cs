using Datas.ValueObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_SupportData", menuName = "BaseDefense/CD_SupportData", order = 0)]

    public class CD_SupportData : ScriptableObject
    {
        public WorkerData SupportData;
    }
}