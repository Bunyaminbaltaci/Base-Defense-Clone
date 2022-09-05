using System;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animatorController;

        #endregion

        #endregion


        public void PlayAnim(PlayerAnimationStates playerAnimationStates, float value)
        {
            animatorController.SetFloat(playerAnimationStates.ToString(),value);
        }
    }
}