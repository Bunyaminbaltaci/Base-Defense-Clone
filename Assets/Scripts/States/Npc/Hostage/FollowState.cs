using Abstract;
using Enums.Npc;
using Controller;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.MinerStates
{
    public class FollowState : IStateMachine
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

        #endregion

        public FollowState(HostageManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public void EnterState()
        {
            BaseSignals.Instance.onRemoveHostageInSpawnPoint?.Invoke(_manager.gameObject);
            _agent.SetDestination(_manager.Target.transform.position);
            _manager.SetTriggerAnim(HostageAnimType.Idle);
        }

        public void UpdateState()
        {
            FollowPlayer();
        }


        public void OnTriggerEnterState(Collider other)
        {
        }

        public void OnTriggerExitState(Collider other)
        {
            
        }
        

        private void FollowPlayer()
        {
            var position = _manager.Target.transform.position;
            _agent.SetDestination(position) ;
            SetAnim();
        }

        private void SetAnim()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _manager.SetBoolAnim(HostageAnimType.Run, false);
                return;
            }

            _manager.SetBoolAnim(HostageAnimType.Run, true);
        }
    }
}