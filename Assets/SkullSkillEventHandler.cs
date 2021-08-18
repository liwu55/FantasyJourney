using System.Collections;
using Frame.Utility;
using Game;
using Game.flag;
using Photon.Pun;
using UnityEngine;

public class SkullSkillEventHandler : BaseFlagSkillEventHandler
{
    //普通攻击的参数
    public float attackDamage = 25;
    public float attackLength = 3;
    public float attackAngle = 180;
    //技能攻击的参数
    private bool skill2DoingDamage = false;
    public float skillRange = 5f;
    public float skillDamage = 5;
    public float skillDamageRate = 0.1f;

    private GameObject skillEffect;
    
    public void SkullAttack1()
    {
        ShowEffect("SkullAttack1");
        Check(attackDamage,"Blood",0.5f, 
            (hero) => AttackJudge.SectorAttack(transform,
            hero.GetTransform(), attackLength, attackAngle));
    }
    
    public void SkullAttack2()
    {
        ShowEffect("SkullAttack2");
        Check(attackDamage,"Blood",0.5f, 
            (hero) => AttackJudge.SectorAttack(transform,
                hero.GetTransform(), attackLength, attackAngle));
    }

    public void SkullSkill2DamageStart()
    {
        skillEffect = ShowEffect("SkullSkill");
        skill2DoingDamage = true;
        StartCoroutine(Skill2Damage());
    }

    public void SkullSkill2DamageEnd()
    {
        //ObjectPool.Instance.RecycleObj(skillEffect);
        skill2DoingDamage = false;
    }

    IEnumerator Skill2Damage()
    {
        while (skill2DoingDamage)
        {
            yield return new WaitForSeconds(skillDamageRate);
            Check(skillDamage,"Blood",0f, 
                (hero) => AttackJudge.CircleAttack(transform,
                    hero.GetTransform(), skillRange)); 
        }
    }

    /*public void SkullSkillAttack2()
    {
        Check(skillDamage2,"Blunt",0f,
            (hero) =>
            {
                bool inRange = AttackJudge.SectorAttack(transform,
                    hero.GetTransform(), skillAttackLength, skillAttackAngle);
                if (inRange)
                {
                    hero.GetPhotonView().RPC("BeStun",RpcTarget.All,new object[]{dizzyTime});
                }

                return inRange;
            });
    }*/
}
