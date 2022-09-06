using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData MovementData;
        public PlayerStackData StackData;
    }

    [Serializable]
    public class PlayerMovementData
    {
        public float PlayerJoystickSpeed = 3;
    }

    [Serializable]
    public class PlayerStackData
    {
        public int StackLimit = 10;
        public float StackoffsetY = 10;
        public float StackoffsetZ = 10;
        public float AnimationDurition = 1;
    }
}