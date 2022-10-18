using System.Collections;
using Abstract;
using Controller;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Enums.Npc;

namespace States.HarvesterStates
{
    public class WaitMoneyState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private HarvesterManager _manager;
        private NavMeshAgent _agent;

        #endregion

        #endregion


        public WaitMoneyState(ref HarvesterManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }


        public void EnterState()
        {
            _manager.SetTriggerAnim(WorkerAnimType.Idle);
            
            _manager.StartCort(WaitForTarget());
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


        IEnumerator WaitForTarget()
        {
            WaitForSeconds waiter = new WaitForSeconds(4);


            while (true)
            {
                _manager.Target = BaseSignals.Instance.onGetHarvesterTarget?.Invoke();
                if (_manager.Target != null)
                {
                    _manager.SwitchState(HarvesterStateType.CollectMoney);
                    break;
                }
                
                yield return waiter;
              
            }

          
        }
    }
}