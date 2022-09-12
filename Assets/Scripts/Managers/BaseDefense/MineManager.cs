using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MineManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private List<GameObject> targetList;
        [SerializeField] private GameObject diamondHolder;

        #endregion

        #region Private Variables

        private List<GameObject> _diamondList;
        private int _count;
        private int _directY;
        private int _directZ;
        private int _directX;

        #endregion

        #endregion


        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            IdleSignals.Instance.onGetMineTarget += OnGetMineTarget;
            IdleSignals.Instance.onGetDiamondInStack += OnGetDiamondInStack;
            IdleSignals.Instance.onAddDiamondStack += OnAddDiamondStack;
        }


        private void UnSubscribeEvent()
        {
            IdleSignals.Instance.onGetMineTarget -= OnGetMineTarget;
            IdleSignals.Instance.onGetDiamondInStack -= OnGetDiamondInStack;
            IdleSignals.Instance.onAddDiamondStack -= OnAddDiamondStack;
        }


        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private GameObject OnGetMineTarget() => targetList[Random.Range(0, targetList.Count - 1)];

        private void OnGetDiamondInStack(Transform target)
        {
            if (_diamondList.Count <= 0) return;
            var obj = _diamondList[0];
            _diamondList.RemoveAt(0);
            _diamondList.TrimExcess();
            obj.transform.DOMove(target.position, 1f);
            PoolSignals.Instance.onSendPool?.Invoke(obj, PoolType.diamond);
            ScoreSignals.Instance.onAddDiamond?.Invoke(1);
        }

        private void OnAddDiamondStack()
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.diamond);
            obj.transform.parent = diamondHolder.transform;
            SetObjPosition(obj);
            obj.SetActive(true);
            _diamondList.Add(obj);
        }

        private void SetObjPosition(GameObject obj)
        {
            obj.transform.DOLocalRotate(Vector3.zero, 0);
            obj.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ), 0.5f);
            _directY = (int)(_diamondList.Count / 9);
            _directZ = (int)(_diamondList.Count / 3) % 9;
            _directX = (int)(_diamondList.Count % 3);
        }
    }
}