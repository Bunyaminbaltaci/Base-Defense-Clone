using System.Collections.Generic;
using Enums;
using Enums.Npc;
using Managers.Core;
using Signals;
using UnityEngine;

namespace Controller
{
    public class HostageCollectorController
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables


        #endregion

        #region Private Variables

        private PlayerManager _playerManager;
        private List<GameObject> _hostageList=new List<GameObject>();

        #endregion

        #endregion

        public HostageCollectorController(ref PlayerManager manager)
        {
            _playerManager = manager;

        }

   
        
        public GameObject GetHostageTarget(GameObject hostage)
        {
            if (_hostageList.Count == 0)
            {
                _hostageList.Add(hostage);
                return _playerManager.transform.gameObject;  
            }

            _hostageList.Add(hostage);
            return _hostageList[_hostageList.Count - 2];
        }

        public void DropFollowers()
        {
            for (int i = _hostageList.Count-1; i >=0; i--)
            {
                _hostageList[i].GetComponent<HostageManager>().SwitchState(HostageStateType.Terrified);
            }
            
        }

        public void HostageAddMine()
        {
            while (BaseSignals.Instance.onGetMinerCapacity() > 0 && _hostageList.Count > 0)
            {
                var lastHostage = _hostageList.Count - 1;
                var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Miner);
                obj.transform.position = _hostageList[lastHostage].transform.position;
                obj.transform.rotation = _hostageList[lastHostage].transform.rotation;
                PoolSignals.Instance.onSendPool(_hostageList[lastHostage], PoolType.Hostage);
                obj.SetActive(true);
                BaseSignals.Instance.onAddMinerInMine?.Invoke(obj);
                _hostageList.RemoveAt(lastHostage);
                _hostageList.TrimExcess();
            }
        }
    }
}