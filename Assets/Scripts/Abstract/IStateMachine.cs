using UnityEngine;

namespace Abstract
{
    public interface IStateMachine
    {
        void EnterState();
        void UpdateState();
        void OnCollisionDetectionState(Collider other);
        void SwitchState();

    }
}