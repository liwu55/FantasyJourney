using Game.flag;

namespace Game.DoOneFight.State
{
    public class RedLoloSkillHandler: BaseSkillEventHandler
    {
        
        float attackLength = 3f;
        private float attackAngle = 180f;
        private void attackCheck()
        {
            Check(5f,"CatHit",0.5f, 
                (hero) => AttackJudge.SectorAttack(transform,
                    hero.GetTransform(), attackLength, attackAngle));
        }
        
        
    }
}