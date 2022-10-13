using System;
using System.Collections;
using System.Collections.Generic;
using Controller.Other;
using Controllers;
using Data;
using DG.Tweening;
using Enums;
using Keys;
using Manager;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers.Core
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GameObject CurrentParent;
        public GameObject Target;

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

        private void Awake()
        {
            GetReferences();
            SendPlayerDataToControllers();
        }

        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onGetHostageTarget += OnGetHostageTarget;
        }

        private void Unsubscribe()
        {
            InputSignals.Instance.onInputDragged -= OnInputDragged;
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

        private void Start()
        {
            CurrentParent = transform.parent.gameObject;
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(transform);
        }

        public void AddMoneyOnStack(GameObject obj)
        {
            if (stackController.StackList.Count < _data.MoneyBoxStackData.StackLimit)
            {
                obj.GetComponent<MoneyController>().isTaked();
                stackController.AddStack(obj);
            }
        }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(_data);
            stackController.SetStackData(_data.BulletBoxStackData, _data.MoneyBoxStackData);
        }

        private void OnInputDragged(InputParams inputParam)
        {
            playerMovementController.UpdateInputValue(inputParam);
        }

        public void PlayAnim(PlayerAnimationStates playerAnimationStates, float Value)
        {
            playerAnimationController.PlayAnim(playerAnimationStates, Value);
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
            while (BaseSignals.Instance.onGetMinerCapacity() > 0 && _hostageList.Count > 0)
            {
                var lastHostage = _hostageList.Count - 1;
                var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Miner);
                obj.transform.position = _hostageList[lastHostage].transform.position;
                obj.transform.rotation = _hostageList[lastHostage].transform.rotation;
                PoolSignals.Instance.onSendPool(_hostageList[lastHostage], PoolType.Hostage);
                obj.SetActive(true);
                BaseSignals.Instance.onAddMinerInMine?.Invoke(obj);
                _hostageList.RemoveAt(lastHostage);
                _hostageList.TrimExcess();
            }
        }
        //_____________________________________________________________________________

        public void StartCollectStack()
        {
            stackController.StartCollect();
        }

        public void ChangeMovement(PlayerMovementState state)
        {
            playerMovementController.ChangeState(state);
        }

        public IEnumerator StartBulletBoxSend(GameObject target)
        {
            var waiter = new WaitForSeconds(0.2f);
            while (stackController.StackList.Count > 0)
            {
                if (BaseSignals.Instance.onGetTurretLimit(target) > 0)
                    BaseSignals.Instance.onSendAmmoInStack?.Invoke(target, stackController.SendBulletBox());

                yield return waiter;
            }
        }

        public void InTurret(GameObject other)
        {
            var newparent = other.GetComponent<TurretManager>().PlayerHandle.transform;
            transform.parent = newparent;
            transform.DOLocalMove(new Vector3(0, transform.localPosition.y, 0), .5f);
            transform.DOLocalRotate(Vector3.zero, 0.5f);
            ChangeMovement(PlayerMovementState.Turret);
            BaseSignals.Instance.onPlayerInTurret.Invoke(other.gameObject);
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Turret);
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(newparent.transform.parent);
        }

        public void OutTurret(GameObject other)
        {
            BaseSignals.Instance.onPlayerOutTurret?.Invoke(other.gameObject);
            transform.parent = CurrentParent.transform;
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(transform);
        }

        public IEnumerator TakeBulletBox()
        {
            var waiter = new WaitForSeconds(0.2f);
            while (stackController.StackList.Count < _data.BulletBoxStackData.StackLimit)
            {
                var obj = BaseSignals.Instance.onGetBulletBox?.Invoke();
                if (obj == null)
                    break;
                stackController.AddStack(obj);
                yield return waiter;
            }
        }
    }
}