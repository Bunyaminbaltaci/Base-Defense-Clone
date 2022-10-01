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
            
            _manager.Target = BaseSignals.Instance.onGetTurretStack?.Invoke();

            if (_manager.Target==null)
            {
                _manager.SwitchState(SupportStatesType.WaitForDischarge);
            }
            else
            {
                _agent.SetDestination(_manager.Target.transform.position);
                _manager.SetTriggerAnim(WorkerAnimType.Walk);
            }
          
        }

        public void UpdateState()
        {
          
        }

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("TurretStack") || _manager.Target==other.gameObject)
            {
                _manager.StartCoroutine(_manager.StartBulletBoxSend(other.gameObject));
                _manager.SwitchState(SupportStatesType.WaitForDischarge);
            }
        }

        public void OnTriggerExitState(Collider other)
        {
          
        }

     
    }
}