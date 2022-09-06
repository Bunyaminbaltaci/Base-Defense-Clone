using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PoolGenerator", menuName = "BaseDefense/CD_PoolGenerator", order = 0)]

    public class CD_PoolGenerator : ScriptableObject
    {
        public List<PoolData> PoolDataList;
    }
}