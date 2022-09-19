using System.Collections.Generic;
using Controllers;
using Data;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerStackController playerStackController;

        #endregion

        #region Private Variables

        private PlayerData _data;
        private List<GameObject> _hostageList;
        private PlayerAnimationStates _animationState;

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
            InputSignals.Instance.onInputTaken += playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased += playerMovementController.DeactiveMovement;

            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetHostageTarget += OnGetHostageTarget;
        }


        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased -= playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged -= OnInputDragged;

            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onGetHostageTarget -= OnGetHostageTarget;
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

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        public void AddStack(GameObject obj)
        {
            playerStackController.AddStack(obj);
        }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(_data.MovementData);
            playerStackController.SetStackData(_data.StackData);
        }


        private void OnInputDragged(InputParams InputParam)
        {
            playerMovementController.UpdateInputValue(InputParam);
        }


        public void PlayAnim(PlayerAnimationStates playerAnimationStates, float isTrue)
        {
            playerAnimationController.PlayAnim(playerAnimationStates, isTrue);
        }

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
           Debug.Log("Girdik");
           Debug.Log(IdleSignals.Instance.onGetMinerCapacity());
            while (IdleSignals.Instance.onGetMinerCapacity()>0 && _hostageList.Count>0 )
            {
                Debug.Log("Whileİçindeyim");
                Debug.Log(IdleSignals.Instance.onGetMinerCapacity());
                int lastHostage = _hostageList.Count - 1;
                var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Miner);
                obj.transform.position = _hostageList[lastHostage].transform.position;
                obj.transform.rotation = _hostageList[lastHostage].transform.rotation;
                PoolSignals.Instance.onSendPool(_hostageList[lastHostage],PoolType.Hostage);
                obj.SetActive(true);
                IdleSignals.Instance.onAddMinerInMine?.Invoke(obj);
                _hostageList.RemoveAt(lastHostage);
                _hostageList.TrimExcess();
                
            }
        }

        private void OnPlay()
        {
            playerMovementController.IsReadyToPlay(true);
        }

        private void OnReset()
        {
            playerMovementController.OnReset();
        }
    }
}