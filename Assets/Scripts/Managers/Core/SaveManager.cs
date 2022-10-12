using System.Collections.Generic;
using Datas.ValueObject;
using Enums;
using ValueObject;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.Rendering;

namespace Manager
{
    public class SaveManager : MonoBehaviour
    {
        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            SaveSignals.Instance.onSaveBaseData += OnSaveBaseData;
            SaveSignals.Instance.onLoadBaseData += OnLoadBaseData;
            SaveSignals.Instance.onSaveScoreData += OnSaveScoreData;
            SaveSignals.Instance.onLoadScoreData += OnLoadScoreData;
            SaveSignals.Instance.onSaveIdleData += OnSaveIdleData;
            SaveSignals.Instance.onLoadIdleData += OnLoadIdleData;
            SaveSignals.Instance.onSaveGunShopData += OnSaveGunShopData;
            SaveSignals.Instance.onLoadGunShopData += OnLoadGunShopData;
            SaveSignals.Instance.onSaveWorkerAreaData += OnSaveWorkerAreaData;
            SaveSignals.Instance.onLoadWorkerAreaData += onLoadWorkerAreaData;
        }

        private void Unsubscribe()
        {
            SaveSignals.Instance.onSaveBaseData -= OnSaveBaseData;
            SaveSignals.Instance.onLoadBaseData -= OnLoadBaseData;
            SaveSignals.Instance.onSaveScoreData -= OnSaveScoreData;
            SaveSignals.Instance.onLoadScoreData -= OnLoadScoreData;
            SaveSignals.Instance.onSaveIdleData -= OnSaveIdleData;
            SaveSignals.Instance.onLoadIdleData -= OnLoadIdleData;
            SaveSignals.Instance.onSaveGunShopData -= OnSaveGunShopData;
            SaveSignals.Instance.onLoadGunShopData -= OnLoadGunShopData;
            SaveSignals.Instance.onSaveWorkerAreaData -= OnSaveWorkerAreaData;
            SaveSignals.Instance.onLoadWorkerAreaData -= onLoadWorkerAreaData;
        }


        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void OnSaveWorkerAreaData()
        {
            SaveWorkerData(SaveSignals.Instance.onGetWorkerAreapData());
        }

        private void OnSaveIdleData()
        {
            SaveBaseData(SaveSignals.Instance.onGetSaveIdleData());
        }

        private void OnSaveBaseData()
        {
            SaveIdleData(SaveSignals.Instance.onGetBaseData());
        }

        private void OnSaveScoreData()
        {
            SaveScoreData(SaveSignals.Instance.onGetSaveScoreData());
        }

        private void OnSaveGunShopData()
        {
            SaveGunShopData(SaveSignals.Instance.onGetGunShopData());
        }

        #region Save Data

        private void SaveWorkerData(WorkerDataParams workerAreaData)
        {
            if (workerAreaData.WorkerList != null)
                ES3.Save("WorkerList",
                    workerAreaData.WorkerList,
                    "WorkerAreaData.json");
            if (workerAreaData.CapacityLevel != null)
                ES3.Save("CapacityLevel",
                    workerAreaData.CapacityLevel,
                    "WorkerAreaData.json");
            if (workerAreaData.SpeedLevel != null)
                ES3.Save("SpeedLevel",
                    workerAreaData.SpeedLevel,
                    "WorkerAreaData.json");
        }

        private void SaveGunShopData(GunsDataParams gunsDataParams)
        {
            if (gunsDataParams.GData != null)
                ES3.Save("GData",
                    gunsDataParams.GData,
                    "GunShopData.json");
            if (gunsDataParams.type != null)
                ES3.Save("Type",
                    gunsDataParams.type,
                    "GunShopData.json");
        }


        private void SaveIdleData(IdleDataParams ıdleDataParams)
        {
            if (ıdleDataParams.BaseLevel != null)
                ES3.Save("CityLevel",
                    ıdleDataParams.BaseLevel,
                    "IdleData.json");
            if (ıdleDataParams.AreaDictionary != null)
                ES3.Save("AreaDatas",
                    ıdleDataParams.AreaDictionary,
                    "IdleData.json");
        }

