using System.Collections.Generic;
using Abstract;
using Data.UnityObject;
using ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class IdleManager : MonoBehaviour, ISavable
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject BaseHolder;

        #endregion

        #region Private Variables

        [ShowInInspector] private Dictionary<int, AreaData> _areaDictionary = new Dictionary<int, AreaData>();
        private int _cityLevel;
        private CD_IdleData _cdIdleData;
        private int _completedArea;
        private bool _levelIsPlayable;
        private int _score;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }


        private void GetReferences()
        {
            _cdIdleData = GetIdleData();
        }

        private CD_IdleData GetIdleData()
        {
            return Resources.Load<CD_IdleData>("Data/CD_IdleData");
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            IdleGameSignals.Instance.onAreaComplete += OnAreaComplete;
            IdleGameSignals.Instance.onSetAreaData += OnSetAreaData;
            IdleGameSignals.Instance.onGetAreaData += OnGetAreaData;
            IdleGameSignals.Instance.onCityComplete += OnCityComplete;

            // LevelSignals.Instance.onNextLevel += OnNextLevel;

            SaveSignals.Instance.onGetSaveIdleData += OnGetIdleDatas;

            //     ScoreSignals.Instance.onGetIdleScore += OnGetIdleScore;
            //     ScoreSignals.Instance.onSetIdleScore += OnSetIdleScore;
        }

        private void UnSubscribeEvent()
        {
            IdleGameSignals.Instance.onAreaComplete -= OnAreaComplete;
            IdleGameSignals.Instance.onSetAreaData -= OnSetAreaData;
            IdleGameSignals.Instance.onGetAreaData -= OnGetAreaData;
            IdleGameSignals.Instance.onCityComplete -= OnCityComplete;

            // LevelSignals.Instance.onNextLevel -= OnNextLevel;

            SaveSignals.Instance.onGetSaveIdleData -= OnGetIdleDatas;

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

        private IdleDataParams OnGetIdleDatas()
        {
            return new IdleDataParams
            {
                AreaDictionary = _areaDictionary,
                CityLevel = _cityLevel,
            };
        }

        private int OnGetIdleScore()
        {
            return _score;
        }


        private void OnAreaComplete()
        {
            IdleGameSignals.Instance.onPrepareAreaWithSave?.Invoke();
        }

        private void OnInitializeLevel()
        {
            Instantiate(
                Resources.Load<GameObject>($"Prefabs/BasePrefabs/Base {_cityLevel % _cdIdleData.DataList.Count}"),
                BaseHolder.transform);
        }

        private AreaData OnGetAreaData(int id)
        {
            Debug.Log(id);
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
            if (_completedArea ==
                _cdIdleData.DataList[_cityLevel]
                    .BuildCount)
            {
                _cityLevel++;
                _levelIsPlayable = true;
                _completedArea = 0;
            }
        }

        private void OnNextLevel()
        {
            if (_levelIsPlayable)
            {
                _areaDictionary.Clear();
                Destroy(BaseHolder.transform.GetChild(0)
                    .gameObject);
                OnInitializeLevel();
                IdleGameSignals.Instance.onCityComplete?.Invoke();
                _levelIsPlayable = false;
            }
            else
            {
                IdleGameSignals.Instance.onPrepareAreaWithSave?.Invoke();
            }
        }


        public void LoadData()
        {
            IdleDataParams _data = SaveSignals.Instance.onLoadIdleData();

            _areaDictionary = _data.AreaDictionary;
            _cityLevel = _data.CityLevel;
        }

        public void SaveData()
        {
            SaveSignals.Instance.onSaveIdleData?.Invoke();
        }
    }
}