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

        public Func<GameObject> onGetBulletBox = delegate { return default; };
        public Func<GameObject> onGetAmmoArea = delegate { return default; };
        public UnityAction<GameStates> onGetGameState = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };

        public UnityAction<Transform> onSetCameraTarget = delegate { };
        public UnityAction<GameStates> onSetGameState = delegate { };
        
       
        public Func<GameObject,GameObject> onGetHostageTarget;
    }
}