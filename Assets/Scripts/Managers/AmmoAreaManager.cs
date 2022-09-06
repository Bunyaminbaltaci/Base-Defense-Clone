using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AmmoAreaManager : MonoBehaviour
    {
        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onGetammo += OnGetAmmo;
        }

        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onGetammo -= OnGetAmmo;

        }
        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion
        private GameObject OnGetAmmo()
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Bullet);
            if (obj == null)
                return null;
            obj.transform.position = transform.position;
            obj.SetActive(true);
            return obj;
        }
    }
}