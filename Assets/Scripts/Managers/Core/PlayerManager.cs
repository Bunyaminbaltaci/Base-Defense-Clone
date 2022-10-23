using System.Collections;
using Abstract;
using Controller;
using Data;
using DG.Tweening;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers.Core
{
    public class PlayerManager : MonoBehaviour, IDamageable
    {
        #region Self Variables

        #region Public Variables

        private int _health = 100;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                playerHealthController.SetHealthText(value);
               
            }
        }


        public GameObject CurrentParent;
        public GameObject Target;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private StackController stackController;
        [SerializeField] private PlayerAttackController playerAttackController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private PlayerHealthController playerHealthController;

        #endregion

        #region Private Variables

        private HostageCollectorController _hostageCollectorController;
        private PlayerData _data;
        private LayerType _side;
        private bool _isDead=false;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            SendPlayerDataToControllers();
        }

        private void GetReferences()
        {
            var manager = this;
            _hostageCollectorController = new HostageCollectorController(ref manager);
            _data = GetPlayerData();
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            InputSignals.Instance.onInputDragged += OnInputDragged;
            BaseSignals.Instance.onGunDataSet += OnGunDataSet;
            BaseSignals.Instance.onRemoveInDamageableStack += OnRemoveInDamageableStack;
            CoreGameSignals.Instance.onGetHostageTarget += _hostageCollectorController.GetHostageTarget;
        }

      

        private void Unsubscribe()
        {
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            BaseSignals.Instance.onGunDataSet -= OnGunDataSet;
            BaseSignals.Instance.onRemoveInDamageableStack += OnRemoveInDamageableStack;
            CoreGameSignals.Instance.onGetHostageTarget -= _hostageCollectorController.GetHostageTarget;
        }

        private void OnDisable()
        {
            Unsubscribe();
            StopAllCoroutines();
        }

        #endregion

        private void Start()
        {
            CurrentParent = transform.parent.gameObject;
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(transform);
            OnGunDataSet();
        }

        private void OnInputDragged(InputParams value)
        {
            if (_isDead) value=new InputParams();
            playerMovementController.UpdateInputValue(value);
        }
        public void ChangeLayer()
        {
            switch (_side)
            {
                case LayerType.Default:
                    _side = LayerType.BattleArea;
                    break;
                case LayerType.BattleArea:
                    _side = LayerType.Default;
                    break;
            }
            Target = null;
            playerHealthController.ChangedLayer(_side);
            playerPhysicsController.ChangeLayer(_side);
            playerAttackController.ChangeLayer(_side);
            playerAnimationController.ChangeLayer(_side);
            stackController.SetStackType(_side);
        }


        public void AddMoneyOnStack(GameObject obj)
        {
            if (stackController.StackList.Count < _data.MoneyBoxStackData.StackLimit)
            {
                obj.GetComponent<MoneyController>().isTaked();
                stackController.AddStack(obj);
            }
        }


        public void PlayFloatAnim(PlayerAnimationStates playerAnimationStates, float value)
        {
            playerAnimationController.PlayFloatAnim(playerAnimationStates, value);
        }

        public void PlayTriggerAnim(PlayerAnimationStates playerAnimationStates)
        {
            playerAnimationController.PlayTriggerAnim(playerAnimationStates);
        }

        public void TakeBulletBox()
        {
            StartCoroutine(stackController.TakeBulletBox());
        }

        public void StartBulletBoxSend(GameObject target)
        {
            StartCoroutine(stackController.StartBulletBoxSend(target));
        }

        #region Turret

        public void InTurret(GameObject other)
        {
            var newparent = other.GetComponent<TurretManager>().PlayerHandle.transform;
            transform.parent = newparent;
            transform.DOLocalMove(new Vector3(0, transform.localPosition.y, 0.30f), .5f);
            transform.DOLocalRotate(new Vector3(0,180,0), 0.5f).SetDelay(0.2f);
            ChangeMovement(PlayerMovementState.Turret);
            BaseSignals.Instance.onPlayerInTurret.Invoke(other.gameObject);
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Turret);
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(newparent.transform.parent);
            PlayTriggerAnim(PlayerAnimationStates.Hold);
        }

        public void OutTurret(GameObject other)
        {
            BaseSignals.Instance.onPlayerOutTurret?.Invoke(other.gameObject);
            transform.parent = CurrentParent.transform;
            transform.DOLocalRotate(Vector3.zero, 0.5f);
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(transform);
        }

        #endregion

        public void ChangeMovement(PlayerMovementState state)
        {
            playerMovementController.ChangedState(state);
        }

        public void HostageAddMine()
        {
            _hostageCollectorController.HostageAddMine();
        }

        private void OnRemoveInDamageableStack(GameObject enemy)
        {
            playerAttackController.RemoveFromList(enemy);
        }

        private void OnGunDataSet()
        {
            playerAttackController.GunDataSet();
        }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(_data);
            stackController.SetStackData(_data.BulletBoxStackData, _data.MoneyBoxStackData);
        }

        private IEnumerator isDead()
        {
            WaitForSeconds waiter = new WaitForSeconds(2f);
            stackController.DropMoney();
            _hostageCollectorController.DropFollowers();
            _isDead = true;
            PlayTriggerAnim(PlayerAnimationStates.Dead);
            ChangeLayer();
            yield return waiter;
        
            PlayTriggerAnim(PlayerAnimationStates.Run); 
            _isDead = false;
            transform.position = BaseSignals.Instance.onGetEnter.Invoke().transform.position;
        }

        public void Damage(int damage)
        {
            if (_isDead==false)
            {
                Health -= damage;
                if (Health<=0)
                {
                    Health = 0;
                    StartCoroutine(isDead());
             
                }
            }
        }
    }
}