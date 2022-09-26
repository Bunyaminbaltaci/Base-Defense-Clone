using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Commands
{
    public class MoneyDepositCommand
    {

        #region Self Variables

        #region Private Variables

        private List<GameObject> _stackList;

        #endregion

        #region Serialized Variables

        

        #endregion
        #region Public Variables

        

        #endregion

        #endregion
        public MoneyDepositCommand(ref List<GameObject> stackList)
        {
            _stackList = stackList;
        }
        
        public void Execute(){            
                    
            var limit = _stackList.Count;
            for (var i = 0; i < limit; i++)
            {
                var obj = _stackList[0];
              
                obj.transform.DOLocalMove(
                    new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 0.5f), Random.Range(-0.5f, 0.5f)), 0.5f);
                obj.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 0.5f).SetDelay(0.5f).OnComplete(() =>
                {
                    PoolSignals.Instance.onSendPool?.Invoke(obj, PoolType.Money);
                    
                    obj.GetComponent<Collider>().enabled = true;
                });
                _stackList.RemoveAt(0); 
                _stackList.TrimExcess();
                ScoreSignals.Instance.onAddMoney?.Invoke(1);
            }

            SaveSignals.Instance.onSaveScoreData?.Invoke();
        }
    }
}