using Abstract;
using Enums;
using Enums.Npc;
using Controller;
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
        private IStateMachine _─▒npcStateMachineImplementation;

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
            _manager.SetTriggerAnim(MinerAnimType.Run);
            _manager.SetAnimLayer(AnimLayerType.UpperBody,1);
        }

        public void UpdateState()
        {
            _agent.destination=_manager.Stack.transform.position;
        }

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("MineWareHouse"))
            {
                PushDiamondOnStack();
            }
        }

        public void OnTriggerExitState(Collider other)
        {
            _─▒npcStateMachineImplementation.OnTriggerExitState(other);
        }

        private void PushDiamondOnStack()
        {
            BaseSignals.Instance.onAddDiamondStack?.Invoke(_manager.transform.gameObject);
            SwitchState();
        }


        public void SwitchState()
        {
            _manager.SwitchState(MinerStatesType.GoMine);
        }
    }
}