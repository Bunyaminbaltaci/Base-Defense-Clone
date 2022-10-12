using System;
using System.Collections;
using Enums;
using Manager;
using Signals;
using TMPro;
using UnityEngine;

namespace Controller.WorkerArea
{
    public class WorkerBuyAreaController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public TextMeshPro CostText;

        #endregion

        #region Serialized Variables

        [SerializeField] private WorkerAreaManager workerAreaManager;
        [SerializeField] private WorkerType type;

        #endregion

        #region Private Variables

        #endregion

        #endregion


        private void SetAreaTexts()
        {
            CostText.text = (int.Parse(CostText.text) - 1).ToString();
        }


        private void AreaCostDown()
        {
            SetAreaTexts();
            if (int.Parse(CostText.text) < 1)
            {
                StopAllCoroutines();
                BuyWorker();
            }
        }

        private void BuyWorker()
        {
            switch (type)
            {
                case WorkerType.Harvester:
                    workerAreaManager.BuyHarvester();
                    break;
                case WorkerType.Support:
                    workerAreaManager.BuySupport();
                    break;
            }
        }


        IEnumerator StartBuy()
        {
            int money = ScoreSignals.Instance.onGetMoney();
            WaitForSeconds timer = new WaitForSeconds(0.002f);
            while (money > int.Parse(CostText.text))
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

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StopAllCoroutines();
            }
        }
    }
}