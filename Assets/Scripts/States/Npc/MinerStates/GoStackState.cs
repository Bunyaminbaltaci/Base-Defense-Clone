using Abstract;
using Enums.Npc;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.MinerStates
{
    public class GoStackState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private MinerManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion

        public GoStackState(MinerManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public void EnterState()
        {
            _agent.SetDestination(_manager.Stack.transform.position);
            _manager.SetAnim(MinerAnimType.Carry);
        }

        public void UpdateState()
        {
        }

        public void OnCollisionDetectionState(Collider other)
        {
            if (other.CompareTag("MineWareHouse"))
            {
                PushDiamondOnStack();
            }
        }

        private void PushDiamondOnStack()
        {
            IdleSignals.Instance.onAddDiamondStack?.Invoke(_manager.transform.gameObject);
            SwitchState();
        }


        public void SwitchState()
        {
            _manager.SwitchState(_manager.GoMine);
        }
    }
}