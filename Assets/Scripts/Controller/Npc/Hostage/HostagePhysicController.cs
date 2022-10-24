using UnityEngine;

namespace Controller.Npc.Hostage
{
    public class HostagePhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private HostageManager hostageManager;

        #endregion

        #region Private Variables


        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            hostageManager.CurrentInpcState.OnTriggerEnterState(other);
        }
    }
}