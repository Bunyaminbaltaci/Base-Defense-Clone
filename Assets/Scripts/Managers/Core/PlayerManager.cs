using System;
using System.Collections.Generic;
using Controllers;
using Data;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers.Core
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private StackController stackController;

        #endregion

        #region Private Variables

        private PlayerData _data;
    
        private List<GameObject> _hostageList;

        #endregion

        #endregion

        #region Event Subscription

        private void Awake()
        {
            GetReferences();
            SendPlayerDataToControllers();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onGetHostageTarget += OnGetHostageTarget;
            IdleSignals.Instance.onGetAmmoInStack += OnGetAmmoInStack;
        }

       


        private void Unsubscribe()
        {
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            CoreGameSignals.Instance.onGetHostageTarget -= OnGetHostageTarget;
            IdleSignals.Instance.onGetAmmoInStack -= OnGetAmmoInStack;
        }


        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void GetReferences()
        {
            _data = GetPlayerData();
          
            _hostageList = new List<GameObject>();
        }

        private PlayerData GetPlayerData()=>Resources.Load<CD_Player>("Data/CD_Player").Data;
     
      


        public void AddStack(GameObject obj)
        {
            stackController.AddStack(obj);
        }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(_data);
            stackController.SetStackData(_data.BulletBoxStackData,_data.MoneyBoxStackData);
        }


        private void OnInputDragged(InputParams inputParam)
        {
            playerMovementController.UpdateInputValue(inputParam);
        }


        public void PlayAnim(PlayerAnimationStates playerAnimationStates, float isTrue)
        {
            playerAnimationController.PlayAnim(playerAnimationStates, isTrue);
        }

        public void ChageStackState(StackType type)
        {
            stackController.SetStackType(type);
        }

        //ToDo:Controllera Ayır Bu managerın görevi değil
        private GameObject OnGetHostageTarget(GameObject hostage)
        {
            if (_hostageList.Count == 0)
            {
                _hostageList.Add(hostage);
                return transform.gameObject;
            }

            _hostageList.Add(hostage);
            return _hostageList[_hostageList.Count - 2];
        }

        public void HostageAddMine()
        {
            while (IdleSignals.Instance.onGetMinerCapacity() > 0 && _hostageList.Count > 0)
            {
                var lastHostage = _hostageList.Count - 1;
                var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Miner);
                obj.transform.position = _hostageList[lastHostage].transform.position;
                obj.transform.rotation = _hostageList[lastHostage].transform.rotation;
                PoolSignals.Instance.onSendPool(_hostageList[lastHostage], PoolType.Hostage);
                obj.SetActive(true);
                IdleSignals.Instance.onAddMinerInMine?.Invoke(obj);
                _hostageList.RemoveAt(lastHostage);
                _hostageList.TrimExcess();
            }
        }
        //_____________________________________________________________________________



        public void StartCollectStack()
        {
          stackController.StartCollect();
            
        }
        private GameObject OnGetAmmoInStack(GameObject arg)
        {
            if (arg == transform.GetChild(1).gameObject)
                return stackController.SendBulletBox();
            return null;
        }
       
    }
}