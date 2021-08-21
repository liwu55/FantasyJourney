using System.Collections;
using Frame.Utility;
using Game;
using Game.flag;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class NiuNiuSkillEventHandler : BaseFlagSkillEventHandler
{
    //普通攻击的参数
    public float attackLength = 3;
    public float attackAngle = 120;
    //冲刺的参数
    private bool rushing = false;
    [FormerlySerializedAs("showFrogRate")] public float showRushFrogRate = 0.05f;
    public float rushDamageRate = 0.05f;
    public float rushDamageValue = 5;
    public float rushDamageRange = 2;
  
    public void NiuNiuAttack()
    {
        Debug.Log("牛牛普通攻击");
        ShowEffect("NiuNiuAttack");
        Check(40f,"NiuNiuHit",1f,(hero)=>
        {
            return AttackJudge.SectorAttack(transform, hero.GetTransform(), attackLength, attackAngle);
        });
    }

    public void NiuNiuRushStart()
    {
        rushing = true;
        StartCoroutine(ShowRunFrog());
        //房主计算伤害
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        Debug.Log("牛牛冲刺开始");
        StartCoroutine(JudgeRushDamage());
    }

    private IEnumerator ShowRunFrog()
    {
        while (rushing)
        {
            ShowRushFrog();
            yield return new WaitForSeconds(showRushFrogRate);
        }
    }

    private void ShowRushFrog()
    {
        GameObject frog = ObjectPool.Instance.SpawnObj("NiuNiuRun");
        frog.transform.position = transform.position;
    }

    public void NiuNiuRushEnd()
    {
        rushing = false;
        
        //房主计算伤害
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        Debug.Log("牛牛冲刺结束");
    }

    IEnumerator JudgeRushDamage()
    {
        while (rushing)
        {
            RushDamage();
            yield return new WaitForSeconds(rushDamageRate);
        }
    }

    private void RushDamage()
    {
        Debug.Log("牛牛冲刺攻击");
        Check(rushDamageValue,"NiuNiuHit",3, (hero) =>
        {
            Vector3 i2Target = hero.GetTransform().position - transform.position;
            //超出冲刺攻击范围
            if (i2Target.magnitude > rushDamageRange)
            {
                return false;
            }
            return true;
        });
    }
}