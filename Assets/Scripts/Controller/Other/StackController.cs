using System;
using System.Collections;
using System.Collections.Generic;
using Commands;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Controller
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

        public void SetStackType(LayerType side)
        {
            StartCollect();
            switch (side)
            {
                case LayerType.Default:
                    this.stackType = StackType.Ammo;
                    break;
                case LayerType.BattleArea:
                    this.stackType = StackType.Money;
                    break;
                    
                
            }
        }

        public void DropMoney()
        {
            for (int i = StackList.Count-1; i >=0; i--)
            {
                StackList[i].transform.parent = BaseSignals.Instance.onGetBase?.Invoke().transform;
                StackList[i].GetComponent<MoneyController>().OnDrop();
                StackList.RemoveAt(i);
                
            }
        }
        public IEnumerator StartBulletBoxSend(GameObject target)
        {
            var waiter = new WaitForSeconds(0.2f);
            while (StackList.Count > 0)
            {
                if (BaseSignals.Instance.onGetTurretLimit(target) > 0)
                    BaseSignals.Instance.onSendAmmoInStack?.Invoke(target, SendBulletBox());

                yield return waiter;
            }
        }
        public IEnumerator TakeBulletBox()
        {
            var waiter = new WaitForSeconds(0.2f);
            while (StackList.Count < _bulletBoxStackData.StackLimit)
            {
                var obj = BaseSignals.Instance.onGetBulletBox?.Invoke();
                if (obj == null)
                    break;
                AddStack(obj);
                yield return waiter;
            }
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