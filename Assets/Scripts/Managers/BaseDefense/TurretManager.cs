using System;
using System.Collections.Generic;
using Controllers;
using Data;
using Datas.ValueObject;
using DG.Tweening;
using Keys;
using Signals;
using UnityEngine;

namespace Manager
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject stackHolder;

        #endregion

        #region Private Variables

        private List<GameObject> _bulletBoxList;
        private TurretData _data;
        private float _directY;
        private float _directZ;
        private float _directX;

        #endregion

        #endregion

        private void Awake()
        {
            _bulletBoxList = new List<GameObject>();
            
            _data = GetTurretData();
            
        }

        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
            BaseSignals.Instance.onHoldTurretData?.Invoke(stackHolder.transform.parent.gameObject,new TurretParams
            {
                StackLimit = _data.BulletBoxStackData.StackLimit,
                StackZone = stackHolder
            });
        }

        private void Subscribe()
        {
            BaseSignals.Instance.onSendAmmoInStack += OnSendAmmoInStack;
        }


        private void Unsubscribe()
        {
            BaseSignals.Instance.onSendAmmoInStack -= OnSendAmmoInStack;
        }


        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void Start()
        {
            BaseSignals.Instance.onHoldTurretData?.Invoke(stackHolder.transform.parent.gameObject,new TurretParams
            {
                StackLimit = _data.BulletBoxStackData.StackLimit,
                StackZone = stackHolder
            });
        }

      

        private TurretData GetTurretData() => Resources.Load<CD_TurretData>("Data/CD_TurretData").Data;


        private void OnSendAmmoInStack(GameObject target, GameObject bulletBox)
        {
            if (target == stackHolder.transform.parent.gameObject)
            {
                bulletBox.transform.parent = stackHolder.transform;
                SetObjPosition(bulletBox);
                _bulletBoxList.Add(bulletBox);
                BaseSignals.Instance.onHoldTurretData?.Invoke(stackHolder.transform.parent.gameObject,new TurretParams
                {
                    StackLimit = _data.BulletBoxStackData.StackLimit-_bulletBoxList.Count,
                    StackZone = stackHolder
                });
            }
            
        }

        private void SetObjPosition(GameObject obj)
        {
            _directX = _bulletBoxList.Count % _data.BulletBoxStackData.LimitX * _data.BulletBoxStackData.OffsetX;
            _directY = _bulletBoxList.Count / (_data.BulletBoxStackData.LimitX * _data.BulletBoxStackData.LimitZ) *
                       _data.BulletBoxStackData.OffsetY;
            _directZ = _bulletBoxList.Count % (_data.BulletBoxStackData.LimitX * _data.BulletBoxStackData.LimitZ) /
                _data.BulletBoxStackData.LimitX * _data.BulletBoxStackData.OffsetZ;
            obj.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ),
                _data.BulletBoxStackData.AnimationDurition);
            obj.transform.DORotate(Vector3.zero, 0);
        }
    }
}