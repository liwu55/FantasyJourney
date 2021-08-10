using System.Collections;
using UnityEngine;

namespace Game.flag.State
{
    public class Skill1State:Frame.FSM.State
    {
        private SimpleHeroController simpleHeroController;
        private Animator animator;
        public Skill1State(string stateName, SimpleHeroController simpleHeroController) 
            : base(stateName)
        {
            this.simpleHeroController = simpleHeroController;
            animator = simpleHeroController.GetComponentInChildren<Animator>();
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