using System;

namespace Datas.ValueObject
{
    [Serializable]
    public class TurretData
    {
        public StackData BulletBoxStackData;
        public float FireRate=1;
        public int Damage=1;
        public int AutoModeCost=500;

    }
}