using System;
using System.Collections;
using Abstract;
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
        [SerializeField] private ParticleSystem explodePartical;
        [SerializeField] private GameObject model;

        #endregion

        #region Private Variables

        #endregion

        #endregion


        public void InHand()
        {
            model.SetActive(true);
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.isKinematic = true;
            rigidbody.freezeRotation = true;
            collider.enabled = false;
        }

        public void Throw()
        {
            rigidbody.isKinematic = false;
            rigidbody.freezeRotation = false;
            collider.enabled = true;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                StartCoroutine(Explode());
            }
        }

        IEnumerator Explode()
        {
            Collider[] bombArea = Physics.OverlapSphere(model.transform.position, 2.34f);
            foreach (var damageTaken in bombArea)
            {
                if (damageTaken.tag == "Player")
                {
                    damageTaken.GetComponentInParent<IDamageable>().Damage(35);
                    break;
                }
            }

            InHand();
            model.SetActive(false);
            explodePartical.Play();
            WaitForSeconds watier = new WaitForSeconds(2f);
            yield return watier;
            model.SetActive(true);
            PoolSignals.Instance.onSendPool?.Invoke(gameObject, PoolType.Grenade);
        }
    }
}