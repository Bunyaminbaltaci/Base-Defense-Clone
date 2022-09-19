using System;
using Abstract;
using Enums.Npc;
using Managers.Npc;
using Managers.Npc.Hostage;
using States.MinerStates;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class HostageManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public HostageStateType HostageSType;
        public IStateMachine CurrentState;
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
            CurrentState.EnterState();
        }

        private void OnDisable()
        {
            Target = null;
            CurrentState = _terrified;
        }

        private void GetReferences()
        {
            
            _follow = new FollowState(this, ref agent);
            _terrified = new TerrifiedState(this, ref agent);
            CurrentState = _terrified;
        }

        private void Start()
        {
            CurrentState.EnterState();
        }

        private void Update()
        {
            CurrentState.UpdateState();
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
                    CurrentState = _terrified;
                    break;
                case HostageStateType.Follow:
                    CurrentState = _follow;
                    break;
            }
            CurrentState.EnterState();
        }
    }
}