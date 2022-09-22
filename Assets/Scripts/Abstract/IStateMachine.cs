using UnityEngine;

namespace Abstract
{
    public interface IStateMachine
    {
        void EnterState();
        void UpdateState();
        void OnTriggerEnterState(Collider other);
        void OnTriggerExitState(Collider other);
      

    }

  
}