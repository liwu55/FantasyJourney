using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.flag.State
{
    public class BaseInteruptState : BaseState
    {
        public BaseInteruptState(string stateName,SimpleHeroController simpleHeroController) 
            : base(stateName,simpleHeroController)
        {
            OnStateUpdate += IsMove;
        }
        
        private void IsMove(Frame.FSM.State state)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            if( Mathf.Abs(h) < 0.01f && Mathf.Abs(v) < 0.01f)
            {
                return;
            }
            Debug.Log("Checking IsMove h="+h+" v="+v);
            IsMoveDo();
        }

        protected virtual void IsMoveDo()
        {
            
        }
    }
}