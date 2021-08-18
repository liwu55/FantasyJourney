using System.Collections;
using UnityEngine;

namespace Game.flag.State
{
    public class Skill1State:BaseInteruptState
    {
        public Skill1State(string stateName, SimpleHeroController simpleHeroController) 
            : base(stateName,simpleHeroController)
        {
            OnStateEnter += OnEnter;
            OnStateExit += OnExit;
        }

        private void OnExit(Frame.FSM.State obj)
        {
            Debug.Log("Skill1State Exit");
            animator.SetBool("Skill1",false);
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            animator.SetBool("Skill1",true);
            //simpleHeroController.StartCoroutine(ResetSkillState());
        }
        
        IEnumerator ResetSkillState()
        {
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("Skill1",false);
        }

        protected override void IsMoveDo()
        {
            Debug.Log("Call IsMoveDo");
            simpleHeroController.isSkilling1 = false;
        }
    }
}