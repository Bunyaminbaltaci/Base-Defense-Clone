using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameObject> onCheckAreaControl = delegate { };
        public UnityAction onEnterFinish = delegate { };

        public Func<GameObject> onGetammo = delegate { return default; };
        public UnityAction<GameStates> onGetGameState = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };

        public UnityAction<Transform> onSetCameraTarget = delegate { };
        public UnityAction<GameStates> onSetGameState = delegate { };
    }
}