using Frame.FSM;
using Frame.Utility;
using Game.DoOneFight.HealthPoint;
using Game.DoOneFight.Interface;
using Game.flag;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using EventType = UnityEngine.EventType;

namespace Game.DoOneFight.State
{
    public class PlayerCrtlr : HealthSystem, IHurtable ,IHeroController
    {
        [HideInInspector] public CharacterController cc;
        [HideInInspector] public CharacterAniCtrler _aniCtrler;
        private Camera _atkCamera;
        private PlayerCanvas _playerCanvas;
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
        public bool isDead;

        private void Awake()
        {
            /*_animator = GetComponent<Animator>();
            _aniCtrler = gameObject.GetComponent<CharacterAniCtrler>();
            _aniCtrler.Init(_animator);*/
            //非战斗总状态机
            SceneHeroes.Instance.Add(this);
            _atkCamera = transform.Find("atkCamera").GetComponent<Camera>();
            _playerCanvas = transform.Find("HeroUI").GetComponent<PlayerCanvas>();
            TransParentControl.Instance.Init(transform);
            if (!photonView.IsMine)
                return;
            attackpoint = transform.Find("AttackPoint").position;
            _aniCtrler = gameObject.GetComponent<CharacterAniCtrler>();
            cc = transform.GetComponent<CharacterController>();
            BindCamera();
            StateMachine playerStateMachine = new StateMachine("playerStateMachine");
            StateMachine attackStateMachine = new StateMachine("attackStateMachine");
            NormalState normalState = new NormalState("Normal", this);
            AttackState attackState = new AttackState("AttackState", this);
            Skill_01_AttackState skill01AttackState = new Skill_01_AttackState("Skill_01_AttackState", this);
            Skill_02_AttackState skill02AttackState = new Skill_02_AttackState("Skill_02_AttackState", this);
            HurtState hurtState = new HurtState("HurtState", this);
            DeadState deadState = new DeadState("DeadState",this);
            //状态
            playerStateMachine.AddState(normalState);
            playerStateMachine.AddState(attackStateMachine);
            playerStateMachine.AddState(skill01AttackState);
            playerStateMachine.AddState(skill02AttackState);
            playerStateMachine.AddState(hurtState);
            playerStateMachine.AddState(deadState);
            attackStateMachine.AddState(attackState);
            //playerStateMachine.AddState();
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
            normalState.AddTransition("DeadState",()=>isDead);
            attackStateMachine.AddTransition("DeadState",()=>isDead);
            
            playerStateMachine.EnterState();
            //添加受伤事件
            EventCenter.Instance.AddListener<float>(Frame.Utility.EventType.GetHurt, MinusHp);
        }

        void Start()
        {
            _playerCanvas.gameObject.SetActive(true);
            _playerCanvas.SetPlayerName(photonView.Owner.NickName);
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
            if (!photonView.IsMine)
                return;
            if (cc.enabled)
            {
                cc.SimpleMove(dir * speed);

                if (!IsGrounded())
                {
                    dir.y -= gravity * Time.deltaTime;
                    isOnSkill_01 = false;
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

        private void BindCamera()
        {
            CameraFollowTPS.Instance.BindPlayer(transform);
        }
        
        #region 动画事件 取反bool值
        private void NormalAtkEnd()
        {
            if (!cc.enabled)
            {
                cc.enabled = true;
            }
            _aniCtrler.RestAction();
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
            isOnSkill_02 = false;
        }

        private void HurtEnd()
        {
            isHurt = false;
        }

        #endregion


        private void SkillAttackCheck()
        {
            
        }

        private void HeavyAttackCheck()
        {
            Ray _ray = _atkCamera.ScreenPointToRay(transform.forward);
            RaycastHit info;
            if ( Physics.Raycast(_ray,out info , 2.5f))
            {
                PlayerCrtlr _target = info.transform?.GetComponent<PlayerCrtlr>();
                if (_target != null)
                {
                    _target.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All,5f);
                }
            }
        }
        
        
        private void AttackCheck()
        {
            Ray _ray = _atkCamera.ScreenPointToRay(transform.forward);
            RaycastHit info;
            if ( Physics.Raycast(_ray,out info , 1.5f))
            {
                PlayerCrtlr _target = info.transform?.GetComponent<PlayerCrtlr>();
                if (_target != null)
                {
                    _target.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All,5f);
                }
            }
                
            /*Collider[] colliders = Physics.OverlapCapsule(transform.position, attackpoint, 5f);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    IHurtable target = colliders[i]?.GetComponent<IHurtable>();
                    if (target != null)
                    {
                        if (target == this.GetComponent<IHurtable>())
                            continue;
                        PlayerCrtlr _target = (target as PlayerCrtlr);
                        //target.TakeDamage(normalDamage);
                        _target.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All,5f);
                        //_target._playerCanvas.SetHpPercent((_target.currentHp)/(_target.maxHp));
                    }
                }
            }*/
        }

        [PunRPC]
        public virtual void BeAttack(Vector3 point, Vector3 dir, string effectName, float damage)
        {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                currentHp = 0;
                isDead = true;
                
            }
            
            
        }




        public bool IsHurt()
        {
            return isHurt;
        }

        [PunRPC]
        public void TakeDamage(float damage)
        {
            print(name + "受到了" + damage + "伤害 ，还剩 " + currentHp + " 生命值");
            isHurt = true;
            MinusHp(damage);
            _playerCanvas.SetHpPercent((currentHp-damage)/maxHp);
            if (currentHp <= 0)
                isDead = true;

        }
        
        private void ShowHitEffect(string effectName,Vector3 point,Vector3 dir)
        {
            GameObject go = ObjectPool.Instance.SpawnObj(effectName);
            go.transform.position = point;
            go.transform.forward = dir;
            Destroy(go,2);
        }

        public PhotonView GetPhotonView()
        {
            return photonView;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}