using Keys;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_GunShopData", menuName = "BaseDefense/CD_GunShopData", order = 0)]

    public class CD_GunShopData : ScriptableObject
    {
        public GunsData Data;
    }
}