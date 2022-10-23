using System.Collections;
using Abstract;
using Controller.Npc;
using Controller;
using Data;
using Datas.ValueObject;
using Enums.Npc;
using States.HarvesterStates;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class HarvesterManager : MonoBehaviour
    {
          #region Self Variables

        #region Public Variables
        
        public IStateMachine CurrentState;
        public GameObject Target;

        #endregion

        #region Serialized Variables

  
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private HarvesterAnimationController animationController;
        [SerializeField] private StackController stackController;

        #endregion

        #region Private Variables

      
        private GoEnterBaseState _goEnterBase;
        private WaitMoneyState _waitMoney;
        private CollectMoneyState _collectMoneyState;
        private WorkerData _data;
        
       

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            _data = GetWorkerData();
            SendPlayerDataToControllers();

        }
        private WorkerData GetWorkerData() => Resources.Load<CD_WorkerData>("Data/CD_WorkerData").HarvesterData;
        private void OnEnable()
        {
            CurrentState.EnterState();
        }

        private void OnDisable()
        {
            Target = null;
            CurrentState = _goEnterBase;
        }

        private void GetReferences()
        {
            var manager = this;
            _waitMoney = new WaitMoneyState(ref manager,ref agent);
            _collectMoneyState = new CollectMoneyState(ref manager,ref agent);
            _goEnterBase = new GoEnterBaseState(ref manager,ref agent);
            CurrentState = _goEnterBase;
        }

        private void Start()
        {
          
        }

        private void Update()
        {
            CurrentState.UpdateState();
        }

        public void SetTriggerAnim(WorkerAnimType animType)
        {
            animationController.SetAnim(animType);
        }
      
        public void SwitchState(HarvesterStateType stateType)
        {
            switch (stateType)
            {
                case HarvesterStateType.GoEnterBase  :
                    CurrentState = _goEnterBase;
                    break;
              case HarvesterStateType.CollectMoney :
                    CurrentState = _collectMoneyState ;
                    break;
                case HarvesterStateType.WaitMoney:
                    CurrentState = _waitMoney;
                    break;
            }
            CurrentState.EnterState();
        }
        public void StartCollect()
        {
            stackController.StartCollect();
        }

        public void StartCort(IEnumerator name)
        {
            StartCoroutine(name);
        }

        public void AddStack(GameObject money)
        {
         
            if (stackController.StackList.Count<=_data.SData.StackLimit)
            {
                money.GetComponent<MoneyController>().isTaked();
                stackController.AddStack(money);
            }
            else
            {
                SwitchState(HarvesterStateType.GoEnterBase);
            }
        }
        private void SendPlayerDataToControllers()
        {
            stackController.SetStackData(_data.SData, _data.SData);
        }
    }
    
}