using Abstract;
using Enums.Npc;
using Manager;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.SupportStates
{
    public class GoAmmoAreaState:IStateMachine
    {
        #region Self Variables

        #region Private Variables

        private SupportManager _manager;
        private NavMeshAgent _agent;
        
        

        #endregion

        #endregion

        public GoAmmoAreaState(ref SupportManager manager,ref NavMeshAgent agent)
        {
            _agent = agent;
            _manager = manager;
        }
        
        
        public void EnterState()
        {
            _manager.Target = BaseSignals.Instance.onGetAmmoArea?.Invoke();
            if (_manager.Target==null)
            {
                _manager.SwitchState(SupportStatesType.WaitForFullStack);
            }
            _manager.SetTriggerAnim(WorkerAnimType.Walk);
            _agent.SetDestination(_manager.Target.transform.position);

        }

        public void UpdateState()
        {
          
        }

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("BulletArea"))
            {
                _manager.StartCoroutine(_manager.TakeBulletBox());
                _manager.SwitchState(SupportStatesType.WaitForFullStack);   
            }
        
        }

        public void OnTriggerExitState(Collider other)
        {
     
        }
        
        
        
    }
}