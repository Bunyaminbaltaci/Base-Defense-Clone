using System.Collections;
using System.Collections.Generic;
using Abstract;
using Enums;
using Managers.Core;
using Signals;
using UnityEngine;

namespace Controller
{
    public class PlayerAttackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> Damageables = new List<GameObject>();
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private List<GameObject> barrel;
        #endregion

        #region Private Variables

        private GunType _weaponType;
        private float _fireRate;
        private Coroutine _attackCoroutine;

        #endregion

        #endregion

        public void ChangeGunType(GunType gun)
        {
            foreach (var weapons in barrel)
            {
                weapons.SetActive(false);
            }
            _weaponType = gun;
            barrel[(int)_weaponType].transform.parent.gameObject.SetActive(true);
            SetPlayerHand();
        }



        public void GunDataSet()
        {
            SetFireRate(BaseSignals.Instance.onGetGunFireRate());
            ChangeGunType(BaseSignals.Instance.onGetGunType());
            
        }
        public void ChangeLayer(LayerType type)
        {
            Damageables.Clear();
            gameObject.layer = LayerMask.NameToLayer(type.ToString());
        }

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
            if (index == 0)
            {
                playerManager.Target = null;
            }
            setTarget();
        }
         IEnumerator Attack()
        {
            WaitForSeconds waiter = new WaitForSeconds(_fireRate);


           
                while (Damageables.Count > 0)
                {
                    

                        if (Damageables == null)
                        {
                            _attackCoroutine = null;
                            yield break;
                        }
                        setTarget();


                        Fire();


                        yield return waiter;
                }
                _attackCoroutine = null;

        
        }

        private void setTarget()
        {
            if (playerManager.Target==null && Damageables.Count>0)
            {
                playerManager.Target = Damageables[0];
            }
        }
        private void SetPlayerHand()
        {
            switch (_weaponType)
            {
                case GunType.Pistol :
                    playerManager.PlayTriggerAnim(PlayerAnimationStates.Pistol);
                    break;
                case GunType.Ak:
                    playerManager.PlayTriggerAnim(PlayerAnimationStates.Rifle);
                    break;
                case GunType.Shotgun:
                    playerManager.PlayTriggerAnim(PlayerAnimationStates.Shotgun);
                    break;
                
            }
        }

        private void Fire()
        {
            var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Ammo);
            if (obj != null)
            {
                SetBullet(obj);
                obj.GetComponent<Rigidbody>().AddForce(barrel[(int)_weaponType].transform.forward * 5, ForceMode.VelocityChange);
            }
        }
      
        private void SetBullet(GameObject obj)
        {
            obj.transform.position = barrel[(int)_weaponType].transform.position;
            obj.transform.rotation = barrel[(int)_weaponType].transform.rotation;
            obj.transform.Rotate(Vector3.right * 90);
            obj.transform.parent = BaseSignals.Instance.onGetBase?.Invoke().transform;
            obj.SetActive(true);
        }
        private void StartToAttack()
        {
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(Attack());
            }

         
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IDamageable>() != null && other.CompareTag("Enemy"))
            {
                Damageables.Add(other.gameObject);
                StartToAttack();
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