using System;
using System.Collections;
using Abstract;
using Controller.Npc.Support;
using Controllers;
using Data;
using Datas.ValueObject;
using Enums.Npc;
using Signals;
using States.Npc.SupportStates;
using UnityEngine;
using UnityEngine.AI;

namespace Manager
{
    public class SupportManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public IStateMachine CurrentState;
        public GameObject Target;

        #endregion

        #region Serialzed Variables

        [SerializeField] private SupportAnimationController animationController;
        [SerializeField] private SupportManager supportManager;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private StackController stackController;

        #endregion

        #region Private Variables

        private GoAmmoAreaState _goAmmoAreaState;
        private GoTurretStackState _goTurretStackState;
        private WaitForFullStack _waitForFullStack;
        private WaitForDischargeState _waitForDischarge;
        private WorkerData _data;
        

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
            _data = GetWorkerData();
            SendPlayerDataToControllers();
        }

     

        #region Event Subscription

  
        private void OnEnable()
        {
            Subscribe();
            CurrentState.EnterState();
        }

        private void Subscribe()
        {
           
            BaseSignals.Instance.OnSendBulletBox += OnSendBullerBox;
        }

       
        private void Unsubscribe()
        {
       
            BaseSignals.Instance.OnSendBulletBox -= OnSendBullerBox;
        }


        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

     

        private void GetReferences()
        {
            _goAmmoAreaState = new GoAmmoAreaState(ref supportManager, ref agent);
            _goTurretStackState = new GoTurretStackState(ref supportManager, ref agent);
            _waitForFullStack = new WaitForFullStack(ref supportManager, ref agent);
            _waitForDischarge = new WaitForDischargeState(ref supportManager, ref agent);
            CurrentState = _goAmmoAreaState;
        }

        private void Start()
        {
            CurrentState.EnterState();
        }


        private void Update()
        {
            CurrentState.UpdateState();
        }

        private WorkerData GetWorkerData() => Resources.Load<CD_WorkerData>("Data/CD_WorkerData").SupportData;
        public void SwitchState(SupportStatesType stateType)
        {
            switch (stateType)
            {
                case SupportStatesType.GoAmmoArea:
                    CurrentState = _goAmmoAreaState;
                    break;
                case SupportStatesType.GoTurretStack:
                    CurrentState = _goTurretStackState;
                    break;
                case SupportStatesType.WaitForFullStack:
                    CurrentState = _waitForFullStack;
                    break;
                case SupportStatesType.WaitForDischarge:
                    CurrentState = _waitForDischarge;
                    break;
            }

            CurrentState.EnterState();
        }

        public void SetTriggerAnim(WorkerAnimType animType)
        {
            animationController.SetAnim(animType);
        }

        public void StartCort(IEnumerator name)
        {
            StartCoroutine(name);
        }

        public void StopCort()
        {
            StopAllCoroutines();
        }


        public bool StackCheck()
        {
          
            return stackController.StackList.Count > 0;
        }
        
        private void SendPlayerDataToControllers()
        {
            stackController.SetStackData(_data.SData,_data.SData);
        }
        private int OnSendBullerBox(Collider arg1, GameObject arg2)
        {
            if (arg1.gameObject != transform.GetChild(1).gameObject)
                return default;
            stackController.AddStack(arg2);
            return _data.SData.StackLimit;
        }
    }
}