using System;
using Datas.ValueObject;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        public float PlayerJoystickSpeed = 3;
        public int StackLevel = 1;
        public StackData BulletBoxStackData;
        public StackData MoneyBoxStackData;
        
    }

    
}