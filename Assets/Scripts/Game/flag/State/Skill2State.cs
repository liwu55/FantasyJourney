﻿using System.Collections;
using UnityEngine;

namespace Game.flag.State
{
    public class Skill2State : BaseState
    {

        public Skill2State(string stateName, SimpleHeroController simpleHeroController)
            : base(stateName,simpleHeroController)
        {
            OnStateEnter += OnEnter;
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            animator.SetBool("Skill2",true);
            simpleHeroController.StartCoroutine(ResetSkillState());
        }

        IEnumerator ResetSkillState()
        {
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("Skill2",false);
        }
    }
}