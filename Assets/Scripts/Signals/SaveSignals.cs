using System;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction onSaveData = delegate { };

        public Func<IdleDataParams> onGetIdleData = delegate { return default; };
        public UnityAction<IdleDataParams> onLoadIdleData = delegate { };
        
        public Func<ScoreDataParams> onGetScoreData = delegate { return default; };

        
    }
}