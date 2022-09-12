using Datas.ValueObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_MinerData", menuName = "BaseDefense/CD_MinerData", order = 0)]
    public class CD_MinerData : ScriptableObject
    {
        public MinerData MinData;
    }
}