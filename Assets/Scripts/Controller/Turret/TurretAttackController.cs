using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using Enums;
using Controller;
using Signals;
using UnityEngine;

namespace Controller
{
    public class TurretAttackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> Damageables = new List<GameObject>();

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject barrel;
        [SerializeField] private TurretManager turretManager;

        #endregion

        #region Private Variables

        private float _fireRate;
        private int _magazineChanger = 0;

        #endregion

        #endregion


        public void SetFireRate(float fireRate)
        {
            _fireRate = fireRate;
        }

        public void RemoveFromList(GameObject obj)
        {
            if (!Damageables.Contains(obj)) return;
            int index = Damageables.IndexOf(obj);
            Damageables.Remove(obj);
            Damageables.TrimExcess();
            if (index == 0) turretManager.Target = null;
        }


        public IEnumerator Attack()
        {
            WaitForSeconds waiter = new WaitForSeconds(_fireRate);


            if (turretManager.TurretType != TurretState.None)
                while (Damageables.Count > 0)
                {
                    if (turretManager.CheckStack())
                    {

                        if (Damageables == null)
                        {
                            turretManager.AttackCoroutine = null;
                            yield break;
                        }


                        Fire();
                        SetMagazine();
                    }

                    yield return waiter;
                }
            turretManager.AttackCoroutine = null;
        }

        private void Fire()
        {
            var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Bullet);
            if (obj != null)
            {
                SetBullet(obj);
                obj.GetComponent<Rigidbody>().AddForce(barrel.transform.forward * 5, ForceMode.VelocityChange);
            }
        }

        private void SetMagazine()
        {
            _magazineChanger++;
            if (_magazineChanger >= 4)
            {
                _magazineChanger = 0;
                turretManager.DeleteBulletBox();
            }
        }

        private void SetBullet(GameObject obj)
        {
            obj.transform.position = barrel.transform.position;
            obj.transform.rotation = barrel.transform.rotation;
            obj.transform.parent = BaseSignals.Instance.onGetBase?.Invoke().transform;
            obj.transform.Rotate(Vector3.right * 90);
            obj.SetActive(true);
        }

        private void StartToTurret()
        {
            if (turretManager.AttackCoroutine == null)
            {
                turretManager.AttackCoroutine = StartCoroutine(Attack());
            }

            if (turretManager.TurretType == TurretState.AutoMode && turretManager.LockCoroutine == null)
            {
                turretManager.LockTarget();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IDamageable>() != null && other.CompareTag("Enemy"))
            {
                Damageables.Add(other.gameObject);
                StartToTurret();
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IDamageable>() != null && other.CompareTag("Enemy"))
            {
                RemoveFromList(other.gameObject);
            }
        }
    }
}