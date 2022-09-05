using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
     {
    //     #region Self Variables
    //
    //     #region Serialized Variables
    //
    //     [SerializeField] private PlayerManager playerManager;
    //
    //     #endregion
    //
    //     #region Private Variables
    //
    //     private int _timer;
    //
    //     #endregion
    //
    //     #endregion
    //
    //     private void OnTriggerEnter(Collider other)
    //     {
    //     
    //     }
    //
    //     private void OnTriggerStay(Collider other)
    //     {
    //         if (other.CompareTag("BuildArea"))
    //         {
    //             if (_timer >= 20)
    //             {
    //            
    //                 playerManager.DownCost();
    //                 _timer = _timer * 70 / 100;
    //             }
    //             else
    //             {
    //                 _timer++;
    //             }
    //         }
    //     }
    //
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