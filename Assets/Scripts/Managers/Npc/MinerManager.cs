using System;
using System.Net.NetworkInformation;
using Abstract;
using Data;
using Datas.ValueObject;
using Enums;
using Enums.Npc;
using Managers.Npc;
using Signals;
using States.MinerStates;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class MinerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GoMineState GoMine;
        public GoStackState GoStack;
        public DigState Dig;
        public WaitState Wait;
        public IStateMachine CurrentState;
        public GameObject Target;
        public GameObject Stack;
        public GameObject Diamond;
        public GameObject Axe;

        #endregion

        #region Serialized Variables

  
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private MinerAnimationController animationController;

        #endregion

        #region Private Variables

        private MinerData _minerData;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            _minerData = GetMinerData();
        }

        private MinerData GetMinerData() => Resources.Load<CD_MinerData>("NpcData/CD_MinerData").MinData;


        private void GetReferences()
        {
            Dig = new DigState(this, ref agent);
            GoStack = new GoStackState(this, ref agent);
            GoMine = new GoMineState(this, ref agent);
            Wait = new WaitState(this, ref agent);
            CurrentState = GoMine;
        }

        private void Start()
        {
            Target = IdleSignals.Instance.onGetMineTarget();
            Stack = IdleSignals.Instance.onGetMineStackTarget();
            SetAnim(MinerAnimType.Idle);
            CurrentState.EnterState();
            
        }

        private void Update()
        {
            CurrentState.UpdateState();
        }


        public void SwitchState(IStateMachine state)
        {
            CurrentState = state;
            CurrentState.EnterState();
        }

        public void SetAnim(MinerAnimType animType)
        {
            animationController.SetAnim(animType);
        }

        public void SetAnimLayer(AnimLayerType type,float weight)
        {
            animationController.SetLayer(type,weight);
            
        }
    }
}