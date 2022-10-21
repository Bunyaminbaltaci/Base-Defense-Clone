using Enums.Npc;
using UnityEngine;

namespace Controller.Npc.Boss
{
    public class BossAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator animator;
        [SerializeField] private BossManager bossManager;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        public void TriggerToAttack()
        {
            bossManager.Attack();
        }

        public void PrepareBomb()
        {
            bossManager.SetBomb();
        }

        public void SetTriggerAnim(BossAnimType animTypeType)
        {
            animator.SetTrigger(animTypeType.ToString());
        }
    }
}