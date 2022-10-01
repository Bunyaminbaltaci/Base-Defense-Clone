using Enums.Npc;
using UnityEngine;

namespace Controller.Npc
{
    public class HarvesterAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #endregion
        
        public void SetAnim(WorkerAnimType animType)
        {
            animator.SetTrigger(animType.ToString());
        }  
    }
}