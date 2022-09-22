using Abstract;
using Enums;
using Enums.Npc;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace States.MinerStates
{
    public class GoMineInpcState:IStateMachine
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

        public GoMineInpcState( MinerManager manager,ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
            
        }
        public void EnterState()
        {
            
            _manager.SetTriggerAnim(MinerAnimType.Run);
            _manager.SetAnimLayer(AnimLayerType.UpperBody,0);
     

        }

        public void UpdateState()
        {
            _agent.destination = _manager.Target.transform.position;
        }

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("Mine"))
            {
                SwitchState(MinerStatesType.Dig);
            }
           
        }

        public void OnTriggerExitState(Collider other)
        {
            
        }

        public void SwitchState(MinerStatesType type)
        {
            _manager.SwitchState(type);
        }
    }
}