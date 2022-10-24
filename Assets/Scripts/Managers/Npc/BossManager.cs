using System;
using System.Collections;
using Abstract;
using Controller.Npc.Boss;
using Enums.Npc;
using States.Npc.BossStates;
using UnityEngine;

namespace Controller
{
    public class BossManager : MonoBehaviour, IDamageable
    {
        #region Self Variables

        #region Public Variables

        public IStateMachine CurrentState;
        public GameObject Target;


        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                bossHealthController.SetHealthText(value);
            }
        }

        #endregion

        #region Serialized Variables

        [SerializeField] private BossHealthController bossHealthController;
        [SerializeField] private BossAnimationController animationController;
        [SerializeField] private BossAttackController bossAttackController;
     

        #endregion

        #region Private Variables

        private BossAttackState _bossAttackState;
        private BossDeadState _bossDeadState;
        private BossWaitTarget _bossWaitTarget;
        private int _health = 1000;
        private bool IsDead = false;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            var manager = this;
            _bossAttackState = new BossAttackState(ref manager);
            _bossDeadState = new BossDeadState(ref manager);
            _bossWaitTarget = new BossWaitTarget(ref manager);

            CurrentState = _bossWaitTarget;
        }

        private void OnEnable()
        {
            CurrentState = _bossWaitTarget;
            bossHealthController.SetHealthText(_health);
        }

        private void OnDisable()
        {
            bossAttackController.enabled = true;
        }

        private void Start()
        {
            CurrentState.EnterState();
        }

        public void SetTriggerAnim(BossAnimType animTypeType)
        {
            animationController.SetTriggerAnim(animTypeType);
        }

        private void Update()
        {
            CurrentState.UpdateState();
        }

        public void Attack()
        {
            bossAttackController.ThrowBomb();
        }

        public void SetBomb()
        {
            bossAttackController.PrepareBomb();
        }


        public void SwitchState(BossStateType state)
        {
            StopAllCoroutines();
            switch (state)
            {
                case BossStateType.Attack:
                    CurrentState = _bossAttackState;
                    break;
                case BossStateType.Dead:
                    CurrentState = _bossDeadState;
                    break;
                case BossStateType.Wait:
                    CurrentState = _bossWaitTarget;
                    break;
            }

            CurrentState.EnterState();
        }

        IEnumerator DeadWaiter()
        {
            WaitForSeconds waiter = new WaitForSeconds(1.5f);
            SwitchState(BossStateType.Dead);
            yield return waiter;
            gameObject.SetActive(false);
        }

        public void Damage(int damage)
        {
            if (IsDead == false)
            {
                Health -= damage;
                if (Health <= 0)
                {
                    bossAttackController.enabled = false;
                    IsDead = true;
                    StartCoroutine(DeadWaiter());
                }
            }
        }
    }
}