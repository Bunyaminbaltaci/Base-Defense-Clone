using System;
using ValueObject;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public UnityAction onAreaComplete = delegate { };
        public UnityAction onBaseComplete = delegate { };
        public UnityAction onRefreshAreaData = delegate { };
        public UnityAction onPrepareAreaWithSave = delegate { };
        public UnityAction<GameObject> onCheckArea = delegate { };
        public UnityAction<string, AreaData> onSetAreaData = delegate { };
        public Func<string, AreaData> onGetAreaData = delegate { return default; };
        public Func<GameObject> onGetTarget= delegate { return default;};
    }
}