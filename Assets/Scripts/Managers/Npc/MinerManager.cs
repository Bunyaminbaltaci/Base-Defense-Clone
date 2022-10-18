using System;
using System.Collections;
using System.Net.NetworkInformation;
using Abstract;
using Controller.Npc;
using Data;
using Datas.ValueObject;
using Enums;
using Enums.Npc;
using Signals;
using States.MinerStates;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class MinerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        

        public MinerStatesType MinerSType;
        public GameObject Target;
        public GameObject Stack;
        public IStateMachine CurrentState;
       

        #endregion

        #region Serialized Variables

  
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private MinerAnimationController animationController;
        [SerializeField] private GameObject diamond;
        [SerializeField] private GameObject axe;

        #endregion

        #region Private Variables

        private GoMineState _goMine;
        private GoStackState _goStack;
        private DigState _dig;
        private WaitState _wait;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
           
        }

       


        private void GetReferences()
        {
            _dig = new DigState(this);
            _goStack = new GoStackState(this, ref agent);
            _goMine = new GoMineState(this, ref agent);
            _wait = new WaitState(this, ref agent);
            CurrentState = _goMine;
        }

        private void OnEnable()
        {
            Target = BaseSignals.Instance.onGetMineTarget();
            Stack = BaseSignals.Instance.onGetMineStackTarget();
            CurrentState.EnterState();
            
        }

        private void Update()
        {
            CurrentState.UpdateState();
        }


        public void SwitchState( MinerStatesType stateType)
        {
            switch (stateType)
            {
                case MinerStatesType.Dig:
                    CurrentState = _dig;
                    diamond.SetActive(false);
                    axe.SetActive(true);
                    break;
                case MinerStatesType.GoMine:
                    CurrentState = _goMine;
                    diamond.SetActive(false);
                    break;
                case MinerStatesType.GoStack:
                    CurrentState = _goStack;
                    axe.SetActive(false);
                    diamond.SetActive(true);
                    break;
                case MinerStatesType.Wait:
                    CurrentState = _wait;
                    break;
            }

            CurrentState.EnterState();
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