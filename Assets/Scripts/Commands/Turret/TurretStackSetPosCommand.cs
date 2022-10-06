using System.Collections.Generic;
using Datas.ValueObject;
using DG.Tweening;
using UnityEngine;

namespace Commands.Turret
{
   
    public class TurretStackSetPosCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _bulletBoxList;
        private TurretData _data;
        private float _directY;
        private float _directZ;
        private float _directX;

        #endregion

        #endregion

        public TurretStackSetPosCommand(ref List<GameObject> bulletBoxList,ref TurretData data)
        {
            _bulletBoxList = bulletBoxList;
            _data = data;
        }
        
       





        public void Execute(GameObject obj)
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