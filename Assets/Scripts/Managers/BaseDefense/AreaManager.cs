using System;
using Data;
using ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Managers
{
    public class AreaManager : MonoBehaviour
    {
        #region Self Variables

        [Header("Data")]

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private BuildType buildType;

        [SerializeField] private TextMeshPro buildCost;
        [SerializeField] private GameObject buildCostArea;
        [SerializeField] private GameObject fence;
        [SerializeField] private GameObject area;
        [SerializeField] private int areaId;

        #endregion

        #region Private Variables

        private BuildData _buildData;
        private GameObject _areaCheck;
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


        private void Start()
        {
            BaseSignals.Instance.onRefreshAreaData?.Invoke();
        }

        private void GetReferences()
        {
            _buildData = GetData();
            buildCost.text = _buildData.AreaCost.ToString();
        }


        private BuildData GetData()
        {
            return Resources.Load<CD_BuildData>("Data/CD_BuildData").BuildData[(int)buildType];
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onAreaCostDown += OnAreaCostDown;
            BaseSignals.Instance.onCheckArea += OnCheckArea;
            BaseSignals.Instance.onRefreshAreaData += OnRefreshAreaData;
            BaseSignals.Instance.onPrepareAreaWithSave += OnPrepareAreaWithSave;
        }

        private void UnSubscribeEvents()
        {
            BaseSignals.Instance.onAreaCostDown -= OnAreaCostDown;
            BaseSignals.Instance.onCheckArea -= OnCheckArea;
            BaseSignals.Instance.onRefreshAreaData -= OnRefreshAreaData;
            BaseSignals.Instance.onPrepareAreaWithSave -= OnPrepareAreaWithSave;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        private void OnRefreshAreaData()
        {
            _areaData = (AreaData)BaseSignals.Instance.onGetAreaData?.Invoke(areaId);
            CostAreaVisible();
            SetAreaTexts();
        }

        private void OnAreaCostDown()
        {
            if (_areaCheck != gameObject) return;
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
                BaseSignals.Instance.onAreaComplete?.Invoke();
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

        private void OnCheckArea(GameObject Check)
        {
            _areaCheck = Check;
        }

        private void OnPrepareAreaWithSave()
        {
            BaseSignals.Instance.onSetAreaData?.Invoke(areaId, _areaData);
        }
    }
}