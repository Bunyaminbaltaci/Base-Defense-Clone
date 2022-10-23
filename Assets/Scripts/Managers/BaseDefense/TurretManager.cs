using System.Collections.Generic;
using Commands.Turret;
using Controller;
using Controller.Turret;
using Data;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Controller
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GameObject PlayerHandle;
        public TurretState TurretType = TurretState.None;
        public Coroutine AttackCoroutine;
        public Coroutine LockCoroutine;
        public GameObject Target;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject stackHolder;
        [SerializeField] private GameObject turretOperator;
        [SerializeField] private TurretMovementController turretMovementController;
        [SerializeField] private TurretAttackController turretAttackController;
        [SerializeField] private TurretAutoModeController turretAutoModeController;

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
            _data = GetTurretData();
            GetReferences();
        }

        private void GetReferences()
        {
            var manager = this;
            _bulletBoxList = new List<GameObject>();
            _turretStackSetPosCommand = new TurretStackSetPosCommand(ref _bulletBoxList, ref _data);
            _turretBulletBoxAddCommand =
                new TurretBulletBoxAddCommand(ref _bulletBoxList, ref _data, ref stackHolder, ref manager);
        }


        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
            BaseSignals.Instance.onHoldTurretData?.Invoke(stackHolder.transform.parent.gameObject, new TurretStackParams
            {
                StackLimit = _data.BulletBoxStackData.StackLimit,
                StackZone = stackHolder
            });
            BaseSignals.Instance.onTurretIsAutoSub?.Invoke(transform.parent.name, false);
        }

        private void Subscribe()
        {
            BaseSignals.Instance.onSendAmmoInStack += OnSendAmmoInStack;
            BaseSignals.Instance.onPlayerInTurret += OnPlayerInTurret;
            BaseSignals.Instance.onPlayerOutTurret += OnPlayerOutTurret;
            BaseSignals.Instance.onGetTurretDamage += OnGetTurretDamage;
            BaseSignals.Instance.onRemoveInDamageableStack += OnRemoveInDamageableStack;

            InputSignals.Instance.onInputDragged += OnInputDragged;
        }


        private void Unsubscribe()
        {
            BaseSignals.Instance.onSendAmmoInStack -= OnSendAmmoInStack;
            BaseSignals.Instance.onPlayerInTurret -= OnPlayerInTurret;
            BaseSignals.Instance.onPlayerOutTurret -= OnPlayerOutTurret;
            BaseSignals.Instance.onGetTurretDamage -= OnGetTurretDamage;
            BaseSignals.Instance.onRemoveInDamageableStack -= OnRemoveInDamageableStack;

            InputSignals.Instance.onInputDragged -= OnInputDragged;
        }


        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion


        private void Start()
        {
            Prepare();
        }

        private void Prepare()
        {
            BaseSignals.Instance.onHoldTurretData?.Invoke(stackHolder.transform.parent.gameObject, new TurretStackParams
            {
                StackLimit = _data.BulletBoxStackData.StackLimit,
           
            });
            turretAutoModeController.AutoModeCost.text = _data.AutoModeCost.ToString();
            if (BaseSignals.Instance.onTurretIsAuto(transform.parent.name))
            {
                SwitchState(TurretState.AutoMode);
            }


            turretAttackController.SetFireRate(_data.FireRate);
        }

        private void OnRemoveInDamageableStack(GameObject enemy)
        {
            turretAttackController.RemoveFromList(enemy);
        }

        private int OnGetTurretDamage()
        {
            return _data.Damage;
        }

        private TurretData GetTurretData()
        {
            return Resources.Load<CD_TurretData>("Data/CD_TurretData").Data;
        }


        private void OnSendAmmoInStack(GameObject target, GameObject bulletBox)
        {
            if (target == stackHolder.transform.parent.gameObject) _turretBulletBoxAddCommand.Execute(bulletBox);
        }

        private void OnPlayerOutTurret(GameObject IsCheck)
        {
            if (TurretType == TurretState.PlayerIn && IsCheck == gameObject)
            {
                SwitchState(TurretState.None);

                if (AttackCoroutine != null)
                {
                    StopCoroutine(AttackCoroutine);
                    AttackCoroutine = null;
                }
            }
        }

        private void OnPlayerInTurret(GameObject target)
        {
            if (target == gameObject)
            {
                SwitchState(TurretState.PlayerIn);
                AttackCoroutine = StartCoroutine(turretAttackController.Attack());
            }
        }

        public bool CheckStack()
        {
            return _bulletBoxList.Count > 0;
        }

        public void DeleteBulletBox()
        {
            PoolSignals.Instance.onSendPool?.Invoke(_bulletBoxList[_bulletBoxList.Count - 1], PoolType.BulletBox);
            _bulletBoxList.Remove(_bulletBoxList[_bulletBoxList.Count - 1]);
            _bulletBoxList.TrimExcess();
            BaseSignals.Instance.onHoldTurretData?.Invoke(stackHolder.transform.parent.gameObject, new TurretStackParams
            {
                StackLimit = _data.BulletBoxStackData.StackLimit - _bulletBoxList.Count,
            });
        }

        private void SwitchState(TurretState state)
        {
            switch (state)
            {
                case TurretState.PlayerIn:
                    TurretType = TurretState.PlayerIn;
                    break;
                case TurretState.AutoMode:
                    turretOperator.SetActive(true);
                    SetAutoMode();
                    break;
                case TurretState.None:
                    TurretType = TurretState.None;
                    break;
            }
        }

        private void SetAutoMode()
        {
            TurretType = TurretState.AutoMode;
            turretAutoModeController.IsComplete();
            AttackCoroutine = null;
            AttackCoroutine = StartCoroutine(turretAttackController.Attack());
            LockTarget();
        }

        public void SetObjPosition(GameObject bulletBox)
        {
            _turretStackSetPosCommand.Execute(bulletBox);
        }

        public void GetTarget()
        {
            if (turretAttackController.Damageables.Count > 0)
            {
                Target = turretAttackController.Damageables[0];
            }
        }

        public void IsBuyAutoMode()
        {
            BaseSignals.Instance.onTurretIsAutoSub?.Invoke(transform.parent.name, true);
            SwitchState(TurretState.AutoMode);
        }

        public void LockTarget()
        {
            LockCoroutine = StartCoroutine(turretMovementController.LockTarget());
        }

        private void OnInputDragged(InputParams data)
        {
            turretMovementController.SetTurnValue(data);
        }
    }
}