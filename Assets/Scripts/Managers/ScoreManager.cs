using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private int _money=500;
        private int _diamond;

        #endregion

        #endregion


        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            ScoreSignals.Instance.onBuyArea += OnBuyArea;
            ScoreSignals.Instance.onAddDiamond += OnAddDiamond;
            ScoreSignals.Instance.onAddMoney += OnAddMoney;
            ScoreSignals.Instance.onMoneyDown += OnMoneyDown;
            ScoreSignals.Instance.onDiamondDown += OnDiamondDown;

            SaveSignals.Instance.onGetScoreData += OnGetScoreData;
        }

        private void UnSubscribeEvent()
        {
            ScoreSignals.Instance.onBuyArea -= OnBuyArea;
            ScoreSignals.Instance.onAddDiamond -= OnAddDiamond;
            ScoreSignals.Instance.onAddMoney -= OnAddMoney;
            ScoreSignals.Instance.onMoneyDown -= OnMoneyDown;
            ScoreSignals.Instance.onDiamondDown -= OnDiamondDown;

            SaveSignals.Instance.onGetScoreData -= OnGetScoreData;
        }


        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion
        
        
        private void OnBuyArea()
        {
            if (_money <= 0) return;
            _money--;
            IdleGameSignals.Instance.onAreaCostDown?.Invoke();
            SetMoneyText();
        }


        private void OnAddMoney(int value)
        {
            _money += value;
            SetMoneyText();
        }

        private void OnMoneyDown(int value)
        {
            _money -= value;
            SetMoneyText();
        }

        private void OnAddDiamond(int value)
        {
            _diamond += value;
            SetDiamondText();
        }

        private void OnDiamondDown(int value)
        {
            _diamond -= value;
            SetDiamondText();
        }


        private void SetMoneyText()
        {
            UISignals.Instance.onSetMoneyText?.Invoke(_money);
        }

        private void SetDiamondText()
        {
            UISignals.Instance.onSetDiamondText?.Invoke(_diamond);
        }

        private ScoreDataParams OnGetScoreData()
        {
            return new ScoreDataParams
            {
                Money = _money,
                Diamond = _diamond
            };
        }
    }
}