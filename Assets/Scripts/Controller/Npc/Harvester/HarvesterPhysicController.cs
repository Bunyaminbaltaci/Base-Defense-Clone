using System;
using Controller;
using UnityEngine;

namespace Controller.Npc.Harvester
{
    public class HarvesterPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private HarvesterManager harvesterManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            harvesterManager.CurrentState.OnTriggerEnterState(other);
        }

        private void OnTriggerExit(Collider other)
        {
            harvesterManager.CurrentState.OnTriggerExitState(other);

        }
    }
}