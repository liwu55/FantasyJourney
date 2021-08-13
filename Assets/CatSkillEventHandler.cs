using Game.flag;
using UnityEngine;

public class CatSkillEventHandler : BaseSkillEventHandler
{
    //普通攻击的参数
    public float attackLength = 4;
    public float attackAngle = 180;
    
    public void CatAttack()
    {
        Check(20,"NiuNiuHit",0.5f, (hero) =>
        {
            Vector3 i2Target = hero.transform.position - transform.position;
            //超出攻击范围
            if (i2Target.magnitude > attackLength)
            {
                return false;
            }

            Vector3 forward = transform.forward;
            i2Target.Normalize();
            float cosValue = Vector3.Dot(forward, i2Target);
            float angle = Mathf.Rad2Deg * Mathf.Acos(cosValue);
            //超出角度
            if (Mathf.Abs(angle) > attackAngle / 2)
            {
                return false;
            }
            return true;
        });
    }
}
