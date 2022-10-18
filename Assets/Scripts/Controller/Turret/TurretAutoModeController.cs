using System.Collections;
using Controller;
using Signals;
using TMPro;
using UnityEngine;

namespace Controller.Turret
{
    public class TurretAutoModeController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public TextMeshPro AutoModeCost;

        #endregion

        #region Serialized Variables

        [SerializeField] private TurretManager turretManager;

        #endregion

        #endregion

        private void SetAreaTexts()
        {
            AutoModeCost.text = (int.Parse(AutoModeCost.text) - 1).ToString();
        }


        private void AreaCostDown()
        {
            SetAreaTexts();
            if (int.Parse(AutoModeCost.text) <= 0)
            {
                turretManager.IsBuyAutoMode();
                StopAllCoroutines();
                IsComplete();
            }
            
        }

        
        IEnumerator StartBuy()
        {
            int money = ScoreSignals.Instance.onGetMoney();
            WaitForSeconds timer = new WaitForSeconds(0.002f);
            while (money > int.Parse(AutoModeCost.text))
            {
                AreaCostDown();
                ScoreSignals.Instance.onMoneyDown?.Invoke(1);
                yield return timer;
            }
            
        }


        public void IsComplete()
        {
        gameObject.SetActive(false);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(StartBuy());
            }
        }

      
    }
}   