using System.Collections.Generic;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controller
{
    public class BulletAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

       [ShowInInspector] private Dictionary<GameObject, TurretStackParams> _turretStackDatas;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _turretStackDatas = new Dictionary<GameObject, TurretStackParams>();
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            BaseSignals.Instance.onGetTurretStack += OnGetTurretStack;
            BaseSignals.Instance.onHoldTurretData += OnHoldTurretData;
            BaseSignals.Instance.onGetTurretLimit += OnGetTurretLimit;

            BaseSignals.Instance.onGetBulletBox += OnGetBulletBox;
        }

        private void UnSubscribeEvent()
        {
            BaseSignals.Instance.onHoldTurretData -= OnHoldTurretData;
            BaseSignals.Instance.onGetTurretStack -= OnGetTurretStack;
            BaseSignals.Instance.onGetTurretLimit -= OnGetTurretLimit;

            BaseSignals.Instance.onGetBulletBox -= OnGetBulletBox;
        }


        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private int OnGetTurretLimit(GameObject obj)
        {
            return _turretStackDatas[obj]
                .StackLimit;
        }

        private GameObject OnGetTurretStack()
        {
            var limit = 0;
            GameObject obj = null;

            foreach (var Key in _turretStackDatas)
                if (limit <
                    _turretStackDatas[Key.Key]
                        .StackLimit)
                {
                    limit = _turretStackDatas[Key.Key]
                        .StackLimit;
                    obj = _turretStackDatas[Key.Key]
                        .StackZone;
                }

            return obj;
        }

        private void OnHoldTurretData(GameObject obj,
            TurretStackParams turretStackParams)
        {
            if (_turretStackDatas.ContainsKey(obj))
                _turretStackDatas[obj] = new TurretStackParams
                {
                    StackLimit = turretStackParams.StackLimit,
                    StackZone = _turretStackDatas[obj].StackZone
                  
                };
            else
                _turretStackDatas.Add(obj,
                    new TurretStackParams
                    {
                        StackLimit = turretStackParams.StackLimit,
                        StackZone = turretStackParams.StackZone
                    });
        }

        private GameObject OnGetBulletBox()
        {
            var waiter = new WaitForSeconds(0.2f);

            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.BulletBox);
            if (obj == null)
                return null;
            obj.transform.position = transform.position;
            obj.SetActive(true);
            return obj;
        }
    }
}