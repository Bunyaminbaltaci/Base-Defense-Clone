using Abstract;
using Controller.WorkerArea;
using Data;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Controller
{
    public class WorkerAreaManager : MonoBehaviour, ISavable
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private WorkerBuyAreaController harvesterBuyArea;
        [SerializeField] private WorkerBuyAreaController supportBuyArea;

        #endregion

        #region Private Variables

        private WorkerAreaData _data;

        #endregion

        #endregion


        private void Awake()
        {
            _data = GetData();
          
        }

        private WorkerAreaData GetData() => Resources.Load<CD_WorkerAreaData>("Data/CD_WorkerAreaData").Data;


       


        #region Event Subscribe

        private void OnEnable()
        {
            EventSubscribe();
        }

        private void EventSubscribe()
        {
            SaveSignals.Instance.onGetWorkerAreapData += OnGetWorkerAreapData;
        }

        private void EventUnSubscribe()
        {
            SaveSignals.Instance.onGetWorkerAreapData += OnGetWorkerAreapData;
        }


        private void OnDisable()
        {
            EventUnSubscribe();
        }

        #endregion

        private void Start()
        {
            SetAreaText();
            LoadData();
        }

        private void SetAreaText()
        {
            harvesterBuyArea.CostText.text = _data.HarvesterCost.ToString();
            supportBuyArea.CostText.text = _data.SupportCost.ToString();
        }

        private WorkerDataParams OnGetWorkerAreapData()
        {
            return new WorkerDataParams
            {
                CapacityLevel = _data.WorkerDatas.CapacityLevel,
                SpeedLevel = _data.WorkerDatas.SpeedLevel,
                WorkerList = _data.WorkerDatas.WorkerList
            };
        }

        public void BuyHarvester()
        {
            var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Harvester);
            obj.transform.position = harvesterBuyArea.transform.position;
            obj.SetActive(true);
            harvesterBuyArea.IsComplete();
            if (!_data.WorkerDatas.WorkerList.ContainsKey(WorkerType.Harvester))
            {
                _data.WorkerDatas.WorkerList.Add(WorkerType.Harvester, true);
            }
            SaveData();
        }

        public void BuySupport()
        {
            
            var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Support);
            obj.transform.position = supportBuyArea.transform.position;
            obj.SetActive(true);
            supportBuyArea.IsComplete();
            if (!_data.WorkerDatas.WorkerList.ContainsKey(WorkerType.Support))
            {
                _data.WorkerDatas.WorkerList.Add(WorkerType.Support, true);
            }
            SaveData();
        }

        private void SetWorkerState()
        {
            foreach (var key in _data.WorkerDatas.WorkerList)
            {
               
                if (key.Key == WorkerType.Harvester && key.Value == true)
                {
                    BuyHarvester();
                    
                }

                if (key.Key == WorkerType.Support && key.Value == true)
                {
                    BuySupport();
                   
                }
            }
        }

        public void LoadData()
        {
            var value = SaveSignals.Instance.onLoadWorkerAreaData();
            _data.WorkerDatas.CapacityLevel = value.CapacityLevel;
            _data.WorkerDatas.SpeedLevel = value.SpeedLevel;
            _data.WorkerDatas.WorkerList = value.WorkerList;
          
            SetWorkerState();
        }


        public void SaveData()
        {
            SaveSignals.Instance.onSaveWorkerAreaData?.Invoke();
        }
    }
}