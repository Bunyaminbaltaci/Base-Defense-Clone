using System;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class DoorController : MonoBehaviour
    {

        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject door;

        #endregion

        #endregion

        private void OpenDoor()
        {
            door.transform.DOLocalRotate(new Vector3(0,0,90f),1f);
        }    
        private void CloseDoor()
        {
            door.transform.DOLocalRotate(Vector3.zero, 1f);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") )
            {
               OpenDoor();
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") )
            {
                CloseDoor();
            }
        }
    }
}