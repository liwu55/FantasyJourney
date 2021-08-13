using System;
using Frame.FSM;
using Game.flag.State;
using Photon.Pun;
using UnityEngine;

public class SimpleHeroController : MonoBehaviourPunCallbacks
{
    [NonSerialized] public Vector3 velocity;
    [NonSerialized] public bool isSkilling1 = false;
    [NonSerialized] public bool isSkilling2 = false;
    private CharacterController cc;
    //状态机
    protected StateMachine heroStateMachine;
    protected NormalState normalState;
    protected State skill1State;
    protected State skill2State;
    protected State overState;

    protected virtual void Awake()
    {
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

        normalState.AddTransition("Skilling1", () => isSkilling1);
        normalState.AddTransition("Skilling2", () => isSkilling2);
        normalState.AddTransition("Over", () => FlagData.Instance.gameOver);

        skill1State.AddTransition("Normal", () => !isSkilling1);

        skill2State.AddTransition("Normal", () => !isSkilling2);

        heroStateMachine.AddState(normalState);
        heroStateMachine.AddState(skill1State);
        heroStateMachine.AddState(skill2State);
        heroStateMachine.AddState(overState);
        //增加额外的状态
        AddSelfState();

        heroStateMachine.EnterState();
    }
    
    private void InitPublic()
    {
        
    }

    private void FixedUpdate()
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
   

    protected virtual void AddSelfState()
    {
        
    }
}