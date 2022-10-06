using System;
using System.Collections.Generic;
using Datas.ValueObject;
using Extentions;
using Keys;
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

        public Func<GameObject,int> onGetTurretLimit= delegate { return default; };
        public UnityAction<GameObject, GameObject> onSendAmmoInStack=delegate{};
        public UnityAction<GameObject,TurretParams> onHoldTurretData=delegate {  };
        public UnityAction<GameObject> onPlayerInTurret = delegate { };
        public UnityAction<GameObject> onPlayerOutTurret = delegate { };
        public Func<int> onGetTurretDamage= delegate { return default;};
        
        

        #endregion



        #region Worker

        public Func<GameObject> onGetTurretStack= delegate { return default;};
        public Func<GameObject> onGetAmmoArea= delegate { return default;};
        public Func<GameObject> onGetEnter= delegate { return default;};
        public Func<GameObject> onGetHarvesterTarget= delegate { return default;};


        #endregion


        #region Enemy

        public Func<GameObject> onGetEnemyTarget= delegate { return default;};
        public UnityAction<GameObject> onRemoveInDamageableStack = delegate { };


        #endregion

        #region BulleTArea Area

        public Func<GameObject> onGetBulletBox= delegate { return default;};

        #endregion

        #region Money 
        
        public UnityAction<GameObject> onAddHaversterTargetList=delegate{  };
        public UnityAction<GameObject> onRemoveHaversterTargetList=delegate{  };

        #endregion

        #region GunShop

        public Func<int> onGetGunDamage= delegate { return default;};

        #endregion

    }
}