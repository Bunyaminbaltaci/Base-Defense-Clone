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
        public UnityAction onAddDiamondStack= delegate { };   
        public UnityAction<Transform> onGetDiamondInStack= delegate { };
    

        #endregion
        
        
        
        
        
    }
}