using Abstract;
using Enums.Npc;
using Controller;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.Enemy
{
    public class AttackTargetState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion

        public AttackTargetState(ref EnemyManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public void EnterState()
        {
            Debug.Log("Attack");

            _manager.SetTriggerAnim(EnemyAnimType.Attack);
            _manager.StartCoroutine(_manager.Attack());

        }

        public void UpdateState()
        {
            _agent.destination = _manager.Target.transform.position;
            if ((_manager.transform.position-_manager.Target.transform.position).sqrMagnitude >Mathf.Pow(_agent.stoppingDistance,2))
            {
                SwitchState(EnemyStateType.RushTarget);
            }
        }
        
    

        public void OnTriggerEnterState(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player girdi Attack State");
                _manager.TargetIdamageable = other.GetComponentInParent<IDamageable>();
                _manager.Target = other.gameObject;
                SwitchState(EnemyStateType.RushTarget);
            }
        }

        public void OnTriggerExitState(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _manager.TargetIdamageable = null;
                SwitchState(EnemyStateType.WalkTarget);
            }
        }

        public void SwitchState(EnemyStateType type)
        {
            _manager.SwitchState(type);
        }
    }
}