using System;
using UnityEngine;

namespace Controller.Other
{
    public class MoneyController : MonoBehaviour
    {
        #region Self Variables

        #region Seriazlized Varables

        [SerializeField] private Collider col;
        [SerializeField] private Collider col2;
        [SerializeField] private Rigidbody rb;

        #endregion

        #endregion
        public void isTaked()
        {
            rb.isKinematic = true;
            col.enabled = false;
            col2.enabled = false;
        }

        private void OnDisable()
        {
            rb.isKinematic = false;
            col.enabled = true;
            col2.enabled = true;
        }


        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.CompareTag("Player"))
        //     {
        //         isTaked();
        //     }
        //
        //     if (other.CompareTag("Harvester"))
        //     {
        //         isTaked();
        //     }
        // }
    }
}