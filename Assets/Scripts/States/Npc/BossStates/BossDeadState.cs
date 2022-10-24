using Abstract;
using Controller;
using Enums.Npc;
using Signals;
using UnityEngine;

namespace States.Npc.BossStates
{
    public class BossDeadState:IStateMachine
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

        public BossDeadState(ref BossManager manager)
        {
            _manager = manager;
            

        }


        public void EnterState()
        {
            _manager.SetTriggerAnim(BossAnimType.Dead);
            _manager.Target = null;
         
            BaseSignals.Instance.onBossIsDead?.Invoke();
        }

        public void UpdateState()
        {
        }

        public void OnTriggerEnterState(Collider other)
        {
        }

        public void OnTriggerExitState(Collider other)
        {
        }
    }
}