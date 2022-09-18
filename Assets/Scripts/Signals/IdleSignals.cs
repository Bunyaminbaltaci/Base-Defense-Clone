using System;
using System.Collections.Generic;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class IdleSignals : MonoSingleton<IdleSignals>
    {
        #region Mine

        public Func<GameObject> onGetMineTarget= delegate { return default;};
        public Func<GameObject> onGetMineStackTarget= delegate { return default;};
        public UnityAction<GameObject> onAddDiamondStack= delegate { };   
    

        #endregion
        
        
        
    }
}