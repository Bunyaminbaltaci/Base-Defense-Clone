using DG.Tweening;
using Enums;
using UnityEngine;

namespace Controller
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animatorController;

        #endregion

        float endValue;

        #endregion


        public void ChangeLayer(LayerType layer)
        {
            if (layer==LayerType.Default)
            {
                SetLayer(AnimLayerType.UpperBody,0);
                return;
            }
            SetLayer(AnimLayerType.UpperBody,1);
            
            
        }

        public void PlayFloatAnim(PlayerAnimationStates playerAnimationStates, float value)
        {
            animatorController.SetFloat(playerAnimationStates.ToString(), value);
        }

        public void PlayTriggerAnim(PlayerAnimationStates playerAnimationStates)
        {
            animatorController.SetTrigger(playerAnimationStates.ToString());
        }

        private void SetLayer(AnimLayerType type, float weight)
        {
            animatorController.SetLayerWeight((int)type, weight);
        }
    }
}