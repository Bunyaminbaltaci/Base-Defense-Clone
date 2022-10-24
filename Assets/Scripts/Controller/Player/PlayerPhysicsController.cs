using System;
using DG.Tweening;
using Enums;
using Controller;
using Managers.Core;
using Signals;
using Unity.Mathematics;
using UnityEngine;

namespace Controller
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

        public void ChangeLayer(LayerType type)
        {
            gameObject.layer = LayerMask.NameToLayer(type.ToString());
        }
     

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                playerManager.AddMoneyOnStack(other.gameObject);
                BaseSignals.Instance.onRemoveHaversterTargetList?.Invoke(other.gameObject);
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

            if (other.CompareTag("TurretStack"))
            {
                playerManager.StartBulletBoxSend(other.gameObject);
            }

            if (other.CompareTag("BulletArea"))
            {
                playerManager.TakeBulletBox();
            }

            if (other.CompareTag("Turret"))
            {
                if (other.GetComponent<TurretManager>().TurretType == TurretState.None)
                {
                    playerManager.InTurret(other.gameObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("BaseLimit"))
            {
                playerManager.ChangeLayer();
            }

            if (other.CompareTag("TurretStack"))
            {
                playerManager.StopAllCoroutines();
            }
            if (other.CompareTag("BulletArea"))
            {
                playerManager.StopAllCoroutines();
            }

            if (other.CompareTag("Turret"))
            {
                if (other.GetComponent<TurretManager>().TurretType == TurretState.PlayerIn)
                {
                    playerManager.OutTurret(other.gameObject);
                }
            }
        }
    }
}