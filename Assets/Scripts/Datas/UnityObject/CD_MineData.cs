using Datas.ValueObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_MineData", menuName = "BaseDefense/CD_MineData", order = 0)]
    public class CD_MineData : ScriptableObject
    {
        public MineData Data;
    }
}