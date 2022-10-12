using System.Collections;
using Abstract;
using Enums;
using Enums.Npc;
using Manager;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.Npc.Enemy
{
    public class DeadState : IStateMachine
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

        public DeadState(ref EnemyManager manager, ref NavMeshAgent agent)
        {
            _manager = manager;
            _agent = agent;
        }

        public void EnterState()
        {
            _agent.SetDestination(_manager.transform.position);
            IsDead();
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

        public void SwitchState(EnemyStateType type)
        {
        }


        private void IsDead()
        {
            for (int i = 0; i < 3; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Money);

                if (obj == null)
                {
                    break;
                }

                obj.transform.SetParent(_manager.transform.parent);
                obj.SetActive(true);
                obj.transform.localPosition = _manager.transform.localPosition;
                obj.GetComponent<Rigidbody>().AddForce(new Vector3(
                    Random.Range(1f, 3f),
                    Random.Range(1f, 3f),
                    Random.Range(1f, 3f)
                ), ForceMode.Force);
                BaseSignals.Instance.onAddHaversterTargetList?.Invoke(obj);
            }

            _manager.StartCoroutine(WaitDeadAnim());
        }

        IEnumerator WaitDeadAnim()
        {
            _manager.SetTriggerAnim(EnemyAnimType.Dead);

            BaseSignals.Instance.onRemoveInDamageableStack?.Invoke(_manager.gameObject);
            yield return new WaitForSeconds(1f);
            PoolSignals.Instance.onSendPool?.Invoke(_manager.gameObject, PoolType.Enemy);

        }
    }
}