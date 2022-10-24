using System.Collections.Generic;
using Datas.ValueObject;
using DG.Tweening;
using Keys;
using Controller;
using Signals;
using UnityEngine;

namespace Commands.Turret
{
    public class TurretBulletBoxAddCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _bulletBoxList;
        private TurretData _data;
        private GameObject _stackHolder;
        private TurretManager _manager;

        #endregion

        #endregion

        public TurretBulletBoxAddCommand(ref List<GameObject> bulletBoxList, ref TurretData data,
            ref GameObject stackHolder, ref TurretManager manager)
        {
            _bulletBoxList = bulletBoxList;
            _data = data;
            _stackHolder = stackHolder;
            _manager = manager;
        }


        public void Execute(GameObject bulletBox)
        {
            bulletBox.transform.parent = _stackHolder.transform;
            _manager.SetObjPosition(bulletBox);
            _bulletBoxList.Add(bulletBox);
            BaseSignals.Instance.onHoldTurretData?.Invoke(_stackHolder.transform.parent.gameObject, new TurretStackParams
            {
                StackLimit = _data.BulletBoxStackData.StackLimit - _bulletBoxList.Count,
             
            });
        }
    }
}