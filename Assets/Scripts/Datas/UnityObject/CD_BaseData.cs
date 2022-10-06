using System.Collections.Generic;
using ValueObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_BaseData", menuName = "BaseDefense/CD_BaseData", order = 0)]
    public class CD_BaseData : ScriptableObject
    {
        public List<BaseData> DataList;
    }
}