using Enums;
using Enums.Npc;
using UnityEngine;

namespace Manager.Npc
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
        public void SetLayer(AnimLayerType type,float weight )
        {
            animator.SetLayerWeight((int)type,weight);
        }   
        public void SetBoolAnim(MinerAnimType animType,bool check )
        {
            animator.SetBool(animType.ToString(),check);
        }
    }
}