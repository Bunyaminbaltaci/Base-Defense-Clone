using System;
using Datas.ValueObject;
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
        public UnityAction onSaveGunShopData=delegate {  };
        public UnityAction onSaveWorkerAreaData=delegate {  };

        #endregion

        #region Base

        public Func<IdleDataParams> onGetBaseData = delegate { return default; };
        public Func<IdleDataParams> onLoadBaseData = delegate { return default; };

        #endregion

        #region Score

        public Func<ScoreDataParams> onGetSaveScoreData = delegate { return default; };
        public Func<ScoreDataParams> onLoadScoreData = delegate { return default; };

        #endregion

        #region Idle

        public Func<BaseDataParams> onGetSaveIdleData = delegate { return default; };
        public Func<BaseDataParams> onLoadIdleData = delegate { return default; };

        #endregion

        #region GunShop

        public Func<GunsDataParams>onGetGunShopData= delegate { return default;};
        public Func<GunsDataParams> onLoadGunShopData = delegate { return default; };  
        
        #endregion

        #region WorkerArea

        public Func<WorkerDataParams>onGetWorkerAreapData= delegate { return default;};
        public Func<WorkerDataParams> onLoadWorkerAreaData = delegate { return default; };
        

        #endregion
    }
}