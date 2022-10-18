using Abstract;
using Enums;
using Signals;
using UnityEngine;

namespace Controller
{
    public class AmmoController : MonoBehaviour
    {
        #region SelfVariables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Rigidbody rb;
       

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
            return (int)BaseSignals.Instance.onGetGunDamage?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable;
            if (other.CompareTag("Enemy"))
            {
                damageable = other.GetComponent<IDamageable>();
                PoolSignals.Instance.onSendPool?.Invoke(gameObject, PoolType.Ammo);
                damageable.Damage(getDamage());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("AttackArea"))
            {
                PoolSignals.Instance.onSendPool?.Invoke(gameObject, PoolType.Ammo);
            }
        }
    }
}