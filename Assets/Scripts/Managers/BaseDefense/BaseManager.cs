using System.Collections.Generic;
using Abstract;
using Data;
using ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour, ISavable
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject BaseHolder;

        #endregion

        #region Private Variables

        [ShowInInspector] private Dictionary<int, AreaData> _areaDictionary = new Dictionary<int, AreaData>();
        private int _baseLevel;
        private CD_BaseData _cdBaseData;
        private int _completedArea;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }


        private void GetReferences()
        {
            _cdBaseData = GetBaseData();
        }

        private CD_BaseData GetBaseData()
        {
            return Resources.Load<CD_BaseData>("Data/CD_BaseData");
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            BaseSignals.Instance.onAreaComplete += OnAreaComplete;
            BaseSignals.Instance.onSetAreaData += OnSetAreaData;
            BaseSignals.Instance.onGetAreaData += OnGetAreaData;
            BaseSignals.Instance.onBaseComplete += OnCityComplete;

            // LevelSignals.Instance.onNextLevel += OnNextLevel;

            SaveSignals.Instance.onGetBaseData += OnGetBaseDatas;

            //     ScoreSignals.Instance.onGetIdleScore += OnGetIdleScore;
            //     ScoreSignals.Instance.onSetIdleScore += OnSetIdleScore;
        }

        private void UnSubscribeEvent()
        {
            BaseSignals.Instance.onAreaComplete -= OnAreaComplete;
            BaseSignals.Instance.onSetAreaData -= OnSetAreaData;
            BaseSignals.Instance.onGetAreaData -= OnGetAreaData;
            BaseSignals.Instance.onBaseComplete -= OnCityComplete;

            // LevelSignals.Instance.onNextLevel -= OnNextLevel;

            SaveSignals.Instance.onGetBaseData -= OnGetBaseDatas;

            // ScoreSignals.Instance.onGetIdleScore -= OnGetIdleScore;
            // ScoreSignals.Instance.onSetIdleScore -= OnSetIdleScore;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private void Start()
        {
            LoadData();
            OnInitializeLevel();
        }

        private BaseDataParams OnGetBaseDatas()
        {
            return new BaseDataParams
            {
                AreaDictionary = _areaDictionary,
                BaseLevel = _baseLevel,
            };
        }


        private void OnAreaComplete()
        {
            BaseSignals.Instance.onPrepareAreaWithSave?.Invoke();
        }

        private void OnInitializeLevel()
        {
            Instantiate(
                Resources.Load<GameObject>($"Prefabs/BasePrefabs/Base {_baseLevel % _cdBaseData.DataList.Count}"),
                BaseHolder.transform);
        }

        private AreaData OnGetAreaData(int id)
        {
            return _areaDictionary.ContainsKey(id)
                ? _areaDictionary[id]
                : new AreaData();
        }

        private void OnSetAreaData(int id,
            AreaData araeData)
        {
            if (_areaDictionary.ContainsKey(id))
                _areaDictionary[id] = araeData;
            else
                _areaDictionary.Add(id,
                    araeData);
            SaveData();
        }

        private void OnCityComplete()
        {
            _baseLevel++;
        }

        private void OnNextLevel()
        {
            _areaDictionary.Clear();
            Destroy(BaseHolder.transform.GetChild(0)
                .gameObject);
            OnInitializeLevel();
            BaseSignals.Instance.onBaseComplete?.Invoke();
        }


        public void LoadData()
        {
            BaseDataParams _data = SaveSignals.Instance.onLoadBaseData();

            _areaDictionary = _data.AreaDictionary;
            _baseLevel = _data.BaseLevel;
        }

        public void SaveData()
        {
            SaveSignals.Instance.onSaveBaseData?.Invoke();
            SaveSignals.Instance.onSaveScoreData?.Invoke();
        }
    }
}