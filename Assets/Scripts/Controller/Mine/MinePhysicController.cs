using System;
using UnityEngine;

namespace Managers.Mine
{
    public class MinePhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private MineManager mineManager;

        #endregion

        #region Private Variables
        

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
             mineManager.StartCollectDiamond(other.gameObject);
            }
        }
    }
}