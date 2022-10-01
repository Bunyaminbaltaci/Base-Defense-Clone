using Abstract;
using Enums.Npc;
using Manager;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.HarvesterStates
{
    public class GoEnterBaseState:IStateMachine
    {
        #region Self Varibles

        #region Private Variables

        private NavMeshAgent _agent;
        private HarvesterManager _manager;

        

        #endregion

        #endregion
        public GoEnterBaseState(ref HarvesterManager manager,ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
            
        }
        public void EnterState()
        {
            _manager.Target = BaseSignals.Instance.onGetEnter?.Invoke();
            _agent.SetDestination(_manager.Target.transform.position);
        }

        public void UpdateState()
        {
            if (_agent.remainingDistance<=_agent.stoppingDistance)
            {
                _manager.SwitchState(HarvesterStateType.GoExitBase);
            }
        }

        public void OnTriggerEnterState(Collider other)
        {
          
        }

        public void OnTriggerExitState(Collider other)
        {
            if (other.CompareTag("BaseLimit"))
            {
              _manager.StartCollect();
            }
        }
    }
}