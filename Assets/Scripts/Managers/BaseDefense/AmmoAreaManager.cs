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
          
        }

        private void UnSubscribeEvent()
        {
       
        }

        

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion


        public void StartCor(Collider col)
        {
            StartCoroutine(SendBulletBox(col));
        }

        public void StopCor()
        {
            StopAllCoroutines();
        }
        IEnumerator SendBulletBox(Collider col)
        {
            WaitForSeconds waiter = new WaitForSeconds(0.2f);
            while (true)
            {
                yield return waiter;
                var obj = PoolSignals.Instance.onGetPoolObject(PoolType.BulletBox);
                if (obj == null)
                    break;
                obj.transform.position = transform.position;
                obj.SetActive(true);
                var limit=BaseSignals.Instance.OnSendBulletBox?.Invoke(col,obj);
                if (limit==0)
                    break;
            }
            
        }
    }
}