using System.Collections.Generic;
using Abstract;
using Data;
using ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controller
{
    public class IdleManager : MonoBehaviour, ISavable
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject BaseHolder;
       

        #endregion

        #region Private Variables

        [ShowInInspector] private Dictionary<string, AreaData> _areaDictionary = new Dictionary<string, AreaData>();
        private int _baseLevel;
        private CD_BaseData _cdBaseData;
        private int _completedArea;
        private int _minerCount;
        private int _soldierCount;
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
            IdleSignals.Instance.onAreaComplete += OnAreaComplete;
            IdleSignals.Instance.onSetAreaData += OnSetAreaData;
            IdleSignals.Instance.onGetAreaData += OnGetAreaData;
            IdleSignals.Instance.onBaseComplete += OnCityComplete;
            IdleSignals.Instance.onGetBaseLevel += OnGetBaseLevel;
            
            // LevelSignals.Instance.onNextLevel += OnNextLevel;
            SaveSignals.Instance.onGetBaseData += OnGetBaseDatas;
            
        }

        private void UnSubscribeEvent()
        {
            IdleSignals.Instance.onAreaComplete -= OnAreaComplete;
            IdleSignals.Instance.onSetAreaData -= OnSetAreaData;
            IdleSignals.Instance.onGetAreaData -= OnGetAreaData;
            IdleSignals.Instance.onBaseComplete -= OnCityComplete;
            IdleSignals.Instance.onGetBaseLevel -= OnGetBaseLevel;
            // LevelSignals.Instance.onNextLevel -= OnNextLevel;
            
            SaveSignals.Instance.onGetBaseData -= OnGetBaseDatas;
            
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

        private IdleDataParams OnGetBaseDatas()
        {
            return new IdleDataParams
            {
                AreaDictionary = _areaDictionary,
                BaseLevel = _baseLevel,
            };
        }

        private int OnGetBaseLevel()
        {
            return _baseLevel;
        }


        private void OnAreaComplete()
        {
            IdleSignals.Instance.onPrepareAreaWithSave?.Invoke();
        }

        private void OnInitializeLevel()
        {
            Instantiate(
                Resources.Load<GameObject>($"Prefabs/BasePrefabs/Base {_baseLevel % _cdBaseData.DataList.Count}"),
                BaseHolder.transform);
        }

        private AreaData OnGetAreaData(string id)
        {
            return _areaDictionary.ContainsKey(id)
                ? _areaDictionary[id]
                : new AreaData();
        }

        private void OnSetAreaData(string id,
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
            IdleSignals.Instance.onBaseComplete?.Invoke();
        }


        public void LoadData()
        {
            IdleDataParams _data = SaveSignals.Instance.onLoadBaseData();

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