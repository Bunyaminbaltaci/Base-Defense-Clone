using Abstract;
using Enums.Npc;
using Manager;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.Enemy
{
    public class AttackTargetInpcState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion

        public AttackTargetInpcState(ref EnemyManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public void EnterState()
        {
            _manager.SetTriggerAnim(EnemyAnimType.Attack);
            _manager.StartCoroutine(_manager.Attack());
        }

        public void UpdateState()
        {
            _agent.destination = _manager.Target.transform.position;
            if (_agent.remainingDistance > _agent.stoppingDistance)
            {
                _manager.StopAllCoroutines();
                SwitchState(EnemyStateType.RushTarget);
            }
        }

        public void OnTriggerEnterState(Collider other)
        {
        }

        public void OnTriggerExitState(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _manager.Target = BaseSignals.Instance.onGetTarget?.Invoke();
                SwitchState(EnemyStateType.WalkTarget);
            }
        }

        public void SwitchState(EnemyStateType type)
        {
            _manager.SwitchState(type);
        }
    }
}