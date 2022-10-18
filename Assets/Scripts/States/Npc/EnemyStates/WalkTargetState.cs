using Abstract;
using Enums.Npc;
using Controller;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.Enemy
{
    public class WalkTargetState:IStateMachine
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

        public WalkTargetState(ref EnemyManager manager, ref NavMeshAgent agent)
        {

            _manager = manager;
            _agent = agent;

        }
        public void EnterState()
        {
            _manager.Target = BaseSignals.Instance.onGetEnemyTarget?.Invoke();
            _agent.SetDestination(_manager.Target.transform.position);
            _manager.SetTriggerAnim(EnemyAnimType.Walk);
        }

        public void UpdateState()
        {
          
        }

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _manager.Target = other.gameObject;
                SwitchState(EnemyStateType.RushTarget);
            }
        }

        public void OnTriggerExitState(Collider other)
        {

        }

        public void SwitchState(EnemyStateType type)
        {
            _manager.SwitchState(type);
        }
    }
}