using System.Collections;
using Datas.ValueObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CD_StackData", menuName = "BaseDefense/CD_StackData", order = 0)]
    public class CD_StackData : ScriptableObject
    {
        public StackData SData;

    }
}