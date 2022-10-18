using System;
using UnityEngine;

namespace Controller.Area
{
    public class AreaPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AreaManager areaManager;

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
               areaManager.StartBuy();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                areaManager.StopBuy();
            }
        }
    }
}