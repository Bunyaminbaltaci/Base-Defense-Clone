using System;
using Datas.ValueObject;
using Enums;
using UnityEngine.Rendering;

namespace Keys
{
    [Serializable]

    public class GunsData
    {
        public SerializedDictionary<GunType,GunData> GData;
    }
    
}