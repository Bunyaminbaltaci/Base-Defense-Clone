using System;
using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AmmoSpawnerManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Varibles

        private AmmoSpawner _ammoSpawnerData;
        private List<GameObject> _ammoList;

        #endregion

        #endregion


        private void Start()
        {
            StartCoroutine(Spawner());
        }

        IEnumerator Spawner()
        {
            while (true)
            {
              

                if (_ammoList.Count<=_ammoSpawnerData.SpawnLimit)
                {
                    _ammoList.Add(PoolSignals.Instance.onGetPoolObject(PoolType.Bullet));
                    _ammoList[_ammoList.Count - 1].transform.parent =transform;
                    _ammoList[_ammoList.Count - 1].transform.localPosition = new Vector3(0,_ammoList.Count,0);

                }   
                yield return new WaitForSeconds(_ammoSpawnerData.SpawnTime);
                
                
                
            }
            
            
        }
    }
}