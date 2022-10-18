using Abstract;
using Enums.Npc;
using Controller;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.Enemy
{
    public class AttackTargetState : IStateMachine
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

        public AttackTargetState(ref EnemyManager manager, ref NavMeshAgent agent)
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
                SwitchState(EnemyStateType.WalkTarget);
            }
        }

        public void SwitchState(EnemyStateType type)
        {
            _manager.SwitchState(type);
        }
    }
}