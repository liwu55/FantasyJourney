using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.flag.State
{
    public class BaseInteruptState : BaseState
    {
        private float enterTime;
        private float leastStayTime = 0.1f;
        public BaseInteruptState(string stateName,SimpleHeroController simpleHeroController) 
            : base(stateName,simpleHeroController)
        {
            OnStateEnter += OnEnter;
            OnStateUpdate += IsMove;
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            enterTime = Time.time;
        }

        private void IsMove(Frame.FSM.State state)
        {
            float stayTime = Time.time - enterTime;
            //先呆着吧
            if (stayTime < leastStayTime)
            {
                return;
            }
            //float h = Input.GetAxisRaw("Horizontal");
            //float v = Input.GetAxisRaw("Vertical");
            bool pressDir = Input.GetKey(KeyCode.A) 
                            || Input.GetKey(KeyCode.D) 
                            || Input.GetKey(KeyCode.W) 
                            || Input.GetKey(KeyCode.S);
            if (pressDir)
            {
                IsMoveDo();
            }
            /*if( Mathf.Abs(h) < 0.01f && Mathf.Abs(v) < 0.01f)
            {
                return;
            }*/
            //Debug.Log("Checking IsMove h="+h+" v="+v);
            
        }

        protected virtual void IsMoveDo()
        {
            
        }
    }
}