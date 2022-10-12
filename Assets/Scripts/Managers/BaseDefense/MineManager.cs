using System.Collections.Generic;
using Data;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Manager
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

        private MineData _data;
        private readonly List<GameObject> _diamondList = new List<GameObject>();
        private int _minerCount;
        private float _directY;
        private float _directZ;
        private float _directX;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetMineData();
        }

        private MineData GetMineData()
        {
            return Resources.Load<CD_MineData>("Data/CD_MineData").Data;
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            BaseSignals.Instance.onGetMineTarget += OnGetMineTarget;
            BaseSignals.Instance.onGetMineStackTarget += OnGetMineStackTarget;
            BaseSignals.Instance.onAddDiamondStack += OnAddDiamondStack;
            BaseSignals.Instance.onGetMinerCapacity += OnGetMinerCapacity;
            BaseSignals.Instance.onAddMinerInMine += OnAddMinerInMine;
            BaseSignals.Instance.OnStartCollectDiamond += StartCollectDiamond;
        }

        private void UnSubscribeEvent()
        {
            BaseSignals.Instance.onGetMineTarget -= OnGetMineTarget;
            BaseSignals.Instance.onGetMineStackTarget -= OnGetMineStackTarget;
            BaseSignals.Instance.onAddDiamondStack -= OnAddDiamondStack;
            BaseSignals.Instance.onGetMinerCapacity -= OnGetMinerCapacity;
            BaseSignals.Instance.onAddMinerInMine -= OnAddMinerInMine;
            BaseSignals.Instance.OnStartCollectDiamond -= StartCollectDiamond;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private void Start()
        {
            _minerCount = BaseSignals.Instance.onGetMinerCount();
            Init();
        }

        private GameObject OnGetMineTarget()
        {
            return targetList[Random.Range(0, targetList.Count)];
        }

        private GameObject OnGetMineStackTarget()
        {
            return diamondHolder;
        }

        private int OnGetMinerCapacity()
        {
            return _data.MinerCapacity - _minerCount;
        }

        private void Init()
        {
            int count = _minerCount;
            for (var i = 0; i < count; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Miner);
                if (obj != null)
                {
                    obj.transform.position = diamondHolder.transform.position + Vector3.right;
                    obj.SetActive(true);
                    obj.transform.parent = transform;
                }
                else
                {
                    break;
                }
            }
        }

        public void StartCollectDiamond(GameObject target)
        {
            var limit = _diamondList.Count;
            for (var i = 0; i < limit; i++)
            {
                var obj = _diamondList[0];
                _diamondList.RemoveAt(0);
                _diamondList.TrimExcess();
                obj.transform.parent = target.transform;
                obj.transform.DOLocalMove(
                    new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 0.5f), Random.Range(-0.5f, 0.5f)), 0.5f);
                obj.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 0.5f).SetDelay(0.5f).OnComplete(() =>
                {
                    PoolSignals.Instance.onSendPool?.Invoke(obj, PoolType.Diamond);
                });
                ScoreSignals.Instance.onAddDiamond?.Invoke(1);
            }

            SaveSignals.Instance.onSaveScoreData?.Invoke();
        }

        private void OnAddDiamondStack(GameObject target)
        {
            var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Diamond);
            if (obj == null) return;
            obj.transform.parent = diamondHolder.transform;
            obj.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1,
                target.transform.position.z);
            SetObjPosition(obj);
            obj.SetActive(true);
            _diamondList.Add(obj);
        }

        private void SetObjPosition(GameObject obj)
        {
            _directX = _diamondList.Count % _data.SData.LimitX * _data.SData.OffsetX;
            _directY = _diamondList.Count / (_data.SData.LimitX * _data.SData.LimitZ) * _data.SData.OffsetY;
            _directZ = _diamondList.Count % (_data.SData.LimitX * _data.SData.LimitZ) / _data.SData.LimitX * _data.SData.OffsetZ;
            obj.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ), 0.5f);
        }

        private void OnAddMinerInMine(GameObject obj)
        {
            _minerCount++;
            obj.transform.parent = transform;
        }
    }
}