using System.Collections.Generic;
using Controllers;
using Datas.ValueObject;
using DG.Tweening;
using UnityEngine;

namespace Commands
{
    public class MoneyStackCommand
    {
        #region Self Variables

        #region Private Variables

       
        private List<GameObject> _stack;
      
        private StackData _data;
        private float _directY;
        private float _directX;
        private float _directZ;


        #endregion

        #endregion

        public MoneyStackCommand(ref List<GameObject> stack,ref StackData data)
        {
            _stack = stack;
            _data = data;
        }
        
       
        public void Execute(GameObject obj)
        {
            _directY = _stack.Count % _data.LimitY * _data.OffsetY;
            _directX = _stack.Count / (_data.LimitY * _data.LimitZ) * _data.OffsetX;
            _directZ = -(_stack.Count % (_data.LimitY * _data.LimitZ) / _data.LimitY * _data.OffsetZ);
          
            
            
            
            
            
            obj.transform.DOLocalRotate(Vector3.zero, _data.AnimationDurition);
            obj.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ), 0.5f);
           
        }
    }
}