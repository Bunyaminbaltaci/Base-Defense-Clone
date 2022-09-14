using Commands.Pool;
using Data;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        private void Awake()
        {
            GetReferences();
            Init();
            StartPool();
        }

        private void GetReferences()
        {
            _cdPoolGenerator = GetPoolData();
        }

        private void Init()
        {
            _poolGenerateCommand =
                new PoolGenerateCommand(ref _cdPoolGenerator, ref poolManagerG, ref _emptyGameObject);
        }


        private void StartPool()
        {
            _poolGenerateCommand.Execute();
        }

        private CD_PoolGenerator GetPoolData()
        {
            return Resources.Load<CD_PoolGenerator>("Data/CD_PoolGenerator");
        }

        private GameObject OnGetPoolObject(PoolType poolType)
        {
            var parent = transform.GetChild((int)poolType);
            var obj = parent.childCount != 0
                ? parent.transform.GetChild(0).gameObject
                : null;
            return obj;
        }

        private void OnSendPool(GameObject poolObject, PoolType poolType)
        {
            poolObject.SetActive(false);
            poolObject.transform.localPosition = Vector3.zero;
            poolObject.transform.parent = transform.GetChild((int)poolType);
        }

        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform poolManagerG;

        #endregion

        #region Private Variables

        private CD_PoolGenerator _cdPoolGenerator;
        private GameObject _emptyGameObject;
        private PoolGenerateCommand _poolGenerateCommand;

        #endregion

        #endregion

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onSendPool += OnSendPool;
        }

        private void UnSubscribeEvent()
        {
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onSendPool -= OnSendPool;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion
    }
}