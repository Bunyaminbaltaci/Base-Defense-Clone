using System;
using System.Collections.Generic;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {

        public UnityAction<GameStates> onSetGameState = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };

        public UnityAction<Transform> onSetCameraTarget = delegate { };
        public UnityAction<GameStates> onChangeGameState = delegate { };
        
       
        public Func<GameObject,GameObject> onGetHostageTarget;


      
    }
}