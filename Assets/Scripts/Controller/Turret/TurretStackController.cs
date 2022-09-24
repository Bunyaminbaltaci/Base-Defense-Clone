using System;
using Manager;
using UnityEngine;

namespace Controller.Turret
{
    public class TurretStackController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretManager turretManager;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                turretManager.StartTake(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
              turretManager.StopTake();
            }
        }
    }
}