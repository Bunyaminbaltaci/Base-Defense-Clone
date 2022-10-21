using System;
using UnityEngine;

namespace Controller.Npc.Boss
{
    public class BossPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private BossManager bossManager;
        #endregion

        #region Private Variables

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            bossManager.CurrentState.OnTriggerEnterState(other);
        }

        private void OnTriggerExit(Collider other)
        {
            bossManager.CurrentState.OnTriggerExitState(other);
        }
    }
}