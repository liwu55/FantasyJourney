using Game;
using Game.flag;
using Photon.Pun;

public class SkullSkillEventHandler : BaseFlagSkillEventHandler
{
    //普通攻击的参数
    public float attackDamage = 25;
    public float attackLength = 3;
    public float attackAngle = 180;
    //技能攻击的参数
    public float skillAttackLength = 3.5f;
    public float skillAttackAngle = 180;
    public float skillDamage1 = 35;
    public float skillDamage2 = 30;
    public float dizzyTime = 2;
    
    public void SkullAttack()
    {
        Check(attackDamage,"Blood",0.5f, 
            (hero) => AttackJudge.SectorAttack(transform,
            hero.GetTransform(), attackLength, attackAngle));
    }

    public void SkullSkillAttack1()
    {
        Check(skillDamage1,"Blood",0.2f, 
            (hero) => AttackJudge.SectorAttack(transform,
                hero.GetTransform(), skillAttackLength, skillAttackAngle)); 
    }

    public void SkullSkillAttack2()
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
    }
}
