using System;
using Datas.ValueObject;
using Enums;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        public GunType WeaponType;
        public float PlayerJoystickSpeed = 3;
        public int StackLevel = 1;
        public StackData BulletBoxStackData;
        public StackData MoneyBoxStackData;
        
    }

    
}