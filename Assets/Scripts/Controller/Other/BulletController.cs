using System;
using Abstract;
using Enums;
using Signals;
using Unity.Mathematics;
using UnityEngine;

namespace Controller.Other
{
    public class BulletController : MonoBehaviour
    {
        #region SelfVariables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Rigidbody rb;
        [SerializeField] private BulletType bulletType = BulletType.Turret;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void OnDisable()
        {
            rb.velocity = Vector3.zero;
        }

        private int getDamage()
        {
            if (bulletType == BulletType.Turret)
            {
                return (int)BaseSignals.Instance.onGetTurretDamage?.Invoke();
            }

            return (int)BaseSignals.Instance.onGetGunDamage?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable;
            if (other.TryGetComponent<IDamageable>(out damageable))
            {
                PoolSignals.Instance.onSendPool?.Invoke(gameObject, PoolType.Bullet);
                damageable.Damage(getDamage());
            }
        }
    }
}