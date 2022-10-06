using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using Enums;
using Signals;
using UnityEngine;

namespace Controller.Other
{
    public class TurretAttackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> Damageables = new List<GameObject>();

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject barrel;

        #endregion
        #region Private Variables

    
        private float _fireRate;

        #endregion

        #endregion

        public void SetFireRate(float fireRate)
        {
            _fireRate = fireRate;
        }

        public void RemoveFromList(GameObject obj)
        {
            if (!Damageables.Contains(obj)) return;
            Damageables.Remove(obj);
            Damageables.TrimExcess();
        }
        

        private IEnumerator Attack()
        {
            WaitForSeconds waiter = new WaitForSeconds(_fireRate);
        
            while (Damageables.Count > 0)
            {

                yield return waiter;
                if (Damageables==null)
                {
                 yield break;
                }


                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Bullet);
                obj.transform.position = barrel.transform.position;
                obj.transform.rotation = barrel.transform.rotation;
                obj.SetActive(true);
                obj.GetComponent<Rigidbody>().AddForce(barrel.transform.forward,ForceMode.VelocityChange);





            }
        }

        private void OnTriggerEnter(Collider other)
        {
            

            if (other.GetComponent<IDamageable>() != null && other.CompareTag("Enemy"))
            {
                Damageables.Add(other.gameObject);
                StartCoroutine(Attack());

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IDamageable>() != null && other.CompareTag("Enemy"))
            {
                Damageables.Remove(other.gameObject);
                Damageables.TrimExcess();

            }
        }
    }
}