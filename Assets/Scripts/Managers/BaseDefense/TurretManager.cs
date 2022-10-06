using System;
using System.Collections.Generic;
using Commands.Turret;
using Controller.Other;
using Controller.Turret;
using Controllers;
using Data;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Manager
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GameObject PlayerHandle;
        public TurretState TurretType = TurretState.None;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject stackHolder;
        [SerializeField] private TurretMovementController turretMovementController;
        [SerializeField] private TurretAttackController turretAttackController;

        #endregion

        #region Private Variables

        private List<GameObject> _bulletBoxList;
        private TurretData _data;
        private TurretStackSetPosCommand _turretStackSetPosCommand;
        private TurretBulletBoxAddCommand _turretBulletBoxAddCommand;
        private float _directY;
        private float _directZ;
        private float _directX;

        #endregion

        #endregion

        private void Awake()
        {
            _bulletBoxList = new List<GameObject>();
            _data = GetTurretData();
            GetReferences();
        }

        private void GetReferences()
        {
            var manager = this;
            _turretStackSetPosCommand = new TurretStackSetPosCommand(ref _bulletBoxList, ref _data);
            _turretBulletBoxAddCommand =
                new TurretBulletBoxAddCommand(ref _bulletBoxList, ref _data, ref stackHolder, ref manager);
        }


        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
            BaseSignals.Instance.onHoldTurretData?.Invoke(stackHolder.transform.parent.gameObject, new TurretParams
            {
                StackLimit = _data.BulletBoxStackData.StackLimit,
                StackZone = stackHolder
            });
        }

        private void Subscribe()
        {
            BaseSignals.Instance.onSendAmmoInStack += OnSendAmmoInStack;
            BaseSignals.Instance.onPlayerInTurret += OnPlayerInTurret;
            BaseSignals.Instance.onPlayerOutTurret+= OnPlayerOutTurret;
            BaseSignals.Instance.onGetTurretDamage += OnGetTurretDamage;
            BaseSignals.Instance.onRemoveInDamageableStack += OnRemoveInDamageableStack;
            
            InputSignals.Instance.onInputDragged += OnInputDragged;
        }


        private void Unsubscribe()
        {
            BaseSignals.Instance.onSendAmmoInStack -= OnSendAmmoInStack;
            BaseSignals.Instance.onPlayerInTurret -= OnPlayerInTurret;
            BaseSignals.Instance.onPlayerOutTurret-= OnPlayerOutTurret;
            BaseSignals.Instance.onGetTurretDamage -= OnGetTurretDamage;
            BaseSignals.Instance.onRemoveInDamageableStack -= OnRemoveInDamageableStack;
            
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            
        }

        private void OnRemoveInDamageableStack(GameObject enemy)
        {
         turretAttackController.RemoveFromList(enemy); 
        }

        private int OnGetTurretDamage() => _data.Damage;
        


        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion


        private void Start()
        {
            BaseSignals.Instance.onHoldTurretData?.Invoke(stackHolder.transform.parent.gameObject, new TurretParams
            {
                StackLimit = _data.BulletBoxStackData.StackLimit,
                StackZone = stackHolder
            });
            turretAttackController.SetFireRate(_data.FireRate);
        }


        private TurretData GetTurretData() => Resources.Load<CD_TurretData>("Data/CD_TurretData").Data;


        private void OnSendAmmoInStack(GameObject target, GameObject bulletBox)
        {
            if (target == stackHolder.transform.parent.gameObject)
            {
                _turretBulletBoxAddCommand.Execute(bulletBox);
            }
        }
        private void OnPlayerOutTurret(GameObject IsCheck)
        {
            if (TurretType==TurretState.PlayerIn && IsCheck==gameObject)
            {
                TurretType = TurretState.None;
            }
        }

        private void OnPlayerInTurret(GameObject target)
        {
            if (target==gameObject)
            {
                TurretType = TurretState.PlayerIn;
            }
        }
        
        public void SetObjPosition(GameObject bulletBox)
        {
            _turretStackSetPosCommand.Execute(bulletBox);
        }
        private void OnInputDragged(InputParams data)
        {
           turretMovementController.SetTurnValue(data);
        }
    }
}