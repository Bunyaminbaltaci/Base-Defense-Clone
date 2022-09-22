using Enums.Npc;
using UnityEngine;

namespace Controller.Npc.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
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
        public void SetTriggerAnim(EnemyAnimType animType)
        {
            animator.SetTrigger(animType.ToString());
        }
        
    }
}