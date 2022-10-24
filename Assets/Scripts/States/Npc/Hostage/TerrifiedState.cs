using Abstract;
using Enums.Npc;
using Controller;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.MinerStates
{
    public class TerrifiedState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private HostageManager _manager;
        private NavMeshAgent _agent;

        #endregion

        public TerrifiedState(HostageManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        #endregion

        public void EnterState()
        {
            _manager.Target = null;
            _manager.SetTriggerAnim(HostageAnimType.Terrified);
        }

        public void UpdateState()
        {
        }

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SetFollowTarget(other);
            }
        }

        public void OnTriggerExitState(Collider other)
        {
            
        }


        public void SwitchState(HostageStateType type)
        {
            _manager.SwitchState(type);
        }

        private void SetFollowTarget(Collider other)
        {
            _manager.Target = CoreGameSignals.Instance.onGetHostageTarget(_manager.transform.gameObject);

          
            SwitchState(HostageStateType.Follow);
        }
    }
}