using System.Collections;
using System.Collections.Generic;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private List<GameObject> enemySpawnPoints;
        [SerializeField] private GameObject hostageSpawnPointParent;

        #endregion

        #region Private Variables

        private int _spawnAreaPointer;
        private Coroutine _hostageSpawnCoroutine;
        [ShowInInspector] private List<HostageSpawnParams> _hostageSpawnPoints;

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _hostageSpawnPoints = new List<HostageSpawnParams>();

            foreach (Transform child in hostageSpawnPointParent.transform)
            {
                _hostageSpawnPoints.Add(new HostageSpawnParams
                {
                    Spawpoint = child.gameObject,
                    Hostage = null
                });
            }
        }

        #region Event Subscribe

        private void OnEnable()
        {
            EventSubscribe();
        }

        private void EventSubscribe()
        {
            BaseSignals.Instance.onRemoveHostageInSpawnPoint += OnRemoveHostageInSpawnPoint;
        }

        private void EventUnSubscribe()
        {
            BaseSignals.Instance.onRemoveHostageInSpawnPoint -= OnRemoveHostageInSpawnPoint;
        }


        private void OnDisable()
        {
            EventUnSubscribe();
        }

        #endregion

        private void Start()
        {
            StartCoroutine(StartEnemySpawn());
            _hostageSpawnCoroutine = StartCoroutine(StartHostageSpawn());
        }

        private void OnRemoveHostageInSpawnPoint(GameObject hostage)
        {
            for (int i = 0; i < _hostageSpawnPoints.Count; i++)
            {
                if (_hostageSpawnPoints[i].Hostage == hostage)
                    _hostageSpawnPoints[i] = new HostageSpawnParams
                    {
                        Hostage = null,
                        Spawpoint = _hostageSpawnPoints[i].Spawpoint
                    };
            }

            if (_hostageSpawnCoroutine == null)
            {
                _hostageSpawnCoroutine = StartCoroutine(StartHostageSpawn());
            }
        }


        private IEnumerator StartEnemySpawn()
        {
            var waiter = new WaitForSeconds(2.5f);
            while (true)
            {
                var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Enemy);
                if (obj != null)
                {
                    obj.transform.position =
                        enemySpawnPoints[Random.Range(0,
                                enemySpawnPoints.Count)]
                            .transform.position;
                    obj.transform.parent = transform.parent;
                    obj.SetActive(true);
                }

                yield return waiter;
            }
        }

        private IEnumerator StartHostageSpawn()
        {
            var waiter = new WaitForSeconds(5f);

            for (int i = 0; i < _hostageSpawnPoints.Count; i++)
            {
                if (_hostageSpawnPoints[i].Hostage != null) continue;
                GameObject obj = PoolSignals.Instance.onGetPoolObject(PoolType.Hostage);
                if (obj == null)
                    break;
                obj.transform.position = _hostageSpawnPoints[i].Spawpoint.transform.position;
                obj.transform.parent = transform.parent;
                obj.SetActive(true);
                _hostageSpawnPoints[i] = new HostageSpawnParams
                {
                    Hostage = obj,
                    Spawpoint = _hostageSpawnPoints[i].Spawpoint
                };
                yield return waiter;
            }

            _hostageSpawnCoroutine = null;
        }
    }
}