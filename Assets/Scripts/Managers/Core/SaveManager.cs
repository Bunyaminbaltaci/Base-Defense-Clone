using System.Collections.Generic;
using ValueObject;
using Keys;
using Signals;
using UnityEngine;

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
        }

        private void Unsubscribe()
        {
            SaveSignals.Instance.onSaveBaseData -= OnSaveBaseData;
            SaveSignals.Instance.onLoadBaseData -= OnLoadBaseData;
            SaveSignals.Instance.onSaveScoreData -= OnSaveScoreData;
            SaveSignals.Instance.onLoadScoreData -= OnLoadScoreData;
            SaveSignals.Instance.onSaveIdleData -= OnSaveIdleData;
            SaveSignals.Instance.onLoadIdleData -= OnLoadIdleData;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void OnSaveIdleData()
        {
            SaveIdleData(SaveSignals.Instance.onGetSaveIdleData());
        }

        private void OnSaveBaseData()
        {
            SaveBaseData(SaveSignals.Instance.onGetBaseData());
        }

        private void OnSaveScoreData()
        {
            SaveScoreData(SaveSignals.Instance.onGetSaveScoreData());
        }

        private void SaveBaseData(IdleDataParams ıdleDataParams)
        {
            if (ıdleDataParams.BaseLevel != null)
                ES3.Save("CityLevel",
                    ıdleDataParams.BaseLevel,
                    "BaseData.json");
            if (ıdleDataParams.AreaDictionary != null)
                ES3.Save("AreaDatas",
                    ıdleDataParams.AreaDictionary,
                    "BaseData.json");
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

        private void SaveIdleData(BaseDataParams baseDataparams)
        {
            if (baseDataparams.MinerCount != null)
                ES3.Save("MinerCount",
                    baseDataparams.MinerCount,
                    "IdleData.json");
            if (baseDataparams.SoldierCount != null)
                ES3.Save("SoldierCount",
                    baseDataparams.SoldierCount,
                    "IdleData.json");
        }

        private IdleDataParams OnLoadBaseData()
        {
            return new IdleDataParams
            {
                AreaDictionary =
                    ES3.KeyExists("AreaDatas",
                        "BaseData.json")
                        ? ES3.Load<Dictionary<string, AreaData>>("AreaDatas",
                            "BaseData.json")
                        : new Dictionary<string, AreaData>(),
                BaseLevel = ES3.KeyExists("CityLevel",
                    "BaseData.json")
                    ? ES3.Load<int>("CityLevel",
                        "BaseData.json")
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
                        "IdleData.json")
                        ? ES3.Load<int>("MinerCount",
                            "IdleData.json")
                        : 0,
                SoldierCount = ES3.KeyExists("SoldierCount",
                    "IdleData.json")
                    ? ES3.Load<int>("SoldierCount",
                        "IdleData.json")
                    : 0,
            };
        }
    }
}