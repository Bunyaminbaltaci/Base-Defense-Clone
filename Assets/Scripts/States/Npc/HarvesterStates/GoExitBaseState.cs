using Abstract;
using Enums.Npc;
using Manager;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.HarvesterStates
{
    public class GoExitBaseState:IStateMachine
    {
        #region Self Varibles

        #region Private Variables

        private NavMeshAgent _agent;
        private HarvesterManager _manager;

        

        #endregion

        #endregion
        public GoExitBaseState(ref HarvesterManager manager,ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
            
        }
        public void EnterState()
        {

            _manager.Target = BaseSignals.Instance.onGetExit?.Invoke();
            _agent.SetDestination(_manager.Target.transform.position);
            
        }

        public void UpdateState()
        {
            if (_agent.remainingDistance<=_agent.stoppingDistance)
            {
                _manager.SwitchState(HarvesterStateType.CollectMoney);
            }
        }

        public void OnTriggerEnterState(Collider other)
        {
          
        }

        public void OnTriggerExitState(Collider other)
        {
         
        }
    }
}