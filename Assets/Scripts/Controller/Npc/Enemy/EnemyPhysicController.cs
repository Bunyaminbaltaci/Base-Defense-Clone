using System;
using Controller;
using UnityEngine;

namespace Controller.Npc.Enemy
{
    public class EnemyPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private EnemyManager enemyManager;

        #endregion

        #region Private Variables


        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            enemyManager.CurrentState.OnTriggerEnterState(other);
        }

        private void OnTriggerExit(Collider other)
        {
            enemyManager.CurrentState.OnTriggerExitState(other);
        }
    }
}