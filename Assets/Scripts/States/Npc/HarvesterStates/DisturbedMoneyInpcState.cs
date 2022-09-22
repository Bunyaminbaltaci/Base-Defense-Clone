using Abstract;
using Manager;
using UnityEngine;

namespace States.HarvesterStates
{
    public class DisturbedMoneyInpcState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private HarvesterManager _manager;
        private IStateMachine _ınpcStateMachineImplementation;

        #endregion

        #endregion


        public DisturbedMoneyInpcState(ref HarvesterManager manager)
        {
            _manager = manager;
        }


        public void EnterState()
        {
        }

        public void UpdateState()
        {
        }

        public void OnTriggerEnterState(Collider other)
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerExitState(Collider other)
        {
            throw new System.NotImplementedException();
        }


        public void SwitchState()
        {
        }
    }
}