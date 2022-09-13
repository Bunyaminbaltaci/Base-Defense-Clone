using System.Collections.Generic;
using Data;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class PlayerStackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> StackList;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject stackHolder;

        #endregion

        #region Private Variables

        private PlayerStackData _data;
        private int _currentStackLevel;
        private float _directY;

        #endregion

        #endregion

        public void SetStackData(PlayerStackData data)
        {
            _data = data;
        }


        public void AddStack(GameObject obj)
        {
            if (obj == null) return;
            StackList.Add(obj);
            obj.transform.SetParent(stackHolder.transform);
            SetObjPosition(obj);
        }

        private void SetObjPosition(GameObject obj)
        {
            obj.transform.DOLocalRotate(Vector3.zero, _data.AnimationDurition);
            obj.transform.DOLocalMove(new Vector3(0, _directY, -(_currentStackLevel * _data.StackoffsetZ)),
                _data.AnimationDurition);
            _directY = StackList.Count % _data.StackLimit * _data.StackoffsetY;
            _currentStackLevel = StackList.Count / _data.StackLimit;
        }

    }
}