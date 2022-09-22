using Abstract;
using Manager;
using UnityEngine;

namespace States.HarvesterStates
{
    public class CollectMoneyInpcState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private HarvesterManager _manager;

        #endregion

        #endregion


        public CollectMoneyInpcState(ref HarvesterManager manager)
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
            throw new System.NotImplementedException();
        }
    }
}