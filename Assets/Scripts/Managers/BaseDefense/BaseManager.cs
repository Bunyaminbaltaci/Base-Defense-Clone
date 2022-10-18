using System;
using System.Collections.Generic;
using System.Linq;
using Abstract;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controller
{
    public class BaseManager : MonoBehaviour, ISavable
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private List<GameObject> enemyTargetList;
        [SerializeField] private GameObject ammoArea;
        [SerializeField] private GameObject baseEnterPoint;

        #endregion

        #region Private Variables

        private List<GameObject> _moneyTransformList;
        private Dictionary<string, bool> _turretIsAuto;
        private int _minerCount;
        private int _soldierCount;

        #endregion

        #endregion

        #region EventSubscription

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _moneyTransformList = new List<GameObject>();
            _turretIsAuto = new Dictionary<string, bool>();
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            BaseSignals.Instance.onTurretIsAuto += OnTurretIsAuto;
            BaseSignals.Instance.onTurretIsAutoSub += OnTurretIsAutoSub;

            BaseSignals.Instance.onGetMinerCount += OnGetMinerCount;
            BaseSignals.Instance.onAddMinerInMine += OnAddMinerInMine;

            BaseSignals.Instance.onGetAmmoArea += OnGetAmmoArea;

            BaseSignals.Instance.onGetEnter += OnGetEnter;
            BaseSignals.Instance.onGetBase += OnGetBase;

            BaseSignals.Instance.onGetSoldierCount += OnGetSoldierCount;

            BaseSignals.Instance.onGetEnemyTarget += OnGetEnemyTarget;

            BaseSignals.Instance.onGetHarvesterTarget += OnGetHarvesterTarget;
            BaseSignals.Instance.onAddHaversterTargetList += OnAddHaversterTargetList;
            BaseSignals.Instance.onRemoveHaversterTargetList += OnRemoveHaversterTargetList;

            SaveSignals.Instance.onGetSaveIdleData += OnGetSaveIdleData;
        }

        private void UnSubscribeEvent()
        {
            BaseSignals.Instance.onTurretIsAuto -= OnTurretIsAuto;
            BaseSignals.Instance.onTurretIsAutoSub -= OnTurretIsAutoSub;

            BaseSignals.Instance.onGetMinerCount -= OnGetMinerCount;
            BaseSignals.Instance.onAddMinerInMine -= OnAddMinerInMine;

            BaseSignals.Instance.onGetAmmoArea -= OnGetAmmoArea;

            BaseSignals.Instance.onGetSoldierCount -= OnGetSoldierCount;

            BaseSignals.Instance.onGetEnemyTarget -= OnGetEnemyTarget;

            BaseSignals.Instance.onGetEnter -= OnGetEnter;
            BaseSignals.Instance.onGetBase -= OnGetBase;

            BaseSignals.Instance.onGetHarvesterTarget -= OnGetHarvesterTarget;
            BaseSignals.Instance.onAddHaversterTargetList -= OnAddHaversterTargetList;
            BaseSignals.Instance.onRemoveHaversterTargetList -= OnRemoveHaversterTargetList;


            SaveSignals.Instance.onGetSaveIdleData -= OnGetSaveIdleData;
        }

        private void OnTurretIsAutoSub(string name, bool isAuto)
        {
            if (!_turretIsAuto.ContainsKey(name))
            {
                _turretIsAuto.Add(name,isAuto);
            
            }
            else if (_turretIsAuto.ContainsKey(name) && isAuto)
            {
                _turretIsAuto[name] = isAuto;
            }
         
            SaveData();
          
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

        private bool OnTurretIsAuto(string turret)
        {
            
            if (_turretIsAuto.ContainsKey(turret))
            {
                return _turretIsAuto[turret];
            }

            _turretIsAuto.Add(name, false);

            return false;
        }

        private void OnAddMinerInMine(GameObject obj)
        {
            _minerCount++;
            SaveData();
        }

        private GameObject OnGetBase() => gameObject;


        private void OnAddHaversterTargetList(GameObject money)
        {
            _moneyTransformList.Add(money);
        }

        private void OnRemoveHaversterTargetList(GameObject money)
        {
            _moneyTransformList.Remove(money);
            _moneyTransformList.TrimExcess();
        }

        private GameObject OnGetHarvesterTarget()
        {
            if (_moneyTransformList.Count > 0)
            {
                return _moneyTransformList[Random.Range(0, _moneyTransformList.Count)];
            }

            return null;
        }


        private BaseDataParams OnGetSaveIdleData() => new BaseDataParams
        {
            MinerCount = _minerCount,
            SoldierCount = _soldierCount,
            TurretIsAuto = _turretIsAuto
        };

        private GameObject OnGetAmmoArea() => ammoArea;
        private GameObject OnGetEnemyTarget() => enemyTargetList[Random.Range(0, enemyTargetList.Count)];
        private GameObject OnGetEnter() => baseEnterPoint;
        private int OnGetSoldierCount() => _soldierCount;
        private int OnGetMinerCount() => _minerCount;


        public void LoadData()
        {
            var value = SaveSignals.Instance.onLoadIdleData();
            _minerCount = value.MinerCount;
            _soldierCount = value.SoldierCount;
            _turretIsAuto = value.TurretIsAuto;
        }

        public void SaveData()
        {
            SaveSignals.Instance.onSaveIdleData?.Invoke();
        }
    }
}