using System;
using UnityEngine;

namespace Game.flag.State
{
    public class NormalState : BaseState
    {
        private Animator animator;
        private CharacterController cc;
        public float speed = 8;
        private Transform cameraTrans;
        private float gravity = 29.7f;
        private float y = 0;
        
        public Func<bool> OnMouseLeftClick;
        public Func<bool> OnMouseRightClick;
        
        private float leftClickCoolDown=0.7f;
        private float lastLeftTrigger=0; 
        private float rightClickCoolDown=5f;
        private float lastRightTrigger=0;

        public NormalState(string stateName, SimpleHeroController simpleHeroController)
            : base(stateName,simpleHeroController)
        {
            OnStateUpdate += OnUpdate;
            OnStateExit += OnExit;

            cameraTrans = Camera.main.transform;
            animator = simpleHeroController.GetComponentInChildren<Animator>();
            cc = simpleHeroController.GetComponent<CharacterController>();
        }
        
        private void OnExit(Frame.FSM.State obj)
        {
            simpleHeroController.velocity = Vector3.zero;
            animator.SetFloat("Speed",0);
        }

        private void OnUpdate(Frame.FSM.State obj)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 velocityH = Vector3.zero;
            if (h != 0 || v != 0)
            {
                Vector3 dir = cameraTrans.forward * v + cameraTrans.right * h;
                dir.y = 0;
                dir.Normalize();
                simpleHeroController.transform.forward = dir;
                velocityH = dir * speed;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (cc.isGrounded)
                {
                    y = 15;
                }
            }
            else
            {
                if (cc.isGrounded)
                {
                    if (
                        y < -1)
                    {
                        y = -1;
                    }
                    else
                    {
                        y -= Time.deltaTime * gravity;
                    }
                }
                else
                {
                    y -= Time.deltaTime * gravity;
                }
            }

            simpleHeroController.velocity = velocityH + Vector3.up * y;
            animator.SetFloat("Speed", velocityH.magnitude);
            if (Input.GetMouseButtonDown(0))
            {
                if (OnMouseLeftClick != null && OnMouseLeftClick())
                {
                    Debug.Log("OnMouseLeftClick Intercept");
                }
                else
                {
                    if(Time.time-lastLeftTrigger>leftClickCoolDown)
                    {
                        lastLeftTrigger = Time.time;
                        simpleHeroController.isSkilling1 = true;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (OnMouseRightClick != null && OnMouseRightClick())
                {
                    Debug.Log("OnMouseRightClick Intercept");
                }
                else
                { 
                    if(Time.time-lastRightTrigger>rightClickCoolDown)
                    {
                        lastRightTrigger = Time.time;
                        simpleHeroController.isSkilling2 = true;
                    }
                }
            }
        }
    }
}