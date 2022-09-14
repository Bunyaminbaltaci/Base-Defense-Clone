using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_AmmoSpawner", menuName = "BaseDefense/CD_AmmoSpawner", order = 0)]
    public class CD_AmmoSpawner : ScriptableObject
    {
        public AmmoSpawnerData AmmoSpawnerData;
    }
}