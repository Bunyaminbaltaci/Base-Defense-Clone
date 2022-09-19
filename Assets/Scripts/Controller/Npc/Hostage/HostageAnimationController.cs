using Enums.Npc;
using UnityEngine;

namespace Managers.Npc.Hostage
{
    public class HostageAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables


        #endregion

        #endregion

        public void SetTriggerAnim(HostageAnimType animType)
        {
            animator.SetTrigger(animType.ToString());
        }
        public void SetBoolAnim(HostageAnimType animType,bool check )
        {
            animator.SetBool(animType.ToString(),check);
        }
    }
}