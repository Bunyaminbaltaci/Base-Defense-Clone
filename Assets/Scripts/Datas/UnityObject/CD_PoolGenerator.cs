using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_PoolGenerator", menuName = "BaseDefense/CD_PoolGenerator", order = 0)]
    public class CD_PoolGenerator : ScriptableObject
    {
        public List<PoolData> PoolDataList;
    }
}