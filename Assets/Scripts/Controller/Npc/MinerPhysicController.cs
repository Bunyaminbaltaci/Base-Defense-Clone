using System;
using UnityEngine;

namespace Managers.Npc
{
    public class MinerPhysicController:MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private MinerManager minerManager;


        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            minerManager.CurrentState.OnCollisionDetectionState(other);
        }
    }
}