using System.Collections;
using System.Collections.Generic;
using Controller.Turret;
using Data;
using Datas.ValueObject;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Manager
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretStackController stackController;
        [SerializeField] private GameObject stackHolder;

        #endregion

        #region Private Variables

        private List<GameObject> _bulletBoxList;
        private TurretData _data;
        private float _directY;
        private float _directZ;
        private float _directX;

        #endregion

        #endregion

        private void Awake()
        {
            _bulletBoxList = new List<GameObject>();
            _data = GetTurretData();
        }


        private TurretData GetTurretData() => Resources.Load<CD_TurretData>("Data/CD_TurretData").Data;


        public void StartTake(GameObject target)
        {
            StartCoroutine(TakeAmmo(target));
        }

        public void StopTake()
        {
            StopAllCoroutines();
        }

        IEnumerator TakeAmmo(GameObject target)
        {
            WaitForSeconds waiter = new WaitForSeconds(0.3f);

          
            while (_bulletBoxList.Count <= _data.BulletBoxStackData.StackLimit)
            {
                var obj = BaseSignals.Instance.onGetAmmoInStack(target);
              
                if (obj == null)
                    yield break;
                yield return waiter;
                obj.transform.parent = stackHolder.transform;
                SetObjPosition(obj);
                _bulletBoxList.Add(obj);
                
              
            }
        }

        private void SetObjPosition(GameObject obj)
        {
            _directX = _bulletBoxList.Count % _data.BulletBoxStackData.LimitX * _data.BulletBoxStackData.OffsetX;
            _directY = _bulletBoxList.Count / (_data.BulletBoxStackData.LimitX * _data.BulletBoxStackData.LimitZ) *
                       _data.BulletBoxStackData.OffsetY;
            _directZ = _bulletBoxList.Count % (_data.BulletBoxStackData.LimitX * _data.BulletBoxStackData.LimitZ) /
                _data.BulletBoxStackData.LimitX * _data.BulletBoxStackData.OffsetZ;
            obj.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ),
                _data.BulletBoxStackData.AnimationDurition);
            obj.transform.DORotate(Vector3.zero, 0);
        }
    }
}