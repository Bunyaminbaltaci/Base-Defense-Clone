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
        
        public Func<BaseDataParams> onGetBaseData = delegate { return default;};
        public Func<BaseDataParams> onLoadBaseData = delegate { return default;};
        
        #endregion
        
        #region Score

        public Func<ScoreDataParams> onGetSaveScoreData = delegate { return default; };
        public Func<ScoreDataParams> onLoadScoreData = delegate { return default;};     

        #endregion

        #region Idle

        public Func<IdleDataParams> onGetSaveIdleData = delegate { return default; };
        public Func<IdleDataParams> onLoadIdleData = delegate { return default;};

        #endregion
        
        
        
      
        
     
        
    }
}