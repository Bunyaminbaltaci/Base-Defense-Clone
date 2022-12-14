using Data;
using UnityEngine;

namespace Commands
{
    public class PoolGenerateCommand
    { 
        #region Self Variables
     
             #region Private Variables
     
             private readonly CD_PoolGenerator _cdPoolGenerator;
             private GameObject _emptyGameObject;
             private readonly Transform _managerTranform;
     
             #endregion
     
             #endregion
        public PoolGenerateCommand(ref CD_PoolGenerator cdPoolGenerator, ref Transform managertransform,
            ref GameObject emptyGameObject)
        {
            _cdPoolGenerator = cdPoolGenerator;
            _emptyGameObject = emptyGameObject;
            _managerTranform = managertransform;
        }

        public void Execute()
        {
            var pooldata = _cdPoolGenerator.PoolDataList;
            for (var i = 0; i < pooldata.Count; i++)
            {
                _emptyGameObject = new GameObject();
                _emptyGameObject.transform.parent = _managerTranform;
                _emptyGameObject.name = pooldata[i].Type.ToString();

                for (var j = 0; j < pooldata[i].ObjectCount; j++)
                {
                    var obj = Object.Instantiate(pooldata[i].Pref, _managerTranform.GetChild(i));
                    obj.SetActive(false);
                }
            }
        }

       
    }
}