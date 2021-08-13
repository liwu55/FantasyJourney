using System.Collections;
using System.Collections.Generic;
using Frame.FSM;
using Game.DoOneFight.State;
using UnityEngine;

public class AttackState : State
{
    private PlayerCrtlr _playerCrtlr;
    private Animator animator;
    public AttackState(string stateName,PlayerCrtlr _playerCrtlr) : base(stateName)
    {
        this._playerCrtlr = _playerCrtlr;
        animator = _playerCrtlr.GetComponent<Animator>();
        _playerCrtlr._aniCtrler.Init(animator);
        OnStateEnter += OnEnter;
        OnStateUpdate += OnUpdate;
        OnStateExit += OnExit;
    }


    void OnUpdate(Frame.FSM.State obj)
    {
        _playerCrtlr.isHurt = Input.GetKeyDown(KeyCode.Z);
    }
    void OnEnter(Frame.FSM.State obj)
    {
        _playerCrtlr.cc.enabled = false;
        Debug.Log("AttackState OnEnter");
    }

    void OnExit(Frame.FSM.State obj)
    {
        _playerCrtlr.cc.enabled = true;
    }

    
}
