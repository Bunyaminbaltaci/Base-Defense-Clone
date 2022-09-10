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
            SaveSignals.Instance.onSaveIdleData += onSaveIdleData;
            SaveSignals.Instance.onSaveScoreData += onSaveScoreData;
            SaveSignals.Instance.onLoadIdleData += OnLoadIdleData;
            SaveSignals.Instance.onLoadScoreData += OnLoadScoreData;
        }

        private void Unsubscribe()
        {
            SaveSignals.Instance.onSaveIdleData -= onSaveIdleData;
            SaveSignals.Instance.onSaveScoreData -= onSaveScoreData;
            SaveSignals.Instance.onLoadIdleData -= OnLoadIdleData;
            SaveSignals.Instance.onLoadScoreData -= OnLoadScoreData;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void OnSaveData()
        {
            SaveScoreData(SaveSignals.Instance.onGetSaveScoreData());
        }

        private void onSaveIdleData()
        {
            SaveIdleData(SaveSignals.Instance.onGetSaveIdleData());
        }

        private void onSaveScoreData()
        {
            SaveScoreData(SaveSignals.Instance.onGetSaveScoreData());
        }

        private void SaveIdleData(IdleDataParams idleDataParams)
        {
            if (idleDataParams.CityLevel != null)
                ES3.Save("CityLevel",
                    idleDataParams.CityLevel,
                    "IdleData.json");
            if (idleDataParams.AreaDictionary != null)
                ES3.Save("AreaDatas",
                    idleDataParams.AreaDictionary,
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

        private IdleDataParams OnLoadIdleData()
        {
            return new IdleDataParams
            {
                AreaDictionary =
                    ES3.KeyExists("AreaDatas",
                        "IdleData.json")
                        ? ES3.Load<Dictionary<int, AreaData>>("AreaDatas",
                            "IdleData.json")
                        : new Dictionary<int, AreaData>(),
                CityLevel = ES3.KeyExists("CityLevel",
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
    }
}