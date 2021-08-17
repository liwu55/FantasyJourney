using System.Collections;
using UnityEngine;

namespace Game.flag.State
{
    public class Skill1State:BaseState
    {
        public Skill1State(string stateName, SimpleHeroController simpleHeroController) 
            : base(stateName,simpleHeroController)
        {
            OnStateEnter += OnEnter;
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            animator.SetBool("Skill1",true);
            simpleHeroController.StartCoroutine(ResetSkillState());
        }
        
        IEnumerator ResetSkillState()
        {
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("Skill1",false);
        }
    }
}