using System;
using System.Collections.Generic;
using Datas.ValueObject;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        #region Mine

        public UnityAction<GameObject> onAddMinerInMine=delegate {  };
        public Func<int> onGetMinerCount= delegate { return default;};
        public UnityAction<GameObject> onAddDiamondStack= delegate { };   
        public Func<int> onGetMinerCapacity= delegate { return default;};
        public UnityAction<GameObject> OnStartCollectDiamond=delegate  {  };
        public Func<GameObject> onGetMineTarget= delegate { return default;};
        public Func<GameObject> onGetMineStackTarget= delegate { return default;};


        #endregion

        #region Barrack
        
        
        public Func<int> onGetSoldierCount= delegate { return default;};

        

        #endregion

        #region Turret

        public Func<GameObject,GameObject> onGetAmmoInStack= delegate { return default;};

        #endregion



        #region Worker

        public Func<GameObject> onGetTurretStack= delegate { return default;};
        public Func<GameObject> onGetAmmoArea= delegate { return default;};
        public Func<Collider,GameObject,int> OnSendBulletBox ;


        #endregion


        #region Enemy

        public Func<GameObject> onGetTarget= delegate { return default;};


        #endregion

    }
}