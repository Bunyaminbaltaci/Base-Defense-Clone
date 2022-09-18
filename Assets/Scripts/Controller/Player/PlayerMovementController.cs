using Data;
using Enums;
using Keys;
using Managers;
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

        [Header("Data")] private PlayerMovementData _playerMovementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private InputParams _inputParams;

        #endregion

        #endregion

        private void FixedUpdate()
        {
            JoystickMove();
        }

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            _playerMovementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }

        public void UpdateInputValue(InputParams inputParam)
        {
            _inputParams = inputParam;
        }

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        private void JoystickMove()
        {
            var _movement = new Vector3(_inputParams.Values.x * _playerMovementData.PlayerJoystickSpeed, 0,
                _inputParams.Values.z * _playerMovementData.PlayerJoystickSpeed);
            rigidbody.velocity = _movement;
            playerManager.PlayAnim(PlayerAnimationStates.Run,
                Mathf.Abs(_inputParams.Values.x) + Mathf.Abs(_inputParams.Values.z));
            if (_movement != Vector3.zero)
            {
                var _newDirect = Quaternion.LookRotation(_movement);
                rigidbody.transform.GetChild(0).rotation = _newDirect;
            }
        }

        public void OnReset()
        {
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}