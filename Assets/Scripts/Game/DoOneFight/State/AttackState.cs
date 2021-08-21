using System.Collections;
using System.Collections.Generic;
using Frame.FSM;
using Game.DoOneFight.State;
using UnityEngine;
public class AttackState : State
{
    private PlayerCrtlr _playerCrtlr;
    private Animator animator;
    private float checkFixTime = 1.5f;
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
        if (_playerCrtlr.isNrmAtk)
        {
            checkFixTime -= Time.deltaTime;
            if (checkFixTime<=0)
            {
                if (_playerCrtlr.isNrmAtk)
                {
                    _playerCrtlr.isNrmAtk = false;
                    checkFixTime = 1.5f;
                }
                else
                {
                    checkFixTime = 1.5f;
                }
            }
        }
    }
    void OnEnter(Frame.FSM.State obj)
    {
        _playerCrtlr.cc.enabled = false;
        _playerCrtlr._aniCtrler.PlayAnimation((int)CharacterAniId.NormalAttack);
        Debug.Log("AttackState OnEnter");
    }

    void OnExit(Frame.FSM.State obj)
    {
        _playerCrtlr.cc.enabled = true;
    }

    
}
