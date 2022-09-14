using System;
using System.Collections;
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

        private List<GameObject> _diamondList = new List<GameObject>();
        private float _directY;
        private float _directZ;
        private float _directX;

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
            IdleSignals.Instance.onGetMineStackTarget += OnGetMineStackTarget;
            IdleSignals.Instance.onAddDiamondStack += OnAddDiamondStack;
        }


        private void UnSubscribeEvent()
        {
            IdleSignals.Instance.onGetMineTarget -= OnGetMineTarget;
            IdleSignals.Instance.onGetMineStackTarget -= OnGetMineStackTarget;
            IdleSignals.Instance.onAddDiamondStack -= OnAddDiamondStack;
        }


        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private GameObject OnGetMineTarget() =>
            targetList[Random.Range(0,
                targetList.Count - 1)];

        private GameObject OnGetMineStackTarget() =>
            diamondHolder;


        public void StartCollectDiamond(GameObject target)
        {
            int limit = _diamondList.Count;
            for (int i = 0;
                 i < limit;
                 i++)
            {
                var obj = _diamondList[0];
                _diamondList.RemoveAt(0);
                _diamondList.TrimExcess();
                obj.transform.DOMove(target.transform.position,
                        0.5f)
                    .OnComplete(() =>
                        {
                            PoolSignals.Instance.onSendPool?.Invoke(obj,
                                PoolType.diamond);
                        }
                    );


                ScoreSignals.Instance.onAddDiamond?.Invoke(1);
            }

            SaveSignals.Instance.onSaveScoreData?.Invoke();
        }


        private void OnAddDiamondStack(GameObject target)
        {
            GameObject obj = PoolSignals.Instance.onGetPoolObject(PoolType.diamond);
            obj.transform.parent = diamondHolder.transform;
            obj.transform.position = new Vector3(target.transform.position.x,
                target.transform.position.y + 1,
                target.transform.position.z);
            SetObjPosition(obj);
            obj.SetActive(true);
            _diamondList.Add(obj);
        }

        private void SetObjPosition(GameObject obj)
        {
            _directX = ((int)(_diamondList.Count % 3)) / 2f;
            _directY = ((int)(_diamondList.Count / 9)) / 2f;
            _directZ = ((int)(_diamondList.Count % 9) / 3) / 2f;
            obj.transform.DOLocalMove(new Vector3(_directX,
                    _directY,
                    _directZ),
                0.5f);
        }
    }
}