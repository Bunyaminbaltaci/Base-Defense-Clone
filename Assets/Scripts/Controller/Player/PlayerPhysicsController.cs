using Enums;
using Managers;
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

     
    
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Ammo"))
            {
                if (_timer >= 20)
                {
               
                    playerManager.AddStack(CoreGameSignals.Instance.onGetammo());
                    _timer = _timer * 50 / 100;
                }
                else
                {
                    _timer++;
                }
            }
        }
        
    //     private void OnTriggerExit(Collider other)
    //     {
    //         if (other.CompareTag("BuildArea"))
    //         {
    //             _timer = 0;
    //             playerManager.OnStageChanged();
    //             
    //         }
    //
    //         if (other.CompareTag("Finish"))
    //         {
    //             other.GetComponent<Collider>().isTrigger = false;
    //         }
    //     }
    }
}