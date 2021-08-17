using UnityEngine;

namespace Game.flag.State
{
    /// <summary>
    /// 晕眩
    /// </summary>
    public class DizzyState:BaseState
    {
        public bool isDead = false;
        public float dizzyTime = 5;
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
            simpleHeroController.ResetState(isDead);
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