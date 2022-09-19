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
        public UnityAction<int, AreaData> onSetAreaData = delegate { };
        public Func<int, AreaData> onGetAreaData = delegate { return default; };
    }
}