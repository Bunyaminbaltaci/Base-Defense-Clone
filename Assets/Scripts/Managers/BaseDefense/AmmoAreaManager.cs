using Enums;
using Signals;
using UnityEngine;

namespace Manager
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
            CoreGameSignals.Instance.onGetBulletBox += OnGetAmmo;
            CoreGameSignals.Instance.onGetAmmoArea += OnGetAmmoArea;
        }

        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onGetBulletBox -= OnGetAmmo;
            CoreGameSignals.Instance.onGetAmmoArea -= OnGetAmmoArea;
        }

        private GameObject OnGetAmmoArea() => gameObject;
        

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion
        
        private GameObject OnGetAmmo()
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.BulletBox);
            if (obj == null)
                return null;
            obj.transform.position = transform.position;
            obj.SetActive(true);
            return obj;
        }
    }
}