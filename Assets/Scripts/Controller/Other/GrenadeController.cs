using System;
using Enums;
using Signals;
using UnityEngine;

namespace Controller
{
    public class GrenadeController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Collider collider;

        #endregion

        #region Private Variables

        #endregion

        #endregion


        public void InHand()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.isKinematic = true;
            rigidbody.freezeRotation = true;
        }

        public void Throw()
        {
            rigidbody.isKinematic = false;
            rigidbody.freezeRotation = false;
        }

        private void OnDisable()
        {
            InHand();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PoolSignals.Instance.onSendPool?.Invoke(gameObject, PoolType.Grenade);
            }

            if (other.CompareTag("Ground"))
            {
                PoolSignals.Instance.onSendPool?.Invoke(gameObject, PoolType.Grenade);
            }
        }
    }
}