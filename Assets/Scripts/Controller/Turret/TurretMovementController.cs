using Enums;
using UnityEngine;
using Keys;
using Manager;
using UnityEngine.Serialization;

namespace Controller.Turret
{
    public class TurretMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretManager turretManager;

        #endregion

        #region Private Variables

        private float turretRotation;

        #endregion

        #endregion

        public void SetTurnValue(InputParams data)
        {
            if (turretManager.TurretType == TurretState.PlayerIn)
            {
                turretRotation = data.Values.x;
                SetAim();
            }
        }


        private void SetAim()
        {
            
            if (turretRotation >0.15f || turretRotation<-0.15f)
            {
                transform.rotation = Quaternion.Slerp(Quaternion.Euler(0,
                        transform.rotation.y,
                        0),
                    Quaternion.Euler(0,
                        Mathf.Clamp(turretRotation*35,
                            -35,
                            35),
                        0),
                    1f);

            }
        }
    }
}