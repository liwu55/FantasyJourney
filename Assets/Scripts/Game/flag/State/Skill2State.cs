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
            Debug.Log("OnEnter Skill2");
            animator.SetTrigger("skill2");
        }

        private void OnUpdate(Frame.FSM.State obj)
        {
            simpleHeroController.velocity = transform.forward * rushSpeed;
            float h = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * h * rotateSpeed * Time.deltaTime);
        }
    }
}