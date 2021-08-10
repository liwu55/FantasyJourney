using System.Collections;
using UnityEngine;

namespace Game.flag.State
{
    public class Skill2State : Frame.FSM.State
    {
        private SimpleHeroController simpleHeroController;
        private Animator animator;
        private Transform transform;
        private float rushSpeed = 10;
        private float rotateSpeed = 180;

        public Skill2State(string stateName, SimpleHeroController simpleHeroController)
            : base(stateName)
        {
            this.simpleHeroController = simpleHeroController;
            transform = simpleHeroController.transform;
            animator = simpleHeroController.GetComponentInChildren<Animator>();
            OnStateEnter += OnEnter;
            OnStateUpdate += OnUpdate;
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

        private void OnUpdate(Frame.FSM.State obj)
        {
            simpleHeroController.velocity = transform.forward * rushSpeed;
            float h = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * h * rotateSpeed * Time.deltaTime);
        }
    }
}