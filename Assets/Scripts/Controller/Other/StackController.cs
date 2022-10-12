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
        public List<GameObject> StackList = new List<GameObject>();

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

        #endregion

        #endregion

        private void Start()
        {
            StackList = new List<GameObject>();
            _moneyStackCommand = new MoneyStackCommand( ref StackList, ref _moneyStackData);
            _bulletBoxStackCommand = new BulletBoxStackCommand( ref StackList, ref _bulletBoxStackData);
            _moneyDepositCommand = new MoneyDepositCommand(ref StackList);
            _bulletBoxDepositCommand = new BulletBoxDepositCommand(ref StackList);
        }

        public void SetStackData(StackData ammoStackData, StackData moneyStackData)
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
            StackList.Add(obj);
        }

        public GameObject SendBulletBox()
        {
            if (StackList.Count > 0)
            {
                var obj = StackList[StackList.Count - 1];
                StackList.Remove(obj);
                StackList.TrimExcess();
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