using Abstract;
using Managers;
using UnityEngine;

namespace States.MinerStates
{
    public class DigState:IStateMachine
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

        public DigState( MinerManager manager)
        {
            _manager = manager;
        }
        public void EnterState()
        {
           
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