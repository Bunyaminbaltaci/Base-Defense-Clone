using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {


        public UnityAction onBuyArea=delegate {  };
        public UnityAction<int> onMoneyDown=delegate {  };
        public UnityAction<int> onDiamondDown=delegate {  };
        
        
        public UnityAction<int> onAddMoney=delegate {  };
        public UnityAction<int> onAddDiamond=delegate {  };



    }
}