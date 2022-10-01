using System.Collections;
using System.Collections.Generic;
using Enums;
using Signals;
using UnityEngine;

namespace Manager
{
    public class AmmoAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        #endregion

        #endregion

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            BaseSignals.Instance.onGetBulletBox += OnGetBulletBox;
        }

        private void UnSubscribeEvent()
        {
            BaseSignals.Instance.onGetBulletBox -= OnGetBulletBox;
        }


        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion


        private GameObject OnGetBulletBox()
        {
            WaitForSeconds waiter = new WaitForSeconds(0.2f);
            
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.BulletBox);
            if (obj == null)
                return null;
            obj.transform.position = transform.position;
            obj.SetActive(true);
            return obj;
        }
    }
}