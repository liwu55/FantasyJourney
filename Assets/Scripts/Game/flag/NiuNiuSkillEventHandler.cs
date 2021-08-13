using System.Collections;
using System.Collections.Generic;
using Game.flag;
using Photon.Pun;
using UnityEngine;

public class NiuNiuSkillEventHandler : MonoBehaviour
{
    //普通攻击的参数
    public float attackLength = 3;
    public float attackAngle = 120;
    //冲刺的参数
    private bool rushing = false;
    public float rushDamageRate = 0.05f;
    public float rushDamageValue = 5;
    public float rushDamageRange = 2;
    
    private SimpleHeroController thisHeroController;
    
    private void Awake()
    {
        thisHeroController=GetComponent<SimpleHeroController>();
    }

    public void NiuNiuAttack()
    {
        //房主计算伤害
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        Debug.Log("牛牛普通攻击");
        List<SimpleHeroController> heroes = FlagHeroes.Instance.GetAllAdversary(thisHeroController.photonView.Owner);
        foreach (SimpleHeroController hero in heroes)
        {
            //自己人
            if (hero.photonView.Owner == thisHeroController.photonView.Owner)
            {
                continue;
            }

            Vector3 i2Target = hero.transform.position - transform.position;
            //超出攻击范围
            if (i2Target.magnitude > attackLength)
            {
                continue;
            }

            Vector3 forward = transform.forward;
            i2Target.Normalize();
            float cosValue = Vector3.Dot(forward, i2Target);
            float angle = Mathf.Rad2Deg * Mathf.Acos(cosValue);
            Debug.Log("angle=" + angle);
            //超出角度
            if (Mathf.Abs(angle) > attackAngle / 2)
            {
                continue;
            }

            Vector3[] data = FindHitPoint(hero);
            hero.photonView.RPC("BeAttack",RpcTarget.All,
                new object[]{data[0],data[1],40.0f,1.0f});
        }
    }

    private Vector3[] FindHitPoint(SimpleHeroController hero)
    {
        Vector3[] data=new Vector3[2];
        Ray ray = new Ray();
        ray.origin = transform.position+Vector3.up*1;
        ray.direction = (hero.transform.position + Vector3.up * 1) - ray.origin;
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 10);
        for (int i = 0; i < raycastHits.Length; i++)
        {
            if (raycastHits[i].collider.GetComponent<NiuNiuSkillEventHandler>() == this)
            {
                Debug.Log("跳过自己");
                continue;
            }
            Debug.Log("返回");
            data[0] = raycastHits[i].point;
            data[1] = - ray.direction;
            return data;
        }
        Debug.Log("没找到");
        return default;
    }

    public void NiuNiuRushStart()
    {
        //房主计算伤害
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        Debug.Log("牛牛冲刺开始");
        rushing = true;
        StartCoroutine(JudgeRushDamage());
    }

    public void NiuNiuRushEnd()
    {
        //房主计算伤害
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        rushing = false;
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
        List<SimpleHeroController> heroes = FlagHeroes.Instance.GetAllAdversary(thisHeroController.photonView.Owner);
        foreach (SimpleHeroController hero in heroes)
        {
            //自己人
            if (hero.photonView.Owner == thisHeroController.photonView.Owner)
            {
                continue;
            }

            Vector3 i2Target = hero.transform.position - transform.position;
            //超出冲刺攻击范围
            if (i2Target.magnitude > rushDamageRange)
            {
                continue;
            }

            Vector3[] data = FindHitPoint(hero);
            hero.photonView.RPC("BeAttack",RpcTarget.All,
                new object[]{data[0],data[1],rushDamageValue,4.0f});
        }
    }
}