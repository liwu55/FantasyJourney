using System;
using UnityEngine;

namespace Game.flag.State
{
    public class BaseState : Frame.FSM.State
    {
        protected SimpleHeroController simpleHeroController;
        protected Animator animator;
        
        public BaseState(string stateName,SimpleHeroController simpleHeroController) : base(stateName)
        {
            this.simpleHeroController = simpleHeroController;
            animator = simpleHeroController.GetComponentInChildren<Animator>();
            OnStateEnter += PrintState;
        }

        private void PrintState(Frame.FSM.State obj)
        {
            Debug.Log("Enter "+obj.GetType().Name);
        }
    }
}