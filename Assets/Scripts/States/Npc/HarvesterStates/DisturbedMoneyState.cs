using Abstract;
using Managers;
using UnityEngine;

namespace States.HarvesterStates
{
    public class DisturbedMoneyState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private HarvesterManager _manager;
        private IStateMachine _stateMachineImplementation;

        #endregion

        #endregion


        public DisturbedMoneyState(ref HarvesterManager manager)
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
            throw new System.NotImplementedException();
        }
        

        public void SwitchState()
        {
        }
    }
}