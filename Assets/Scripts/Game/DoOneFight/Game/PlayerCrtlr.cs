using Frame.FSM;
using Frame.Utility;
using Game.DoOneFight.HealthPoint;
using Game.DoOneFight.Interface;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using EventType = UnityEngine.EventType;

namespace Game.DoOneFight.State
{
    public class PlayerCrtlr : HealthSystem, IHurtable
    {
        [HideInInspector] public CharacterController cc;
        [HideInInspector] public CharacterAniCtrler _aniCtrler;
        private float speed = 4f;
        public Vector3 dir;
        private Vector3 attackpoint;
        private float smooth = 2f;
        private float gravity = 10f;
        private float normalDamage = 5f;
        public bool isNrmAtk;
        public bool isOnSkill_01;
        public bool isOnSkill_02;
        public bool isHurt;

        private void Awake()
        {
            /*_animator = GetComponent<Animator>();
            _aniCtrler = gameObject.GetComponent<CharacterAniCtrler>();
            _aniCtrler.Init(_animator);*/
            //非战斗总状态机
            attackpoint = transform.Find("AttackPoint").position;
            _aniCtrler = gameObject.GetComponent<CharacterAniCtrler>();
            cc = transform.GetComponent<CharacterController>();
            StateMachine playerStateMachine = new StateMachine("playerStateMachine");
            StateMachine attackStateMachine = new StateMachine("attackStateMachine");
            NormalState normalState = new NormalState("Normal", this);
            AttackState attackState = new AttackState("AttackState", this);
            Skill_01_AttackState skill01AttackState = new Skill_01_AttackState("Skill_01_AttackState", this);
            Skill_02_AttackState skill02AttackState = new Skill_02_AttackState("Skill_02_AttackState", this);
            HurtState hurtState = new HurtState("HurtState", this);


            //状态
            playerStateMachine.AddState(normalState);
            playerStateMachine.AddState(attackStateMachine);
            playerStateMachine.AddState(skill01AttackState);
            playerStateMachine.AddState(skill02AttackState);
            playerStateMachine.AddState(hurtState);
            attackStateMachine.AddState(attackState);


            //正常 -> 攻击状态
            normalState.AddTransition("attackStateMachine", () => isNrmAtk);
            attackStateMachine.AddTransition("Normal", () => !isNrmAtk);
            normalState.AddTransition("Skill_01_AttackState", () => isOnSkill_01);
            skill01AttackState.AddTransition("Normal", () => !isOnSkill_01);
            normalState.AddTransition("Skill_02_AttackState", () => isOnSkill_02);
            skill02AttackState.AddTransition("Normal", () => !isOnSkill_02);
            normalState.AddTransition("HurtState", () => isHurt);
            hurtState.AddTransition("Normal", () => !isHurt);
            attackStateMachine.AddTransition("HurtState", () => isHurt);


            playerStateMachine.EnterState();

            //添加受伤事件
            EventCenter.Instance.AddListener<float>(Frame.Utility.EventType.GetHurt, MinusHp);
        }

        void Start()
        {
            InputMgr.Instance.InitInputDic();
        }

        bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, 10f);
        }

        //计算夹角的角度 0~360
        /*private float GetAngle(Vector3 from_, Vector3 to_)
        {
            Vector3 v3 = Vector3.Cross(from_, to_);
            if (v3.z >= 0)
                return Vector3.Angle(from_, to_);
            else
                return 360 - Vector3.Angle(from_, to_);
        }*/

        void FixedUpdate()
        {
            if (photonView.IsMine)
            {
                if (cc.enabled)
                {
                    cc.SimpleMove(dir * speed);

                    if (!IsGrounded())
                    {
                        dir.y -= gravity * Time.deltaTime;
                        isOnSkill_01 = false;
                    }
                }

                //Move();
                //Action();
            }
        }

        /*void Move()
        {
            if (Input.GetKeyDown(InputMgr.Instance.inputDic[EKeyName.jump]))
            {
                _aniCtrler.PlayAnimation((int)CharacterAniId.Jump);
            }
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(h, 0, v);
            if (dir.magnitude > 0)
            {
                _aniCtrler.PlayRun();
            }
            else
            {
                _aniCtrler.PlayIdle();
            }
            Vector3 dirWorld = Camera.main.transform.TransformDirection(dir);
            dirWorld.y = 0;
            if (GetAngle(transform.forward, dirWorld) > 135f)
            {
                transform.forward = dirWorld;
            }
            else
            {
                transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 10f);
            }
            cc.SimpleMove(speed * dir);
        }
        */


        void Action()
        {
            if (Input.GetKeyDown(InputMgr.Instance.inputDic[EKeyName.normalAttack]))
            {
                //播放动画
                //_aniCtrler.PlayAnimation((int)CharacterAniId.NormalAttack);
            }

            //跳跃
        }


        #region 动画事件 取反bool值

        private void NormalAtkEnd()
        {
            if (!cc.enabled)
            {
                cc.enabled = true;
            }

            isNrmAtk = false;
        }

        private void NormalAtkStart()
        {
            isNrmAtk = true;
        }

        private void HeavyAttackEnd()
        {
            cc.enabled = true;
            isOnSkill_01 = false;
        }

        private void Skill02End()
        {
            Debug.Log("Skill02End");
            isOnSkill_02 = false;
        }

        private void HurtEnd()
        {
            isHurt = false;
        }

        #endregion

        private void AttackCheck()
        {
            Collider[] colliders = Physics.OverlapCapsule(transform.position, attackpoint, 5f);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    IHurtable target = colliders[i]?.GetComponent<IHurtable>();
                    if (target != null)
                    {
                        target.TakeDamage(normalDamage);
                    }
                }
            }
        }

        public bool IsHurt()
        {
            return isHurt;
        }

        public void TakeDamage(float damage)
        {
            print(name+"受到了"+damage+"伤害 ，还剩 "+ currentHp+" 生命值");
            isHurt = true;
            MinusHp(damage);
        }
    }
}