using System;
using System.Collections.Generic;
using Abstract;
using Data;
using Keys;
using Signals;
using UnityEngine;
using ValueObject;

namespace Manager
{
    public class IdleManager : MonoBehaviour, ISavable
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private int _minerCount;
        private int _soldierCount;

        #endregion

        #endregion


        private void Awake()
        {
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            IdleSignals.Instance.onGetMinerCount += OnGetMinerCount;
            IdleSignals.Instance.onGetSoldierCount += OnGetSoldierCount;
            IdleSignals.Instance.onAddMinerInMine += OnAddMinerInMine;
            
            
            SaveSignals.Instance.onGetSaveIdleData += OnGetSaveIdleData;
        }


        private void UnSubscribeEvent()
        {
            IdleSignals.Instance.onGetMinerCount -= OnGetMinerCount;
            IdleSignals.Instance.onGetSoldierCount -= OnGetSoldierCount;
            IdleSignals.Instance.onAddMinerInMine -= OnAddMinerInMine;
            
            

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
        private int OnGetSoldierCount() => _soldierCount;
        private int OnGetMinerCount() => _minerCount;

        private IdleDataParams OnGetSaveIdleData()
        {
            return new IdleDataParams
            {
                MinerCount = _minerCount,
                SoldierCount = _soldierCount
            };
        }
        

        public void LoadData()
        {
            IdleDataParams value = SaveSignals.Instance.onLoadIdleData();
            _minerCount = value.MinerCount;
            _soldierCount = value.SoldierCount;
        }

        public void SaveData()
        {
            SaveSignals.Instance.onSaveIdleData?.Invoke();
        }
    }
}