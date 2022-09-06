using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AmmoSpawner", menuName = "BaseDefense/CD_AmmoSpawner", order = 0)]

    public class CD_AmmoSpawner : ScriptableObject
    {
        public AmmoSpawner AmmoSpawnerData;
    }
}