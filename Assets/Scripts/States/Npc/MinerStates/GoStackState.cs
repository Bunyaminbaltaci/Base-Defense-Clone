using Abstract;
using Managers;
using UnityEngine;

namespace States.MinerStates
{
    public class GoStackState:IStateMachine
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        private MinerManager _manager;
        #endregion

        #endregion

        public GoStackState( MinerManager manager)
        {
            _manager = manager;
        }
        public void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public void OnCollisionDetectionState(Collider other)
        {
            throw new System.NotImplementedException();
        }
        

        public void SwitchState()
        {
            throw new System.NotImplementedException();
        }
    }
}