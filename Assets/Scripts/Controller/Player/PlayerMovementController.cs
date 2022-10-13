using System;
using Data;
using Enums;
using Keys;
using Manager;
using Managers.Core;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private PlayerData _playerMovementData;
        private InputParams _inputParams;
        private PlayerMovementState _movementState;
        private Vector3 _root;
        private Vector3 _movement;
        

        #endregion

        #endregion

        private void Awake()
        {
            _movementState = PlayerMovementState.Normal;
        }

        private void FixedUpdate()
        {
          ChangeMove();
        }

        public void SetMovementData(PlayerData dataMovementData)
        {
            _playerMovementData = dataMovementData;
        }

 
        public void UpdateInputValue(InputParams inputParam)
        {
            _inputParams = inputParam;
        }

      
        private void NormalMove()
        {
             _movement = new Vector3(_inputParams.Values.x * _playerMovementData.PlayerJoystickSpeed, 0,
                _inputParams.Values.z * _playerMovementData.PlayerJoystickSpeed);
            rigidbody.velocity = _movement;
            SetAnimAndRotation();
         
        }

        private void SetAnimAndRotation()
        {
         
            if (playerManager.Target==null)
            {
             
                playerManager.PlayAnim(PlayerAnimationStates.RunZ,
                    Mathf.Abs(_inputParams.Values.x) + Mathf.Abs(_inputParams.Values.z));
                var _newDirect = Quaternion.LookRotation(_movement);
                rigidbody.transform.GetChild(0).rotation = _newDirect;
            }
            else
            {
                rigidbody.transform.GetChild(0).LookAt(playerManager.Target.transform);
                _root =playerManager.Target.transform.position- playerManager.transform.position;
                playerManager.PlayAnim(PlayerAnimationStates.RunZ,
                    Mathf.Clamp((_root.z+_root.x) * _inputParams.Values.z,-1f,1f)); 
                playerManager.PlayAnim(PlayerAnimationStates.RunX,
                    Mathf.Clamp((_root.z+_root.x)* _inputParams.Values.x,-1f,1f));
            }
            
        }

        private void TurretMove()
        {
           
            if (_inputParams.Values.z<-0.6f)
            {
              
                playerManager.ChangeMovement(PlayerMovementState.Normal);
                BaseSignals.Instance.onPlayerOutTurret?.Invoke(transform.parent.transform.parent.gameObject);
            }
        }

        private void ChangeMove()
        {

            switch (_movementState)
            {
                case PlayerMovementState.Normal :
                    NormalMove();
                    break;
                case PlayerMovementState.Turret:
                    TurretMove();
                    break;
            }
        }
        public void ChangeState(PlayerMovementState state)
        {
            playerManager.PlayAnim(PlayerAnimationStates.RunZ,
                0);   
            playerManager.PlayAnim(PlayerAnimationStates.RunX,
                0);
            
            rigidbody.velocity=Vector3.zero;
            _movementState = state;
        }

        
    }
}