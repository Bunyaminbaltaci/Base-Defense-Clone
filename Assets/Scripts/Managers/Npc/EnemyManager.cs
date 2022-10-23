using System;
using System.Collections;
using Abstract;
using Controller.Npc.Enemy;
using Enums;
using Enums.Npc;
using Managers.Core;
using Signals;
using States.Npc.Enemy;
using UnityEngine;
using UnityEngine.AI;


namespace Controller
{
    public class EnemyManager : MonoBehaviour, IDamageable
    {
        #region Self Variables

        #region Public Variables

        public IStateMachine CurrentState;
        public GameObject Target;
        public IDamageable TargetIdamageable;
        public int Health { get; set; } = 100;

        #endregion

        #region Serialized Variables

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyAnimationController animationController;

        #endregion

        #region Private Variables

        private AttackTargetState _attackTargetState;
        private DeadState _deadState;
        private RushTargetState _rushTargetState;
        private WalkTargetState _walkTargetState;
        private bool IsDead = false;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }


        private void OnEnable()
        {
            CurrentState = _walkTargetState;
            CurrentState.EnterState();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            SetTriggerAnim(EnemyAnimType.Idle);
            Target = null;
            IsDead = false;
            Health = 100;
            agent.enabled = true;
     
        }

        private void GetReferences()
        {
            var manager = this;
            _attackTargetState = new AttackTargetState(ref manager, ref agent);
            _deadState = new DeadState(ref manager, ref agent);
            _rushTargetState = new RushTargetState(ref manager, ref agent);
            _walkTargetState = new WalkTargetState(ref manager, ref agent);

        }

        private void Start()
        {
            CurrentState = _walkTargetState;
            CurrentState.EnterState();
        }


        private void Update()
        {
            CurrentState.UpdateState();
        }

        public IEnumerator Attack()
        {
            yield return new WaitForSeconds(1f);
            if (TargetIdamageable!=null)
            {
                TargetIdamageable.Damage(5);
            }
           
        }

        public void SetTriggerAnim(EnemyAnimType animType)
        {
            animationController.SetTriggerAnim(animType);
        }

        public void SwitchState(EnemyStateType state)
        {
            StopAllCoroutines();
            switch (state)
            {
                case EnemyStateType.WalkTarget:
                    CurrentState = _walkTargetState;
                    break;
                case EnemyStateType.RushTarget:
                    CurrentState = _rushTargetState;
                    break;
                case EnemyStateType.AttackTarget:
                    CurrentState = _attackTargetState;
                    break;
                case EnemyStateType.Dead:
                    CurrentState = _deadState;
                    break;
            }

            CurrentState.EnterState();
        }


        public void Damage(int damage)
        {
            if (IsDead == false)
            {
                Health -= damage;
                if (Health <= 0)
                {
                    agent.enabled = false;
                    IsDead = true;
                    StopAllCoroutines();
                    SwitchState(EnemyStateType.Dead);
                }
            }
        }
    }
}