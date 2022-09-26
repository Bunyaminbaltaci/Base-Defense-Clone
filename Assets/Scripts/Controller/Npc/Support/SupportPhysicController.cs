using System;
using Manager;
using UnityEngine;

namespace Controller.Npc.Support
{
    public class SupportPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SupportManager supportManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            supportManager.CurrentState.OnTriggerEnterState(other);
        }

        private void OnTriggerExit(Collider other)
        {
            supportManager.CurrentState.OnTriggerExitState(other);

        }
    }
}