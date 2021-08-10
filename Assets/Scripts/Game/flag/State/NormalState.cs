using UnityEngine;

namespace Game.flag.State
{
    public class NormalState : Frame.FSM.State
    {
        private Animator animator;
        private CharacterController cc;
        public float speed = 6;
        private Transform cameraTrans;
        private float gravity = 29.7f;
        private float y = 0;
        private SimpleHeroController simpleHeroController;

        public NormalState(string stateName, SimpleHeroController simpleHeroController)
            : base(stateName)
        {
            this.simpleHeroController = simpleHeroController;
            OnStateUpdate += OnUpdate;
            OnStateExit += OnExit;
            OnStateEnter += OnEnter;

            cameraTrans = Camera.main.transform;
            animator = simpleHeroController.GetComponentInChildren<Animator>();
            cc = simpleHeroController.GetComponent<CharacterController>();
        }

        private void OnEnter(Frame.FSM.State obj)
        {
            Debug.Log("Enter NormalState");
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
                if (CheckIfClickPoint(out simpleHeroController.occupingPoint))
                {
                    //转向该点
                    simpleHeroController.transform.forward =
                        simpleHeroController.occupingPoint.transform.position - 
                        simpleHeroController.transform.position;
                    simpleHeroController.occuping = true;
                }
                else
                {
                    simpleHeroController.isSkilling1 = true;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                simpleHeroController.isSkilling2 = true;
            }
        }

        private bool CheckIfClickPoint(out PointBase occupingPoint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int flayPointLayer = LayerMask.NameToLayer("flagPoint");
            bool hitFlag = Physics.Raycast(ray, out hit, 1000, 1 << flayPointLayer);
            if (hitFlag)
            {
                float distance = Vector3.Distance(simpleHeroController.transform.position, hit.transform.position);
                Debug.Log("distance="+distance);
                if (distance > simpleHeroController.operationDistance)
                {
                    Debug.Log("距离太远，不能操作");
                }
                else
                {
                    Debug.Log("距离在范围内，可以操作");
                    PointBase point = hit.collider.GetComponent<PointBase>();
                    if (!point.IsOccupied()||point.GetOccupiedSign()!=simpleHeroController.pointSign)
                    {
                        Debug.Log("占领该据点");
                        occupingPoint = point;
                        return true;
                        //point.ChangeOccupiedSign(simpleHeroController.pointSign);
                    }
                    else
                    {
                        Debug.Log("该据点已占领");
                    }
                }
            }

            occupingPoint = null;
            return false;
        }
    }
}