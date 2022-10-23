using Keys;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_GunData", menuName = "BaseDefense/CD_GunData", order = 0)]

    public class CD_GunData : ScriptableObject
    {
        public GunsData Data;
    }
}