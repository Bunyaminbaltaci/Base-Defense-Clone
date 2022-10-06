using System.Collections.Generic;
using Datas.ValueObject;
using Enums;

namespace Keys
{
    public struct GunsDataParams
    {
        public Dictionary<GunType,GunData> GData;
        public GunType type;

    }
}