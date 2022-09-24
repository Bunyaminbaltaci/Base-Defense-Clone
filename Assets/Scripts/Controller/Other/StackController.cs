using System;
using System.Collections.Generic;
using Commands;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


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
        [SerializeField] private StackType stackType = StackType.Money;

        #endregion

        #region Private Variables

        private StackData _bulletBoxStackData;
        private StackData _moneyStackData;
        private int _currentStackLevel;
        private BulletBoxStackCommand _bulletBoxStackCommand;
        private MoneyStackCommand _moneyStackCommand;
        private MoneyDepositCommand _moneyDepositCommand;
        private BulletBoxDepositCommand _bulletBoxDepositCommand;
        private List<GameObject> _stackList = new List<GameObject>();

        #endregion

        #endregion

        private void Start()
        {
            _stackList = new List<GameObject>();
            _moneyStackCommand = new MoneyStackCommand(ref stackController, ref _stackList, ref stackHolder,
                ref _moneyStackData);
            _bulletBoxStackCommand = new BulletBoxStackCommand(ref stackController, ref _stackList, ref stackHolder,
                ref _bulletBoxStackData);
            _moneyDepositCommand = new MoneyDepositCommand(ref _stackList);
            _bulletBoxDepositCommand = new BulletBoxDepositCommand(ref _stackList);
        }

        public void SetStackData(StackData ammoStackData,StackData moneyStackData)
        {
            _bulletBoxStackData = ammoStackData;
            _moneyStackData = moneyStackData;
        }

        public void SetStackType(StackType stackType)
        {
            this.stackType = stackType;
        }


        public void AddStack(GameObject obj)
        {
            if (obj == null) return;
            obj.transform.SetParent(stackHolder.transform);
            SetObjPosition(obj);
            _stackList.Add(obj);
        }

        public GameObject SendBulletBox()
        {
            if (_stackList.Count>0)
            {
                var obj = _stackList[_stackList.Count-1];
                _stackList.Remove(obj);
                _stackList.TrimExcess();
                return obj;
            }

            return null;
        }

   
        private void SetObjPosition(GameObject obj)
        {
            switch (stackType)
            {
                case StackType.Ammo:
                    _bulletBoxStackCommand.Execute(obj);
                    break;
                case StackType.Money:
                    _moneyStackCommand.Execute(obj);
                    break;
            }
        }

        public void StartCollect()
        {
            switch (stackType)
            {
                case StackType.Ammo:
                    _bulletBoxDepositCommand.Execute();
                    break;
                case StackType.Money:
                    _moneyDepositCommand.Execute();
                    break;
            }
        }
    }
}