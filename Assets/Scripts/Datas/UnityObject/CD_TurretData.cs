using Datas.ValueObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_TurretData", menuName = "BaseDefense/CD_TurretData", order = 0)]

    public class CD_TurretData : ScriptableObject
    {

        public TurretData Data;

    }
}