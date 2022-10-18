using Enums.Npc;
using Controller;
using UnityEngine;

namespace Controller.Npc
{
    public class SupportAnimationController : MonoBehaviour
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