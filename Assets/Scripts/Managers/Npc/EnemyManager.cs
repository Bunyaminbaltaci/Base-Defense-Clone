using System;
using System.Collections;
using Abstract;
using Controller.Npc.Enemy;
using Enums;
using Enums.Npc;
using Signals;
using States.Npc.Enemy;
using UnityEngine;
using UnityEngine.AI;


namespace Manager
{
    public class EnemyManager : MonoBehaviour
    {
         #region Self Variables

        #region Public Variables

        public EnemyStateType EnemySType;
        public IStateMachine CurrentInpcState;
        public GameObject Target;

        #endregion

        #region Serialized Variables

  
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private EnemyAnimationController animationController;

        #endregion

        #region Private Variables

        private AttackTargetState _attackTargetState;
        private DeadState _deadState;
        private RushTargetState _rushTargetState;
        private WalkTargetState _walkTargetState;
        
       

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
           
        }


        private void OnEnable()
        {
            // SubscribeEvent();
            Target = BaseSignals.Instance.onGetEnemyTarget?.Invoke();
            CurrentInpcState.EnterState();
            
        }

        private void OnDisable()
        {
            PushMoney();
            Target = null;
            CurrentInpcState =_walkTargetState ;
        }

        private void GetReferences()
        {
            _attackTargetState = new AttackTargetState(ref enemyManager, ref agent);
            _deadState = new DeadState(ref enemyManager, ref agent);
            _rushTargetState = new RushTargetState(ref enemyManager, ref agent);
            _walkTargetState = new WalkTargetState(ref enemyManager, ref agent);
         
            CurrentInpcState =_walkTargetState ;
        }

        private void Start()
        {
            CurrentInpcState.EnterState();
        }

        private void Update()
        {
            CurrentInpcState.UpdateState();
        }

        public IEnumerator Attack()
        {

            yield return new WaitForSeconds(2f);

            SwitchState(EnemyStateType.RushTarget);

        }
        public void SetTriggerAnim(EnemyAnimType animType)
        {
            animationController.SetTriggerAnim(animType);
        }

        public void SwitchState(EnemyStateType state)
        {
            switch (state)
            {
                case EnemyStateType.WalkTarget :
                    CurrentInpcState =_walkTargetState ;
                    break;
                case EnemyStateType.RushTarget:
                    CurrentInpcState =_rushTargetState ;
                    break;
                case EnemyStateType.AttackTarget:
                    CurrentInpcState =_attackTargetState ;
                    break;    
                case EnemyStateType.Dead:
                    CurrentInpcState =_deadState ;
                    break;
            }
            CurrentInpcState.EnterState();
        }

        private void PushMoney()
        {
            for (int i = 0; i < 3; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Money);
            
                if (obj ==null)
                {
                    break;
                }

                obj.transform.SetParent(transform.parent);
                obj.SetActive(true);
                obj.transform.localPosition = transform.localPosition;
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up+Vector3.right,ForceMode.Force);
                BaseSignals.Instance.onAddHaversterTargetList?.Invoke(obj);
            }
            PoolSignals.Instance.onSendPool?.Invoke(gameObject,PoolType.Enemy);
        }

      
    }
}