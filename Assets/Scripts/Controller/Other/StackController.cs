using System;
using System.Collections.Generic;
using Commands;
using Data;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class StackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        [SerializeField] private Transform stackHolder;
        [SerializeField] private StackController stackController;

        #endregion

        #region Private Variables

        private StackData _data;
        private int _currentStackLevel;
        private AmmoStackCommand _ammoStackCommand;
        private MoneyStackCommand _moneyStackCommand;
        private StackType _stackType=StackType.Ammo;
        private List<GameObject> _stackList=new List<GameObject>();
        

        #endregion

        #endregion

        private void Start()
        {
            _stackList = new List<GameObject>();
            _moneyStackCommand = new MoneyStackCommand(ref stackController,ref _stackList,ref stackHolder,ref _data.MoneyStackData);
            _ammoStackCommand = new AmmoStackCommand(ref stackController,ref _stackList,ref stackHolder,ref _data.AmmoStackData);
        }

        public void SetStackData(StackData data)
        {
            _data =data;
        }     
        public void SetStackType(StackType stackType)
        {
            _stackType = stackType;
        }


        public void AddStack(GameObject obj)
        {
            if (obj == null) return;
            SetObjPosition(obj);
            _stackList.Add(obj);
            obj.transform.SetParent(stackHolder.transform);
        }

        private void SetObjPosition(GameObject obj)
        {
            switch (_stackType)
            {
                case StackType.Ammo:
                    _ammoStackCommand.Execute(obj);
                    break;
                case StackType.Money:
                    _moneyStackCommand.Execute(obj);
                    break;
            }
        }

    }
}