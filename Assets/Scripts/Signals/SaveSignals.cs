using System;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction onSaveIdleData = delegate { };
        public UnityAction onSaveScoreData = delegate { };

        public Func<IdleDataParams> onGetSaveIdleData = delegate { return default;};
        public Func<IdleDataParams> onLoadIdleData = delegate { return default;};    
        
        public Func<ScoreDataParams> onGetSaveScoreData = delegate { return default; };
        public Func<ScoreDataParams> onLoadScoreData = delegate { return default;};
        
      
        
     
        
    }
}