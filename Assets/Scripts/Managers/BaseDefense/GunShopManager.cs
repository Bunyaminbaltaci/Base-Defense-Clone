using System.Collections.Generic;
using Abstract;
using Data;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controller
{
    public class GunShopManager : MonoBehaviour, ISavable
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private Dictionary<GunType, GunData> _data = new Dictionary<GunType, GunData>();
        private GunType _gunType = GunType.Pistol;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetData();
        }

        private SerializedDictionary<GunType, GunData> GetData()
        {
            return Resources.Load<CD_GunShopData>("Data/CD_GunShopData").Data.GData;
        }


        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
            LoadData();
        }

        private void SubscribeEvent()
        {
            BaseSignals.Instance.onGetGunFireRate += OnGetGunFireRate;
            BaseSignals.Instance.onGetGunDamage += OnGetGunDamage;
            BaseSignals.Instance.onGetGunType += OnGetGunType;
            
            SaveSignals.Instance.onGetGunShopData += OnGetGunShopData;
        }


        private void UnSubscribeEvent()
        {
            BaseSignals.Instance.onGetGunFireRate -= OnGetGunFireRate;
            BaseSignals.Instance.onGetGunDamage -= OnGetGunDamage;
            BaseSignals.Instance.onGetGunType -= OnGetGunType;
            
            SaveSignals.Instance.onGetGunShopData -= OnGetGunShopData;
        }

      


        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private float OnGetGunFireRate() => _data[_gunType].FireRate;


        private GunsDataParams OnGetGunShopData() => new GunsDataParams
        {
            GData = _data,
            type = _gunType
        };

        private GunType OnGetGunType() => _gunType;
       
        private void ChangeWeapon()
        {
            BaseSignals.Instance.onGunDataSet?.Invoke();
        }
        private int OnGetGunDamage() => _data[_gunType].Damage *(_data[_gunType].GunLevel+1) ;


        public void LoadData()
        {
            var data = SaveSignals.Instance.onLoadGunShopData();
            if (data.GData != null)
            {
                foreach (var ky in data.GData)
                    _data[ky.Key].GunLevel = ky.Value.GunLevel;
            }
            _gunType = data.type;

        }

        public void SaveData()
        {
            SaveSignals.Instance.onSaveGunShopData?.Invoke();
        }
    }
}