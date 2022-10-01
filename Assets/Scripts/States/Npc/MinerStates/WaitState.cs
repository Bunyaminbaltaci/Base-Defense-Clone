using Abstract;
using Enums.Npc;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace States.MinerStates
{
    public class WaitState:IStateMachine
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        private MinerManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion

        public WaitState( MinerManager manager,ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }
        public void EnterState()
        {
           
          _manager.SetTriggerAnim(MinerAnimType.Idle);
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

        public void SwitchState()
        {
            _manager.SwitchState(MinerStatesType.GoStack);
        }
    }
}