using Datas.ValueObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_SoldierData", menuName = "BaseDefense/CD_SoldierData", order = 0)]

    public class CD_SoldierData : ScriptableObject
    {
        public SoldierData SoldData;
    }
}