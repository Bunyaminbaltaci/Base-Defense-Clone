using Abstract;
using Managers;
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
            _agent.SetDestination(_manager.Target.transform.position);
        }

        public void UpdateState()
        {
        }

        public void OnCollisionDetectionState(Collider other)
        {
        }

        public void SwitchState()
        {
        }
    }
}