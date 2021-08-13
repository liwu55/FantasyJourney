using UnityEngine;

namespace Game.flag.State
{
    /// <summary>
    /// 晕眩
    /// </summary>
    public class DizzyState:BaseState
    {
        private const float dizzyTime = 3;
        private float dizzyStartTime;
        public DizzyState(string stateName,SimpleHeroController simpleHeroController) 
            : base(stateName,simpleHeroController)
        {
            OnStateEnter += OnEnter;
            OnStateExit += OnExit;
        }

        private void OnExit(Frame.FSM.State obj)
        {
            animator.SetBool("Dizzy",false);
            simpleHeroController.ResetBlood();
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            animator.SetBool("Dizzy",true);
            dizzyStartTime = Time.time;
        }

        public bool IsDizzyTimeEnough()
        {
            return Time.time - dizzyStartTime > dizzyTime;
        }
    }
}