        private void SaveScoreData(ScoreDataParams scoreDataParams)
        {
            if (scoreDataParams.Money != null)
                ES3.Save("Money",
                    scoreDataParams.Money,
                    "ScoreData.json");
            if (scoreDataParams.Diamond != null)
                ES3.Save("Diamond",
                    scoreDataParams.Diamond,
                    "ScoreData.json");
        }

        private void SaveBaseData(BaseDataParams baseDataparams)
        {
            if (baseDataparams.MinerCount != null)
                ES3.Save("MinerCount",
                    baseDataparams.MinerCount,
                    "BaseData.json");
            if (baseDataparams.SoldierCount != null)
                ES3.Save("SoldierCount",
                    baseDataparams.SoldierCount,
                    "BaseData.json");
            if (baseDataparams.TurretIsAuto != null)
            {
                ES3.Save("AutoTurret",
                    baseDataparams.TurretIsAuto,
                    "BaseData.json");
            }
        }

        #endregion

        #region Load Data

        private IdleDataParams OnLoadBaseData()
        {
            return new IdleDataParams
            {
                AreaDictionary =
                    ES3.KeyExists("AreaDatas",
                        "IdleData.json")
                        ? ES3.Load<Dictionary<string, AreaData>>("AreaDatas",
                            "IdleData.json")
                        : new Dictionary<string, AreaData>(),
                BaseLevel = ES3.KeyExists("CityLevel",
                    "IdleData.json")
                    ? ES3.Load<int>("CityLevel",
                        "IdleData.json")
                    : 0
            };
        }

        private ScoreDataParams OnLoadScoreData()
        {
            return new ScoreDataParams
            {
                Money = ES3.KeyExists("Money",
                    "ScoreData.json")
                    ? ES3.Load<int>("Money",
                        "ScoreData.json")
                    : 0,
                Diamond = ES3.KeyExists("Diamond",
                    "ScoreData.json")
                    ? ES3.Load<int>("Diamond",
                        "ScoreData.json")
                    : 0
            };
        }

        private BaseDataParams OnLoadIdleData()
        {
            return new BaseDataParams()
            {
                MinerCount =
                    ES3.KeyExists("MinerCount",
                        "BaseData.json")
                        ? ES3.Load<int>("MinerCount",
                            "BaseData.json")
                        : 0,
                SoldierCount = ES3.KeyExists("SoldierCount",
                    "BaseData.json")
                    ? ES3.Load<int>("SoldierCount",
                        "BaseData.json")
                    : 0,
                TurretIsAuto = ES3.KeyExists("AutoTurret",
                    "BaseData.json")
                    ? ES3.Load<Dictionary<string, bool>>("AutoTurret",
                        "BaseData.json")
                    : new Dictionary<string, bool>(),
            };
        }

        private GunsDataParams OnLoadGunShopData()
        {
            return new GunsDataParams
            {
                type =
                    ES3.KeyExists("Type",
                        "GunShopData.json")
                        ? ES3.Load<GunType>("Type",
                            "GunShopData.json")
                        : 0,
                GData = ES3.KeyExists("GData",
                    "GunShopData.json")
                    ? ES3.Load<Dictionary<GunType, GunData>>("GData",
                        "GunShopData.json")
                    : null,
            };
        }

        private WorkerDataParams onLoadWorkerAreaData()
        {
            return new WorkerDataParams
            {
                WorkerList = ES3.KeyExists("WorkerList", "WorkerAreaData.json")
                    ? ES3.Load<Dictionary<WorkerType, bool>>("WorkerList",
                        "WorkerAreaData.json")
                    : new Dictionary<WorkerType, bool>(),
                CapacityLevel = ES3.KeyExists("CapacityLevel",
                    "WorkerAreaData.json")
                    ? ES3.Load<int>("CapacityLevel",
                        "WorkerAreaData.json")
                    : 0,
                SpeedLevel = ES3.KeyExists("SpeedLevel",
                    "WorkerAreaData.json")
                    ? ES3.Load<int>("SpeedLevel",
                        "WorkerAreaData.json")
                    : 0,
            };
        }

        #endregion
    }
}