using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : SingleTonObj<ComboSystem>
{
    private Animator _animator;

    //private CharacterAniCtrler _aniCtrler;

    //连招List
    private List<int> comboList = new List<int>();
    private int skillCombo_01; //技能触发条件的按键连击数
    private int skillCombo_02;
    private int skillCombo_03;
    private float resetTime = 0;
    private float Skill_01_RestTime = 2f;
    private float Skill_02_RestTime = 1f;

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
        //_aniCtrler = new CharacterAniCtrler();
    }

    private void Start()
    {
        //_aniCtrler.Init(_animator);
    }

    void Update()
    {
        //判断技能一连招
        //CheckSkill01();
        //判断技能二连招
        //CheckSkill02();
    }

    private bool GetKeyCodeDown(int i)
    {
        return Input.GetKeyDown(InputMgr.Instance.inputDic[(EKeyName) i]);
    }

    public bool CheckSkill01()
    {
        if (GetKeyCodeDown((int) EKeyName.heavyAttack) && skillCombo_01 < 3)
        {
            //播放动画
            //_aniCtrler.PlayAnimation((int) CharacterAniId.NormalAttack);
            skillCombo_01++;
        }

        if (skillCombo_01 > 0)
        {
            //超过限定的时间连不上按键 按键连击数归零
            Skill_01_RestTime -= Time.deltaTime;
            if (Skill_01_RestTime <= 0)
            {
                skillCombo_01 = 0;
                Skill_01_RestTime = 2f;
            }
        }
        if (skillCombo_01 >= 3)
        {
            skillCombo_01 = 0;
            return true;
            //打出连击按键 释放技能
            //_aniCtrler.PlayAnimation((int) CharacterAniId.HeavyAttack);
        }
        return false;
    }

    public bool CheckSkill02()
    {
        if (GetKeyCodeDown((int) EKeyName.front) && skillCombo_02 < 3)
        {
            skillCombo_02 = 1;
        }

        if (skillCombo_02 > 0)
        {
            //超过限定的时间连不上按键 按键连击数归零
            Skill_02_RestTime -= Time.deltaTime;
            if (Skill_02_RestTime <= 0)
            {
                skillCombo_02 = 0;
                Skill_02_RestTime = 1f;
            }
            if (GetKeyCodeDown((int) EKeyName.leftMove))
                skillCombo_02 = 2;
            if (skillCombo_02 == 2 && GetKeyCodeDown((int) EKeyName.rightMove))
                skillCombo_02 = 3;
            if (skillCombo_02 == 3 && GetKeyCodeDown((int) EKeyName.Skill02))
                skillCombo_02 = 4;
        }
        if (skillCombo_02 == 4)
        {
            skillCombo_02 = 0;
            return true;
            //_aniCtrler.PlayAnimation((int) CharacterAniId.Skill_1);
        }
        return false;
    }
}