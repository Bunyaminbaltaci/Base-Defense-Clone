using System;
using System.Net.NetworkInformation;
using Abstract;
using Data;
using Datas.ValueObject;
using States.MinerStates;
using UnityEngine;

namespace Managers
{
    public class MinerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GoMineState GoMine;
        public GoStackState GoStack;
        public DigState Dig;
        public IStateMachine CurrentState;

        #endregion

        #region Serialized Variables

        [SerializeField] private MinerManager minerManager;

        #endregion

        #region Private Variables

        private MinerData _minerData;
        private GameObject _target;

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
            Dig = new DigState(this);
            GoStack = new GoStackState(this);
            GoMine = new GoMineState(this);
            CurrentState = GoMine;
        }

        private void Start()
        {
            
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
    }
}