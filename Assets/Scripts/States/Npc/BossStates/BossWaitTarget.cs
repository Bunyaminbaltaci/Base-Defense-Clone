using Abstract;
using Controller;
using Enums.Npc;
using UnityEngine;

namespace States.Npc.BossStates
{
    public class BossWaitTarget:IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private BossManager _manager;

        #endregion

        #endregion

        public BossWaitTarget(ref BossManager manager)
        {
            _manager = manager;
        }


        public void EnterState()
        {
            _manager.SetTriggerAnim(BossAnimType.Idle);
        }

        public void UpdateState()
        {
            
        }

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _manager.Target = other.gameObject;
                _manager.SwitchState(BossStateType.Attack);
            }
        }

        public void OnTriggerExitState(Collider other)
        {
        }
    }
}