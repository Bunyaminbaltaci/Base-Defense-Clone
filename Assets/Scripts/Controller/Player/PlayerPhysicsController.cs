using System;
using DG.Tweening;
using Enums;
using Manager;
using Managers.Core;
using Signals;
using Unity.Mathematics;
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
            if (gameObject.layer == LayerMask.NameToLayer("Default"))
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
                playerManager.AddStack(other.gameObject);
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
                playerManager.StartCoroutine(playerManager.StartBulletBoxSend(other.gameObject));
            }

            if (other.CompareTag("BulletArea"))
            {
                playerManager.StartCoroutine(playerManager.TakeBulletBox());
            }

            if (other.CompareTag("Turret"))
            {
                var newparent = other.GetComponent<TurretManager>().PlayerHandle.transform;
                playerManager.transform.parent = newparent;
                playerManager.transform.DOLocalMove(new Vector3(0, playerManager.transform.localPosition.y, 0), .5f);
                playerManager.transform.DOLocalRotate(Vector3.zero, 0.5f) ;
                playerManager.ChangeMovement(PlayerMovementState.Turret);
                BaseSignals.Instance.onPlayerInTurret.Invoke(other.gameObject);
                CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Turret);
                CoreGameSignals.Instance.onSetCameraTarget?.Invoke(newparent.transform.parent);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("BaseLimit"))
            {
                ChangeLayer();
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
                BaseSignals.Instance.onPlayerOutTurret?.Invoke(other.gameObject);
                playerManager.transform.parent = playerManager.CurrentParent.transform;
                CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
                CoreGameSignals.Instance.onSetCameraTarget?.Invoke(playerManager.transform);
            }
        }
    }
}