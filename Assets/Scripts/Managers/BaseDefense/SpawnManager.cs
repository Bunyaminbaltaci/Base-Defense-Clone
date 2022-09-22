using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class SpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables
        [SerializeField] private List<GameObject> TargetList;
        [SerializeField] private List<GameObject> SpawnPoints;

        #endregion

        #region Private Variables


        #endregion

        #endregion

        
        private void Awake()
        {
            GetReferences();
        }


        private void GetReferences()
        {
         
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemy());
        }


        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
          
            BaseSignals.Instance.onGetTarget += OnGetTarget;
         
            
        }

        private void UnSubscribeEvent()
        {
    
            BaseSignals.Instance.onGetTarget -= OnGetTarget;
            
        }

   

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        IEnumerator SpawnEnemy()
        {
            WaitForSeconds timer = new WaitForSeconds(3f);
            while (true)
            {
                yield return timer;



                var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Enemy);
                if (obj!=null)
                {
                    obj.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position;
                    obj.transform.parent = transform.parent;
                    obj.SetActive(true);
                }


            }

          
        }


        private GameObject OnGetTarget() => TargetList[Random.Range(0, TargetList.Count)];
    }
}