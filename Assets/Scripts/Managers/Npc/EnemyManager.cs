using System.Collections;
using Abstract;
using Controller.Npc.Enemy;
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

        private AttackTargetInpcState _attackTargetInpcState;
        private DeadInpcState _deadInpcState;
        private RushTargetInpcState _rushTargetInpcState;
        private WalkTargetInpcState _walkTargetInpcState;
        
       

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
           
        }


        private void OnEnable()
        {
            // SubscribeEvent();
            Target = BaseSignals.Instance.onGetTarget?.Invoke();
            CurrentInpcState.EnterState();
            
        }

        // private void SubscribeEvent()
        // {
        //     
        //   
        // }
        //
        // private void UnSubscribeEvent()
        // {
        //
        // }
        //
        //

        private void OnDisable()
        {
            // UnSubscribeEvent();
            Target = null;
            CurrentInpcState =_walkTargetInpcState ;
        }

        private void GetReferences()
        {
            _attackTargetInpcState = new AttackTargetInpcState(ref enemyManager, ref agent);
            _deadInpcState = new DeadInpcState(ref enemyManager, ref agent);
            _rushTargetInpcState = new RushTargetInpcState(ref enemyManager, ref agent);
            _walkTargetInpcState = new WalkTargetInpcState(ref enemyManager, ref agent);
         
            CurrentInpcState =_walkTargetInpcState ;
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
                    CurrentInpcState =_walkTargetInpcState ;
                    break;
                case EnemyStateType.RushTarget:
                    CurrentInpcState =_rushTargetInpcState ;
                    break;
                case EnemyStateType.AttackTarget:
                    CurrentInpcState =_attackTargetInpcState ;
                    break;    
                case EnemyStateType.Dead:
                    CurrentInpcState =_deadInpcState ;
                    break;
            }
            CurrentInpcState.EnterState();
        }
    }
}