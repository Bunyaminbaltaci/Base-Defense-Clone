using System;
using Manager;
using UnityEngine;

namespace Controller.AmmoArea
{
    public class AmmoAreaPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AmmoAreaManager ammoAreaManagermanager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
             ammoAreaManagermanager.StartCor(other);
            }

            if (other.CompareTag("Support"))
            {
                ammoAreaManagermanager.StartCor(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ammoAreaManagermanager.StopCor();
            } 
            if (other.CompareTag("Support"))
            {
                ammoAreaManagermanager.StopCor();
            }
        }
    }
}