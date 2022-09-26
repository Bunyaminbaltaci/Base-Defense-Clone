using System.Collections;
using Abstract;
using Enums.Npc;
using Manager;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.SupportStates
{
    public class GoTurretStackState:IStateMachine
    {
        #region Self Variables

        #region Private Variables

        private SupportManager _manager;
        private NavMeshAgent _agent;
        
        

        #endregion

        #endregion

        public GoTurretStackState(ref SupportManager manager,ref NavMeshAgent agent)
        {
            _agent = agent;
            _manager = manager;
        }
        public void EnterState()
        {
            while (true)
            {
                var Target = BaseSignals.Instance.onGetTurretStack?.Invoke();
                if (Target!=_manager.Target)
                {
                    _manager.Target = Target;
                    break;
                }
            }

           
            _agent.SetDestination(_manager.Target.transform.position);
            _manager.SetTriggerAnim(WorkerAnimType.Walk);
        }

        public void UpdateState()
        {
          
        }

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("TurretStack"))
            {
                _manager.SwitchState(SupportStatesType.WaitForDischarge);
            }
        }

        public void OnTriggerExitState(Collider other)
        {
          
        }

     
    }
}