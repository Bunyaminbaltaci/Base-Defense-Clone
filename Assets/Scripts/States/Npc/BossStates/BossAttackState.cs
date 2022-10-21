using Abstract;
using Controller;
using Enums.Npc;
using UnityEngine;

namespace States.Npc.BossStates
{
    public class BossAttackState : IStateMachine
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

        public BossAttackState(ref BossManager manager)
        {
            _manager = manager;
        }

        public void EnterState()
        {
            _manager.SetTriggerAnim(BossAnimType.Attack);
        }

        public void UpdateState()
        {
            _manager.transform.LookAt(new Vector3(_manager.Target.transform.position.x,_manager.transform.position.y,_manager.Target.transform.position.z));
        }

        public void OnTriggerEnterState(Collider other)
        {
        }

        public void OnTriggerExitState(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _manager.Target = null;
                _manager.SwitchState(BossStateType.Wait);
            }
        }
    }
}