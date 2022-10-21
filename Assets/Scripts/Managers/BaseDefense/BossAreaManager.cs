using System.Collections;
using Signals;
using UnityEngine;

namespace Managers.BaseDefense
{
    public class BossAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject crosshair;
        [SerializeField] private GameObject portalGate;

        #endregion

        #region Private Variables

        #endregion

        #endregion


        private void Awake()
        {
        }

        #region Event Subscribe

        private void OnEnable()
        {
            EventSubscribe();
        }

        private void EventSubscribe()
        {
            BaseSignals.Instance.onSetCrosshair += OnSetCrosshair;
            BaseSignals.Instance.onBossIsDead += OnBossIsDead;

        }

        private void EventUnSubscribe()
        {
            BaseSignals.Instance.onSetCrosshair -= OnSetCrosshair;
            BaseSignals.Instance.onBossIsDead -= OnBossIsDead;

        }

       


        private void OnDisable()
        {
            EventUnSubscribe();
        }

        #endregion
        
        private void OnSetCrosshair(Vector3 playerTransform)
        {
            crosshair.transform.position = playerTransform;
            crosshair.SetActive(true);
            StartCoroutine(crosshairTimer());
        }

        IEnumerator crosshairTimer()
        {
            WaitForSeconds waiter = new WaitForSeconds(2f);
            yield return waiter;
            crosshair.SetActive(false);
        }
        
        private void OnBossIsDead()
        {
         portalGate.SetActive(false);
        }

   
    }
}