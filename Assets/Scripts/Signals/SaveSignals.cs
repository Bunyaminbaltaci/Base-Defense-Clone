using System;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction onSaveData = delegate { };

        public Func<IdleDataParams> onGetIdleDatas = delegate { return default; };
        public UnityAction<IdleDataParams> onLoadIdleData = delegate { };
        
    }
}