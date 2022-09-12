using Abstract;
using Managers;
using UnityEngine;

namespace States.MinerStates
{
    public class GoMineState:IStateMachine
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

        public GoMineState( MinerManager manager)
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
            if (other.CompareTag("Mine"))
            {
                SwitchState();
            }
           
        }
        
        public void SwitchState()
        {
            _manager.SwitchState(_manager.Dig);
        }
    }
}