using System;
using Enums;
using Manager;
using Managers.Core;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private int _timer;

        #endregion

        #endregion

        private void ChangeLayer()
        {
            playerManager.StartCollectStack();
            if (  gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                gameObject.layer = LayerMask.NameToLayer("BattleArea");
                playerManager.ChageStackState(StackType.Money);
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
                playerManager.ChageStackState(StackType.Ammo);

            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                other.GetComponent<Collider>().enabled = false;
                playerManager.AddStack(other.gameObject);
            }


            if (other.CompareTag("BuildArea"))
            {
                IdleSignals.Instance.onCheckArea?.Invoke(other.transform.parent.gameObject);
            }

            if (other.CompareTag("MineWareHouse"))
            {
                BaseSignals.Instance.OnStartCollectDiamond?.Invoke(gameObject);
            }

            if (other.CompareTag("MineDoor"))
            {
                playerManager.HostageAddMine();
            }
            
            
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("BaseLimit"))
            {
                ChangeLayer();
            }
        }

        
    }
}