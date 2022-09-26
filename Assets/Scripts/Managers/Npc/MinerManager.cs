using System;
using System.Collections;
using System.Net.NetworkInformation;
using Abstract;
using Data;
using Datas.ValueObject;
using Enums;
using Enums.Npc;
using Manager.Npc;
using Signals;
using States.MinerStates;
using UnityEngine;
using UnityEngine.AI;

namespace Manager
{
    public class MinerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        

        public MinerStatesType MinerSType;
        public GameObject Target;
        public GameObject Stack;
        public IStateMachine CurrentInpcState;
       

        #endregion

        #region Serialized Variables

  
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private MinerAnimationController animationController;
        [SerializeField] private GameObject diamond;
        [SerializeField] private GameObject axe;

        #endregion

        #region Private Variables

        private GoMineInpcState _goMineInpc;
        private GoStackInpcState _goStackInpc;
        private DigInpcState _digInpc;
        private WaitInpcState _waitInpc;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
           
        }

       


        private void GetReferences()
        {
            _digInpc = new DigInpcState(this);
            _goStackInpc = new GoStackInpcState(this, ref agent);
            _goMineInpc = new GoMineInpcState(this, ref agent);
            _waitInpc = new WaitInpcState(this, ref agent);
            CurrentInpcState = _goMineInpc;
        }

        private void OnEnable()
        {
            Target = BaseSignals.Instance.onGetMineTarget();
            Stack = BaseSignals.Instance.onGetMineStackTarget();
            CurrentInpcState.EnterState();
            
        }

        private void Update()
        {
            CurrentInpcState.UpdateState();
        }


        public void SwitchState( MinerStatesType stateType)
        {
            switch (stateType)
            {
                case MinerStatesType.Dig:
                    CurrentInpcState = _digInpc;
                    diamond.SetActive(false);
                    axe.SetActive(true);
                    break;
                case MinerStatesType.GoMine:
                    CurrentInpcState = _goMineInpc;
                    diamond.SetActive(false);
                    break;
                case MinerStatesType.GoStack:
                    CurrentInpcState = _goStackInpc;
                    axe.SetActive(false);
                    diamond.SetActive(true);
                    break;
                case MinerStatesType.Wait:
                    CurrentInpcState = _waitInpc;
                    break;
            }

            CurrentInpcState.EnterState();
        }

        public void SetTriggerAnim(MinerAnimType animType)
        {
            animationController.SetAnim(animType);
        }

        public void SetAnimLayer(AnimLayerType type,float weight)
        {
            animationController.SetLayer(type,weight);
            
        }

        public IEnumerator DigDiamond()
        {
            yield return new WaitForSeconds(7);
            SwitchState(MinerStatesType.GoStack);
        }
    }
}