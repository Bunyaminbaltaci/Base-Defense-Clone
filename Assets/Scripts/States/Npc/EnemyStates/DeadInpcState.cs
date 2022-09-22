using Abstract;
using Enums.Npc;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.Enemy
{
    public class DeadInpcState:IStateMachine
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

        public DeadInpcState(ref EnemyManager manager, ref NavMeshAgent agent)
        {

            _manager = manager;
            _agent = agent;

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

        public void SwitchState(EnemyStateType type)
        {
            throw new System.NotImplementedException();
        }
    }
}