using System;
using Frame.FSM;
using Game.flag;
using Game.flag.State;
using Photon.Pun;
using UnityEngine;

public class SimpleHeroController : MonoBehaviourPunCallbacks
{
    [NonSerialized] public Vector3 velocity;
    [NonSerialized] public bool isSkilling1 = false;
    [NonSerialized] public bool isSkilling2 = false;
    protected CharacterController cc;
    //状态机
    protected StateMachine heroStateMachine;
    protected NormalState normalState;
    protected State skill1State;
    protected State skill2State;
    protected State overState;
    protected DizzyState dizzyState;

    [NonSerialized] public float life = 100;

    protected virtual void Awake()
    {
        FlagHeroes.Instance.Add(this);
        
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
        cc = GetComponent<CharacterController>();
        //状态机设置
        heroStateMachine = new StateMachine("HeroState");
        normalState = new NormalState("Normal", this);
        skill1State = new Skill1State("Skilling1", this);
        skill2State = new Skill2State("Skilling2", this);
        overState = new OverState("Over");
        dizzyState = new DizzyState("Dizzy",this);

        normalState.AddTransition("Skilling1", () => isSkilling1);
        normalState.AddTransition("Skilling2", () => isSkilling2);
        normalState.AddTransition("Over", () => FlagData.Instance.gameOver);
        normalState.AddTransition("Dizzy", () => life<=0);

        skill1State.AddTransition("Normal", () => !isSkilling1);
        skill1State.AddTransition("Dizzy", () => life<=0);

        skill2State.AddTransition("Normal", () => !isSkilling2);
        skill2State.AddTransition("Dizzy", () => life<=0);
        
        dizzyState.AddTransition("Normal",dizzyState.IsDizzyTimeEnough);

        heroStateMachine.AddState(normalState);
        heroStateMachine.AddState(skill1State);
        heroStateMachine.AddState(skill2State);
        heroStateMachine.AddState(overState);
        heroStateMachine.AddState(dizzyState);
        //增加额外的状态
        AddSelfState();

        heroStateMachine.EnterState();
        
    }
    
    private void InitPublic()
    {
        
    }
    
    protected virtual void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        cc.SimpleMove(velocity);
    }

    public void Skill1End()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        isSkilling1 = false;
    }

    public void Skill2End()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        isSkilling2 = false;
    }

    [PunRPC]
    public virtual void BeAttack(Vector3 point,Vector3 dir, float damage,float hitBackFactor = 1)
    {
        life -= damage;
        if (life < 0)
        {
            life = 0;
        }
        Debug.Log("被攻击,伤害"+damage+",剩余血量:"+life);
    }
    

    protected virtual void AddSelfState()
    {
        
    }

    public virtual void ResetBlood()
    {
        
    }
}