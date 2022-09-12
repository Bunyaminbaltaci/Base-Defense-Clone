using Abstract;
using Managers;
using UnityEngine;

namespace States.HarvesterStates
{
    public class CollectMoneyState : IStateMachine
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


        public CollectMoneyState(ref HarvesterManager manager)
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