using System;
using System.Collections.Generic;
using Datas.ValueObject;
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
            SaveSignals.Instance.onSaveData += OnSaveData;
        }

        private void Unsubscribe()
        {
            SaveSignals.Instance.onSaveData -= OnSaveData;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void OnSaveData()
        {
            SaveGame(SaveSignals.Instance.onGetIdleData());
        }

        private void Start()
        {
            LoadGame();
        }


        private void SaveGame(IdleDataParams idleDataParams)
        {
            if (idleDataParams.CityLevel != null)
                ES3.Save("CityLevel",
                    idleDataParams.CityLevel);
            if (idleDataParams.AreaDictionary != null)
                ES3.Save("AreaDatas",
                    idleDataParams.AreaDictionary);
        }

        private void LoadGame()
        {
            SaveSignals.Instance.onLoadIdleData?.Invoke(new IdleDataParams
            {
                AreaDictionary = ES3.KeyExists("AreaDatas")
                    ? ES3.Load<Dictionary<int, AreaData>>("AreaDatas")
                    : new Dictionary<int, AreaData>(),
                CityLevel = ES3.KeyExists("CityLevel")
                    ? ES3.Load<int>("CityLevel")
                    : 0,
            });
        }
    }
}