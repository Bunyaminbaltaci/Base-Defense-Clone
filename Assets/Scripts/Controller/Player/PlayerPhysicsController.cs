using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money")) playerManager.AddStack(other.gameObject);


            if (other.CompareTag("BuildArea"))
            {
                BaseSignals.Instance.onCheckArea?.Invoke(other.transform.parent.gameObject);
            }

        
         
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Ammo"))
            {
                if (_timer >= 20)
                {
                    playerManager.AddStack(CoreGameSignals.Instance.onGetammo());
                    _timer = _timer * 70 / 100;
                }
                else
                {
                    _timer++;
                }
            }

            if (other.CompareTag("BuildArea"))
            {
                if (_timer >= 20)
                {
                  ScoreSignals.Instance.onBuyArea?.Invoke();
                }
                else
                {
                    _timer++;
                }
            }
        }

        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private int _timer;

        #endregion

        #endregion

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