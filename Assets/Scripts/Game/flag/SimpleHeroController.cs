using System;
using Frame.FSM;
using Game.flag.State;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SimpleHeroController : MonoBehaviourPun
{
    [NonSerialized]
    public Vector3 velocity;
    [NonSerialized]
    public bool isSkilling1 = false;
    [NonSerialized]
    public bool isSkilling2 = false;
    [NonSerialized]
    public bool occuping=false;
    public float operationDistance = 3;
    public string pointSign = "BluePoint";
    private CharacterController cc;
    //正在占领的据点
    public PointBase occupingPoint;
    //进度条
    private Loading loading;

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        cc = GetComponent<CharacterController>();
        loading = transform.Find("Loading").GetComponent<Loading>();
        //状态机设置
        StateMachine heroStateMachine = new StateMachine("HeroState");
        State normalState = new NormalState("Normal",this);
        State skill1State=new Skill1State("Skilling1",this);
        State skill2State=new Skill2State("Skilling2",this);
        State occupingState=new OccupingState("Occuping",this);
        
        normalState.AddTransition("Skilling1", () => isSkilling1);
        normalState.AddTransition("Skilling2", () => isSkilling2);
        normalState.AddTransition("Occuping", () => occuping);
        
        skill1State.AddTransition("Normal", () => !isSkilling1);
        
        skill2State.AddTransition("Normal", () => !isSkilling2);
        
        occupingState.AddTransition("Normal", () => !occuping);
        
        heroStateMachine.AddState(normalState);
        heroStateMachine.AddState(skill1State);
        heroStateMachine.AddState(skill2State);
        heroStateMachine.AddState(occupingState);
        
        heroStateMachine.EnterState();
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

    //占领的进度改变了
    public void OnOccupyProgressChange(float progress)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (!loading.gameObject.activeSelf)
        {
            loading.gameObject.SetActive(true);
        }
        loading.ShowProgress(progress);
        if (progress >= 1)
        {
            loading.gameObject.SetActive(false);
            occuping = false;
            occupingPoint.ChangeOccupiedSign(pointSign);
        }
    }
}