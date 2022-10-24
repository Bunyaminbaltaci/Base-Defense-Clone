using System;
using System.Collections;
using Data;
using ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Controller
{
    public class AreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private BuildType buildType;
        [SerializeField] private PaymentType paymentType;
        [SerializeField] private TextMeshPro buildCost;
        [SerializeField] private GameObject buildCostArea;
        [SerializeField] private GameObject fence;
        [SerializeField] private GameObject area;
        [SerializeField] private string _areaId;

        #endregion

        #region Private Variables

        private BuildData _buildData;
        private AreaData _areaData;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void Init()
        {
            CostAreaVisible();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            IdleSignals.Instance.onRefreshAreaData += OnRefreshAreaData;
            IdleSignals.Instance.onPrepareAreaWithSave += OnPrepareAreaWithSave;
        }

        private void UnSubscribeEvents()
        {
            IdleSignals.Instance.onRefreshAreaData -= OnRefreshAreaData;
            IdleSignals.Instance.onPrepareAreaWithSave -= OnPrepareAreaWithSave;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion


        private void Start()
        {
            IdleSignals.Instance.onRefreshAreaData?.Invoke();
        }

        private void GetReferences()
        {
            _buildData = GetData();
            buildCost.text = _buildData.AreaCost.ToString();
            _areaId = gameObject.name;
        }


        private BuildData GetData()
        {
            return Resources.Load<CD_BuildData>("Data/CD_BuildData").BuildData[(int)buildType];
        }


        private void OnRefreshAreaData()
        {
            _areaData = (AreaData)IdleSignals.Instance.onGetAreaData?.Invoke(_areaId);
            CostAreaVisible();
            SetAreaTexts();
        }

        private void AreaCostDown()
        {
            switch (_areaData.Type)
            {
                case AreaStageType.Build:
                    _areaData.AreaAddedValue++;
                    SetAreaTexts();
                    if (_buildData.AreaCost == _areaData.AreaAddedValue) ChangeStage();
                    break;
            }
        }


        private void SetAreaTexts()
        {
            buildCost.text = (_buildData.AreaCost - _areaData.AreaAddedValue).ToString();
        }

        private void ChangeStage()
        {
            if (_areaData.Type == AreaStageType.Build)
            {
                _areaData.Type = AreaStageType.Complete;
                IdleSignals.Instance.onAreaComplete?.Invoke();
                StopBuy();
                CostAreaVisible();
            }
            else
            {
                CostAreaVisible();
            }
        }

        private void CostAreaVisible()
        {
            switch (_areaData.Type)
            {
                case AreaStageType.Build:
                    buildCostArea.SetActive(true);
                    fence.SetActive(true);
                    area.SetActive(false);
                    break;
                case AreaStageType.Complete:
                    buildCostArea.SetActive(false);
                    fence.SetActive(false);
                    area.SetActive(true);
                    break;
            }
        }

        public void StartBuy()
        {
            switch (paymentType)
            {
                case PaymentType.Money:
                    StartCoroutine(BuyWithMoney());
                    break;
                case PaymentType.Diamond:
                    StartCoroutine(BuyWithDiamond());
                    break;
            }
        }

        public void StopBuy()
        {
            StopAllCoroutines();
        }

        IEnumerator BuyWithMoney()
        {
            int money = ScoreSignals.Instance.onGetMoney();
            WaitForSeconds timer = new WaitForSeconds(0.05f);
            while (money > _buildData.AreaCost)
            {
                AreaCostDown();
                ScoreSignals.Instance.onMoneyDown?.Invoke(1);
                yield return timer;
            }

            OnPrepareAreaWithSave();
            yield return null;
        }

        IEnumerator BuyWithDiamond()
        {
            int diamond = ScoreSignals.Instance.onGetDiamond();
            WaitForSeconds timer = new WaitForSeconds(0.05f);
            while (diamond > _buildData.AreaCost)
            {
                AreaCostDown();
                ScoreSignals.Instance.onDiamondDown?.Invoke(1);
                yield return timer;
            }

            OnPrepareAreaWithSave();
            yield return null;
        }


        private void OnPrepareAreaWithSave()
        {
            IdleSignals.Instance.onSetAreaData?.Invoke(_areaId, _areaData);
        }
    }
}