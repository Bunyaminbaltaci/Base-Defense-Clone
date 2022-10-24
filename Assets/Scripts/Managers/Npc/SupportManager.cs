using System;
using System.Collections;
using Abstract;
using Controller.Npc;
using Controller;
using Data;
using Datas.ValueObject;
using Enums;
using Enums.Npc;
using Signals;
using States.Npc.SupportStates;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
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
        

      


        private void GetReferences()
        {
            var manager = this;
            _goAmmoAreaState = new GoAmmoAreaState(ref manager, ref agent);
            _goTurretStackState = new GoTurretStackState(ref manager, ref agent);
            _waitForFullStack = new WaitForFullStack(ref manager, ref agent);
            _waitForDischarge = new WaitForDischargeState(ref manager, ref agent);
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
        

        public bool StackCheck()
        {
            return stackController.StackList.Count > 0;
        }

        private void SendPlayerDataToControllers()
        {
            stackController.SetStackData(_data.SData, _data.SData);
        }

        
    
        public IEnumerator TakeBulletBox()
        {
            var waiter = new WaitForSeconds(0.2f);
            while (stackController.StackList.Count < _data.SData.StackLimit)
            {
                var obj =BaseSignals.Instance.onGetBulletBox?.Invoke();
                if (obj==null)
                    break;
                stackController.AddStack(obj);
                yield return waiter;
            }
        }

      

        public IEnumerator StartBulletBoxSend(GameObject target)
        {
            WaitForSeconds waiter = new WaitForSeconds(0.2f);
            while (stackController.StackList.Count > 0)
            {
                if (BaseSignals.Instance.onGetTurretLimit(target) <= 0 )
                    yield break;
                
                BaseSignals.Instance.onSendAmmoInStack?.Invoke(target, stackController.SendBulletBox());
                yield return waiter;
            }
        }
    }
}