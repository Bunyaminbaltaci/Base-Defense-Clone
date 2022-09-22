using Datas.ValueObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "BaseDefense/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData Data;
    }
}