using System.Collections;
using Abstract;
using Enums.Npc;
using Controller;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.SupportStates
{
    public class WaitForFullStack:IStateMachine
    {
        #region Self Variables

        #region Private Variables

        private SupportManager _manager;
        private NavMeshAgent _agent;
        
        

        #endregion

        #endregion

        public WaitForFullStack(ref SupportManager manager,ref NavMeshAgent agent)
        {
            _agent = agent;
            _manager = manager;
        }
        public void EnterState()
        {
          
            _manager.SetTriggerAnim(WorkerAnimType.Idle);
            _manager.StartCort(WaitForFull());
        }

        public void UpdateState()
        {
           
        }

        public void OnTriggerEnterState(Collider other)
        {
          
        }

        public void OnTriggerExitState(Collider other)
        {
           
        }
         IEnumerator WaitForFull()
        {
            yield return new WaitForSeconds(4);
            _manager.SwitchState(SupportStatesType.GoTurretStack);
        }

      
    }
}