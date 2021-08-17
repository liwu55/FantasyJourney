using Frame.Utility;
using Game;
using Game.flag;
using UnityEngine;

public class CatSkillEventHandler : BaseSkillEventHandler
{
    //普通攻击的参数
    public float attackLength = 3;
    public float attackAngle = 180;
    //技能参数
    public float skillRange = 5;
    public float skillDamage = 50;
    
    public void CatAttack()
    {
        Check(20,"CatHit",0.5f, 
            (hero) => AttackJudge.SectorAttack(transform,
                hero.GetTransform(), attackLength, attackAngle));
    }

    public void CatSkill()
    {
        ShowCatSkillEffect();
        Check(skillDamage,"CatHit",5f, 
            (hero) => AttackJudge.CircleAttack(transform, 
                hero.GetTransform(), skillRange));
    }
    
    private void ShowCatSkillEffect()
    {
        GameObject skill = ObjectPool.Instance.SpawnObj("CatSkill");
        skill.transform.position = transform.position;
    }
}
