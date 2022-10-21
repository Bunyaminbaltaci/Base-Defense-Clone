using System;
using System.Collections;
using DG.Tweening;
using Enums;
using Enums.Npc;
using Signals;
using UnityEngine;

namespace Controller.Npc.Boss
{
    public class BossAttackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private BossManager bossManager;
        [SerializeField] private GameObject bombHolder;
        [SerializeField]private float throwTime = 2;

        #endregion

        #region Private Variables

        private GameObject _grenade;
        private float _currentY;
        private float _currentXZ;
        private float _velocityXZ;
        private float _velocityY;
        private Vector3 _differenceXZ;
        private Vector3 _difference;
        private Vector3 _result;

        #endregion

        #endregion


        public void PrepareBomb()
        {
            _grenade = PoolSignals.Instance.onGetPoolObject(PoolType.Grenade);
            _grenade.GetComponent<GrenadeController>().InHand();
            _grenade.transform.parent = bombHolder.transform;
            _grenade.transform.localPosition = Vector3.zero;
            _grenade.transform.DOLocalRotate(Vector3.zero, 0);
            _grenade.SetActive(true);
        }

        private Vector3 VelocityCalculate()
        {
            _difference = bossManager.Target.transform.position - _grenade.transform.position;
            _differenceXZ = _difference;
            _differenceXZ.Normalize();
            _differenceXZ.y = 0f;
            _currentY = _difference.y;
            _currentXZ = _difference.magnitude;
            _velocityXZ = _currentXZ / throwTime;
            _velocityY = _currentY / throwTime + 0.5f * Mathf.Abs(Physics.gravity.y) * throwTime;
            _result = _differenceXZ * _velocityXZ;
            _result.y = _velocityY;
            return _result;
        }

        public void ThrowBomb()
        {
            _grenade.transform.parent = BaseSignals.Instance.onGetBase().transform;
            _grenade.GetComponent<GrenadeController>().Throw();
            _grenade.GetComponent<Rigidbody>().AddForce(VelocityCalculate(), ForceMode.VelocityChange);
            BaseSignals.Instance.onSetCrosshair?.Invoke(bossManager.Target.transform.position);
        }
        
    }
}