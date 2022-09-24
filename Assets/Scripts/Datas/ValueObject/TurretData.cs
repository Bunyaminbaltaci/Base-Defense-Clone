using System;

namespace Datas.ValueObject
{
    [Serializable]
    public class TurretData
    {
        public StackData BulletBoxStackData;
        public int FireRate=1;
        public int Damage=1;

    }
}