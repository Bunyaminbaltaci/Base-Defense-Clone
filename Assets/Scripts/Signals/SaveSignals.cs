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

        public Func<BaseDataParams> onGetBaseData = delegate { return default;};
        public Func<BaseDataParams> onLoadBaseData = delegate { return default;};    
        
        public Func<ScoreDataParams> onGetSaveScoreData = delegate { return default; };
        public Func<ScoreDataParams> onLoadScoreData = delegate { return default;};
        
      
        
     
        
    }
}