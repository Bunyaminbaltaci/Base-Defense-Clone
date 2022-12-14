using System.Collections;
using System.Threading.Tasks;
using Abstract;
using Enums.Npc;
using Controller;
using UnityEngine;
using UnityEngine.AI;
using Enums.Npc;

namespace States.MinerStates
{
    public class DigState : IStateMachine
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private MinerManager _manager;

        #endregion

        #endregion

        public DigState(MinerManager manager  )
        {
            _manager = manager;
        }

        public void EnterState()
        {
            DigDiamond();
            _manager.SetTriggerAnim(MinerAnimType.Dig);
         
        }

        public void UpdateState()
        {
        }

        public void OnTriggerEnterState(Collider other)
        {
        }

        public void OnTriggerExitState(Collider other)
        {
            
        }

        private  void DigDiamond()
        {
            _manager.StartCoroutine(_manager.DigDiamond());
        }

        public void SwitchState()
        {
          
        }
    }
}