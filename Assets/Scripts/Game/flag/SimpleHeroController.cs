using System;
using Frame.FSM;
using Frame.Utility;
using Game;
using Game.flag;
using Game.flag.State;
using Photon.Pun;
using UnityEngine;
using Object = System.Object;

public class SimpleHeroController : MonoBehaviourPunCallbacks, IHeroController
{
    [NonSerialized] public Vector3 velocity;
    [NonSerialized] public bool isSkilling1 = false;
    [NonSerialized] public bool isSkilling2 = false;
    [NonSerialized] public bool isDizzy = false;

    protected CharacterController cc;
    protected Animator anim;

    //状态机
    protected StateMachine heroStateMachine;
    protected NormalState normalState;
    protected State skill1State;
    protected State skill2State;
    [NonSerialized] public DizzyState dizzyState;

    [NonSerialized] public float life = 100;

    protected virtual void Awake()
    {
        Debug.Log("SimpleHeroController Awake");
        SceneHeroes.Instance.Add(this);

        InitPublic();
        if (!photonView.IsMine)
        {
            return;
        }
        
        InitSelf();
        BindCamera();
    }

    private void BindCamera()
    {
        Third.Instance.BindPlayer(transform);
    }

    private void InitSelf()
    {
        Debug.Log("SimpleHeroController InitSelf");
        cc = GetComponent<CharacterController>();
        //状态机设置
        heroStateMachine = new StateMachine("HeroState");
        normalState = new NormalState("Normal", this);
        skill1State = new Skill1State("Skilling1", this);
        skill2State = new Skill2State("Skilling2", this);

        dizzyState = new DizzyState("Dizzy", this);

        normalState.AddTransition("Skilling1", () => isSkilling1);
        normalState.AddTransition("Skilling2", () => isSkilling2);

        normalState.AddTransition("Dizzy", () => life <= 0 || isDizzy);

        skill1State.AddTransition("Normal", () => !isSkilling1);
        skill1State.AddTransition("Dizzy", () => life <= 0 || isDizzy);

        skill2State.AddTransition("Normal", () => !isSkilling2);
        skill2State.AddTransition("Dizzy", () => life <= 0 || isDizzy);

        dizzyState.AddTransition("Normal", dizzyState.IsDizzyTimeEnough);

        heroStateMachine.AddState(normalState);
        heroStateMachine.AddState(skill1State);
        heroStateMachine.AddState(skill2State);
        heroStateMachine.AddState(dizzyState);
        //增加额外的状态
        AddSelfState();

        heroStateMachine.EnterState();
    }

    private void InitPublic()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        //simpleMove没有y轴速度，自带重力
        // cc.SimpleMove(velocity);
        //Debug.Log("SimpleHeroController FixedUpdate v="+velocity);
        cc.Move(velocity * Time.deltaTime);
    }

    public void Skill1End()
    {
        isSkilling1 = false;
        anim.SetBool("Skill1", false);
    }

    public void Skill2End()
    {
        isSkilling2 = false;
        anim.SetBool("Skill2", false);
    }

    [PunRPC]
    public virtual void BeAttack(Vector3 point, Vector3 dir, string effectName, float damage, float hitBackFactor = 1)
    {
        life -= damage;
        if (life <= 0)
        {
            life = 0;
            if (photonView.IsMine)
            {
                //眩晕5s
                dizzyState.isDead = true;
                dizzyState.dizzyTime = 5;
            }
        }

        ShowDamageNumber(damage);
        Debug.Log("被攻击,伤害" + damage + ",剩余血量:" + life);
    }

    private void ShowDamageNumber(float damage)
    {
        ObjectPool.Instance.SpawnObj("DamageNumber",
            data: new Object[] {damage, transform.position + Vector3.up * 1});
    }

    [PunRPC]
    public void BeStun(float dizzyTime)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        dizzyState.dizzyTime = dizzyTime;
        dizzyState.isDead = false;
        isDizzy = true;
    }

    protected virtual void AddSelfState()
    {
    }

    public void ResetState(bool resetBlood)
    {
        isSkilling1 = false;
        isSkilling2 = false;
        isDizzy = false;
        if (resetBlood)
        {
            ResetBlood();
        }
    }

    protected virtual void ResetBlood()
    {
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
        return life;
    }
}