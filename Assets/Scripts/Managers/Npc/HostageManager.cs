using System;
using Abstract;
using Enums.Npc;
using Manager.Npc.Hostage;
using Manager.Npc;
using States.MinerStates;
using UnityEngine;
using UnityEngine.AI;

namespace Manager
{
    public class HostageManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public HostageStateType HostageSType;
        public IStateMachine CurrentInpcState;
        public GameObject Target;

        #endregion

        #region Serialized Variables

  
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private HostageAnimationController animationController;

        #endregion

        #region Private Variables

        private FollowState _follow;
        private TerrifiedState _terrified;
        
       

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
           
        }

        private void OnEnable()
        {
            CurrentInpcState.EnterState();
        }

        private void OnDisable()
        {
            Target = null;
            CurrentInpcState = _terrified;
        }

        private void GetReferences()
        {
            
            _follow = new FollowState(this, ref agent);
            _terrified = new TerrifiedState(this, ref agent);
            CurrentInpcState = _terrified;
        }

        private void Start()
        {
            CurrentInpcState.EnterState();
        }

        private void Update()
        {
            CurrentInpcState.UpdateState();
        }

        public void SetTriggerAnim(HostageAnimType animType)
        {
            animationController.SetTriggerAnim(animType);
        }
        public void SetBoolAnim(HostageAnimType animType,bool check)
        {
            
            animationController.SetBoolAnim( animType, check);
            
        }

        public void SwitchState(HostageStateType state)
        {
            switch (state)
            {
                case HostageStateType.Terrified :
                    CurrentInpcState = _terrified;
                    break;
                case HostageStateType.Follow:
                    CurrentInpcState = _follow;
                    break;
            }
            CurrentInpcState.EnterState();
        }
    }
}