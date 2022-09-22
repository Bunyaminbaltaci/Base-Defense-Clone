using System.Runtime.InteropServices;
using Abstract;
using Enums.Npc;
using Manager;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.Enemy
{
    public class RushTargetInpcState:IStateMachine
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

        public RushTargetInpcState(ref EnemyManager manager, ref NavMeshAgent agent)
        {

            _manager = manager;
            _agent = agent;

        }
        public void EnterState()
        {
            _agent.SetDestination(_manager.Target.transform.position);
            _manager.SetTriggerAnim(EnemyAnimType.Run);
        }

        public void UpdateState()
        {
            _agent.destination = _manager.Target.transform.position;
            if (_agent.remainingDistance<=_agent.stoppingDistance)
            {
                SwitchState(EnemyStateType.AttackTarget);
            }
        }

        public void OnTriggerEnterState(Collider other)
        {
        }

        public void OnTriggerExitState(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _manager.Target = BaseSignals.Instance.onGetTarget();
                SwitchState(EnemyStateType.WalkTarget);
            }
        }
        public void SwitchState(EnemyStateType type)
        {
            _manager.SwitchState(type);
        }
        
        


    }
}