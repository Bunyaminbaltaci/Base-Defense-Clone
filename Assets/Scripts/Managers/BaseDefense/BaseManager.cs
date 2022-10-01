using System;
using System.Collections.Generic;
using Abstract;
using Datas.ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Manager
{
    public class BaseManager : MonoBehaviour, ISavable
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private List<GameObject> targetList;
        [SerializeField] private GameObject ammoArea;
        [SerializeField] private GameObject baseExitPoint;
        [SerializeField] private GameObject baseEnterPoint;

        #endregion

        #region Private Variables

        private Dictionary<GameObject, TurretParams> _turretDatas = new Dictionary<GameObject, TurretParams>();
        [ShowInInspector] private List<GameObject> _moneyTransformList = new List<GameObject>();
        private int _minerCount;
        private int _soldierCount;

        #endregion

        #endregion


        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            BaseSignals.Instance.onGetMinerCount += OnGetMinerCount;
            BaseSignals.Instance.onGetSoldierCount += OnGetSoldierCount;
            BaseSignals.Instance.onAddMinerInMine += OnAddMinerInMine;
            BaseSignals.Instance.onGetTurretStack += OnGetTurretStack;
            BaseSignals.Instance.onGetAmmoArea += OnGetAmmoArea;
            BaseSignals.Instance.onGetEnemyTarget += OnGetEnemyTarget;
            BaseSignals.Instance.onHoldTurretData += OnHoldTurretData;
            BaseSignals.Instance.onGetTurretLimit += OnGetTurretLimit;
            BaseSignals.Instance.onGetEnter += OnGetEnter;
            BaseSignals.Instance.onGetExit += OnGetExit;
            BaseSignals.Instance.onGetHarvesterTarget += OnGetHarvesterTarget;
            BaseSignals.Instance.onAddHaversterTargetList += OnAddHaversterTargetList;
            BaseSignals.Instance.onRemoveHaversterTargetList += OnRemoveHaversterTargetList;


            SaveSignals.Instance.onGetSaveIdleData += OnGetSaveIdleData;
        }


        private void UnSubscribeEvent()
        {
            BaseSignals.Instance.onGetMinerCount -= OnGetMinerCount;
            BaseSignals.Instance.onGetSoldierCount -= OnGetSoldierCount;
            BaseSignals.Instance.onAddMinerInMine -= OnAddMinerInMine;
            BaseSignals.Instance.onGetTurretStack -= OnGetTurretStack;
            BaseSignals.Instance.onGetAmmoArea -= OnGetAmmoArea;
            BaseSignals.Instance.onGetEnemyTarget -= OnGetEnemyTarget;
            BaseSignals.Instance.onHoldTurretData -= OnHoldTurretData;
            BaseSignals.Instance.onGetTurretLimit -= OnGetTurretLimit;
            BaseSignals.Instance.onGetEnter -= OnGetEnter;
            BaseSignals.Instance.onGetExit -= OnGetExit;
            BaseSignals.Instance.onGetHarvesterTarget -= OnGetHarvesterTarget;
            BaseSignals.Instance.onAddHaversterTargetList -= OnAddHaversterTargetList;
            BaseSignals.Instance.onRemoveHaversterTargetList -= OnRemoveHaversterTargetList;


            SaveSignals.Instance.onGetSaveIdleData -= OnGetSaveIdleData;
        }

        private void OnAddHaversterTargetList(GameObject money)
        {
            _moneyTransformList.Add(money);
        }

        private void OnRemoveHaversterTargetList(GameObject money)
        {
            _moneyTransformList.Remove(money);
            _moneyTransformList.TrimExcess();
        }


        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private void Start()
        {
            LoadData();
        }

        private void OnAddMinerInMine(GameObject obj)
        {
            _minerCount++;
            SaveData();
        }


        private GameObject OnGetTurretStack()
        {
            int limit = 0;
            GameObject obj = null;
            foreach (var Key in _turretDatas)
            {
                if (limit < _turretDatas[Key.Key].StackLimit)
                {
                    limit = _turretDatas[Key.Key].StackLimit;
                    obj = _turretDatas[Key.Key].StackZone;
                }
            }

            return obj;
        }

        private GameObject OnGetHarvesterTarget()
        {
            if (_moneyTransformList.Count > 0)
            {
                return _moneyTransformList[Random.Range(0, _moneyTransformList.Count)];
            }

            return null;
        }

        private GameObject OnGetEnter() => baseEnterPoint;
        private GameObject OnGetExit() => baseExitPoint;
        private int OnGetSoldierCount() => _soldierCount;
        private int OnGetMinerCount() => _minerCount;

        private BaseDataParams OnGetSaveIdleData() => new BaseDataParams
        {
            MinerCount = _minerCount,
            SoldierCount = _soldierCount
        };

        private GameObject OnGetAmmoArea() => ammoArea;

        private GameObject OnGetEnemyTarget() => targetList[Random.Range(0, targetList.Count)];

        private int OnGetTurretLimit(GameObject obj) => _turretDatas[obj].StackLimit;

        private void OnHoldTurretData(GameObject obj, TurretParams turretParams)
        {
            if (_turretDatas.ContainsKey(obj))
            {
                _turretDatas[obj] = new TurretParams
                {
                    StackLimit = turretParams.StackLimit,
                    StackZone = turretParams.StackZone
                };
            }
            else
            {
                _turretDatas.Add(obj, new TurretParams
                {
                    StackLimit = turretParams.StackLimit,
                    StackZone = turretParams.StackZone
                });
            }
        }


        public void LoadData()
        {
            var value = SaveSignals.Instance.onLoadIdleData();
            _minerCount = value.MinerCount;
            _soldierCount = value.SoldierCount;
        }

        public void SaveData()
        {
            SaveSignals.Instance.onSaveIdleData?.Invoke();
        }
    }
}