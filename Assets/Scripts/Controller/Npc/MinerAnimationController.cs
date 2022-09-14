using Enums.Npc;
using UnityEngine;

namespace Managers.Npc
{
    public class MinerAnimationController : MonoBehaviour
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

        public void SetAnim(MinerAnimType animType)
        {
            animator.SetTrigger(animType.ToString());
        }
    }
}