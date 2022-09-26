using System.Collections.Generic;
using Abstract;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class BaseManager : MonoBehaviour, ISavable
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private List<GameObject> turretStackList;
        [FormerlySerializedAs("TargetList")] [SerializeField] private List<GameObject> targetList;
        [SerializeField] private GameObject ammoArea;
        [SerializeField] private GameObject baseExitPoint;
        #endregion

        #region Private Variables

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
            BaseSignals.Instance.onGetTarget += OnGetTarget;


            SaveSignals.Instance.onGetSaveIdleData += OnGetSaveIdleData;
        }

       

        private void UnSubscribeEvent()
        {
            BaseSignals.Instance.onGetMinerCount -= OnGetMinerCount;
            BaseSignals.Instance.onGetSoldierCount -= OnGetSoldierCount;
            BaseSignals.Instance.onAddMinerInMine -= OnAddMinerInMine;
            BaseSignals.Instance.onGetTurretStack -= OnGetTurretStack;
            BaseSignals.Instance.onGetAmmoArea -= OnGetAmmoArea;
            BaseSignals.Instance.onGetTarget -= OnGetTarget;



            SaveSignals.Instance.onGetSaveIdleData -= OnGetSaveIdleData;
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

        private GameObject OnGetTurretStack() => turretStackList[Random.Range(0,turretStackList.Count)];
        private int OnGetSoldierCount() => _soldierCount;
        private int OnGetMinerCount() => _minerCount;
        private IdleDataParams OnGetSaveIdleData()=>new IdleDataParams
        {
            MinerCount = _minerCount,
            SoldierCount = _soldierCount
        };
        private GameObject OnGetAmmoArea() => ammoArea;
       
        private GameObject OnGetTarget() => targetList[Random.Range(0, targetList.Count)];
       
        
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