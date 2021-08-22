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
    public class PlayerCrtlr : HealthSystem,IHeroController
    {
        [HideInInspector] public CharacterController cc;
        [HideInInspector] public CharacterAniCtrler _aniCtrler;
        [HideInInspector] public Animator _animator;
        private Camera _atkCamera;
        private PlayerCanvas _playerCanvas;
        public float speed = 3.5f;
        public Vector3 dir;
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
            _playerCanvas = transform.Find("HeroUI").GetComponent<PlayerCanvas>();
            TransParentControl.Instance.Init(transform);
            cc = transform.GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _aniCtrler = gameObject.GetComponent<CharacterAniCtrler>();
            _aniCtrler.Init(_animator);
            if (!photonView.IsMine)
                return;
            BindCamera();
            StateMachine playerStateMachine = new StateMachine("playerStateMachine");
            NormalState normalState = new NormalState("Normal", this);
            AttackState attackState = new AttackState("AttackState", this);
            Skill_01_AttackState skill01AttackState = new Skill_01_AttackState("Skill_01_AttackState", this);
            Skill_02_AttackState skill02AttackState = new Skill_02_AttackState("Skill_02_AttackState", this);
            HurtState hurtState = new HurtState("HurtState", this);
            DeadState deadState = new DeadState("DeadState",this);
            //状态
            playerStateMachine.AddState(normalState);
            playerStateMachine.AddState(attackState);
            playerStateMachine.AddState(skill01AttackState);
            playerStateMachine.AddState(skill02AttackState);
            playerStateMachine.AddState(hurtState);
            playerStateMachine.AddState(deadState);
            //正常 -> 攻击状态
            normalState.AddTransition("AttackState", () => isNrmAtk);
            attackState.AddTransition("Normal", () => !isNrmAtk);
            normalState.AddTransition("Skill_01_AttackState", () => isOnSkill_01);
            skill01AttackState.AddTransition("Normal", () => !isOnSkill_01);
            normalState.AddTransition("Skill_02_AttackState", () => isOnSkill_02);
            skill02AttackState.AddTransition("Normal", () => !isOnSkill_02);
            normalState.AddTransition("HurtState", () => isHurt);
            hurtState.AddTransition("Normal", () => !isHurt);
            attackState.AddTransition("HurtState", () => isHurt);
            normalState.AddTransition("DeadState",()=>isDead);
            attackState.AddTransition("DeadState",()=>isDead);
            
            playerStateMachine.EnterState();
            //添加受伤事件
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
            }
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
        [PunRPC]
        public override void BeAttack(Vector3 point, Vector3 dir, string effectName, float damage,float f)
        {
            ShowHitEffect(effectName,point,dir);
            print(name + "受到了" + damage + "伤害 ，还剩 " + currentHp + " 生命值");
            print("maxHp = " + maxHp);
            isHurt = true;
            MinusHp(damage);
            _playerCanvas.SetHpPercent(currentHp/maxHp);
            if (currentHp <= 0)
            {
                isDead = true;
                UIManager.Instance.ShowModule("DoGameOverPanel",this.photonView.Controller);
            }
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

        public float GetLifeCur()
        {
            return currentHp;
        }
    }
}