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

        private FollowInpcState _followInpc;
        private TerrifiedInpcState _terrifiedInpc;
        
       

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
            CurrentInpcState = _terrifiedInpc;
        }

        private void GetReferences()
        {
            
            _followInpc = new FollowInpcState(this, ref agent);
            _terrifiedInpc = new TerrifiedInpcState(this, ref agent);
            CurrentInpcState = _terrifiedInpc;
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
                    CurrentInpcState = _terrifiedInpc;
                    break;
                case HostageStateType.Follow:
                    CurrentInpcState = _followInpc;
                    break;
            }
            CurrentInpcState.EnterState();
        }
    }
}