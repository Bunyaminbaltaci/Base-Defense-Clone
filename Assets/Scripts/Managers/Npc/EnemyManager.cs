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

        private AttackTargetNPCState _attackTargetNpcState;
        private DeadNPCState _deadNpcState;
        private RushTargetNPCState _rushTargetNpcState;
        private WalkTargetNPCState _walkTargetNpcState;
        
       

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

        private void OnDisable()
        {
            
            Target = null;
            CurrentInpcState =_walkTargetNpcState ;
        }

        private void GetReferences()
        {
            _attackTargetNpcState = new AttackTargetNPCState(ref enemyManager, ref agent);
            _deadNpcState = new DeadNPCState(ref enemyManager, ref agent);
            _rushTargetNpcState = new RushTargetNPCState(ref enemyManager, ref agent);
            _walkTargetNpcState = new WalkTargetNPCState(ref enemyManager, ref agent);
         
            CurrentInpcState =_walkTargetNpcState ;
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
                    CurrentInpcState =_walkTargetNpcState ;
                    break;
                case EnemyStateType.RushTarget:
                    CurrentInpcState =_rushTargetNpcState ;
                    break;
                case EnemyStateType.AttackTarget:
                    CurrentInpcState =_attackTargetNpcState ;
                    break;    
                case EnemyStateType.Dead:
                    CurrentInpcState =_deadNpcState ;
                    break;
            }
            CurrentInpcState.EnterState();
        }
    }
}