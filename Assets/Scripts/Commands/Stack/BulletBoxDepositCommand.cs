using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Commands
{
    public class BulletBoxDepositCommand
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
        public BulletBoxDepositCommand(ref List<GameObject> stackList)
        {
            _stackList = stackList;
        }
        
        public void Execute(){            
                    
            var limit = _stackList.Count;
            for (var i = 0; i < limit; i++)
            {
                var obj = _stackList[0];
                _stackList.RemoveAt(0);
                _stackList.TrimExcess();
                obj.transform.DOLocalMove(
                    new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 0.5f), Random.Range(-0.5f, 0.5f)), 1f);
                obj.transform.parent = CoreGameSignals.Instance.onGetAmmoArea?.Invoke().transform;
                obj.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 1f).SetDelay(1f).OnComplete(() =>
                {
                    PoolSignals.Instance.onSendPool?.Invoke(obj, PoolType.BulletBox);
                });
           
            }
        }
    }
}