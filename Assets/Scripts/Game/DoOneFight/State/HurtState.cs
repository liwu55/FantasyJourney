using System.Collections;
using System.Collections.Generic;
using Game.DoOneFight.State;
using UnityEditor;
using UnityEngine;

public class HurtState : Frame.FSM.State
{
    private PlayerCrtlr _playerCrtlr;
    private Animator _animator;
    private CharacterAniCtrler _aniCtrler;
    public HurtState(string stateName ,PlayerCrtlr _playerCrtlr) : base(stateName)
    {
        this._playerCrtlr = _playerCrtlr;
        _animator = _playerCrtlr.transform.GetComponent<Animator>();
        _playerCrtlr._aniCtrler.Init(_animator);
        OnStateEnter += OnEnter;
        OnStateExit += OnExit;
    }

    void OnEnter(Frame.FSM.State obj)
    {
        Debug.Log("hurt enter");
        _playerCrtlr.cc.enabled = false;
        _playerCrtlr._aniCtrler.PlayAnimation((int)CharacterAniId.Hurt);
        //_animator.SetLayerWeight(1,0);
    }

    void OnExit(Frame.FSM.State obj)
    {
        //_animator.SetLayerWeight(1,1);
        _playerCrtlr._aniCtrler.RestAction();
        _playerCrtlr.isOnSkill_01 = false;
        _playerCrtlr.isOnSkill_02 = false;
        _playerCrtlr.isNrmAtk = false;
        _playerCrtlr.cc.enabled = true;
    }
}
