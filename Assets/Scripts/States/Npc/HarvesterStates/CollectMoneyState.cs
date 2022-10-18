using Abstract;
using Enums.Npc;
using Controller;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.HarvesterStates
{
    public class CollectMoneyState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private HarvesterManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion


        public CollectMoneyState(ref HarvesterManager manager,ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public void EnterState()
        {
            
                _agent.SetDestination(_manager.Target.transform.position);
                _manager.SetTriggerAnim(WorkerAnimType.Walk);

        }

        public void UpdateState()
        {
            if (_agent.remainingDistance<=_agent.stoppingDistance)
            {
                _manager.SwitchState(HarvesterStateType.WaitMoney);
            }
        }

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("Money"))
            {
             
                _manager.AddStack(other.gameObject);
                BaseSignals.Instance.onRemoveHaversterTargetList?.Invoke(other.gameObject);

            }
         
        }

        public void OnTriggerExitState(Collider other)
        {
          
        }
    }
}