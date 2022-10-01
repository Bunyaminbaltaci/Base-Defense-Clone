using System;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        #region Save

        public UnityAction onSaveBaseData = delegate { };
        public UnityAction onSaveScoreData = delegate { };
        public UnityAction onSaveIdleData = delegate { };
        

        #endregion
        #region Base
        
        public Func<IdleDataParams> onGetBaseData = delegate { return default;};
        public Func<IdleDataParams> onLoadBaseData = delegate { return default;};
        
        #endregion
        
        #region Score

        public Func<ScoreDataParams> onGetSaveScoreData = delegate { return default; };
        public Func<ScoreDataParams> onLoadScoreData = delegate { return default;};     

        #endregion

        #region Idle

        public Func<BaseDataParams> onGetSaveIdleData = delegate { return default; };
        public Func<BaseDataParams> onLoadIdleData = delegate { return default;};

        #endregion
        
        
        
      
        
     
        
    }
}