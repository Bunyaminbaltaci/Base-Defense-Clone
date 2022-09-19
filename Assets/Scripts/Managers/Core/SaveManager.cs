using System.Collections.Generic;
using ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
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

        private void SaveBaseData(BaseDataParams baseDataParams)
        {
            if (baseDataParams.BaseLevel != null)
                ES3.Save("CityLevel",
                    baseDataParams.BaseLevel,
                    "BaseData.json");
            if (baseDataParams.AreaDictionary != null)
                ES3.Save("AreaDatas",
                    baseDataParams.AreaDictionary,
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

        private void SaveIdleData(IdleDataParams idleDataparams)
        {
            if (idleDataparams.MinerCount != null)
                ES3.Save("MinerCount",
                    idleDataparams.MinerCount,
                    "IdleData.json");
            if (idleDataparams.SoldierCount != null)
                ES3.Save("SoldierCount",
                    idleDataparams.SoldierCount,
                    "IdleData.json");
        }

        private BaseDataParams OnLoadBaseData()
        {
            return new BaseDataParams
            {
                AreaDictionary =
                    ES3.KeyExists("AreaDatas",
                        "BaseData.json")
                        ? ES3.Load<Dictionary<int, AreaData>>("AreaDatas",
                            "BaseData.json")
                        : new Dictionary<int, AreaData>(),
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

        private IdleDataParams OnLoadIdleData()
        {
            return new IdleDataParams()
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