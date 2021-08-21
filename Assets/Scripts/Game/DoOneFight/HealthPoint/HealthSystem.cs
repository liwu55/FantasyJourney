using Frame.Utility;
using Photon.Pun;
using UnityEngine;
namespace Game.DoOneFight.HealthPoint
{
    public class HealthSystem:MonoBehaviourPun 
    {
        public float maxHp = 10;
        public float currentHp = 10;
        public bool isHealing;

        
        
        //这个方法应该监听进入Hurt状态
        public void MinusHp(float damage)
        {
            currentHp = Mathf.Clamp(currentHp -= damage, 0, maxHp);
        }

        //这个方法应该监听吃果子
        public void AddHp(float hpPerSec,float time)
        {
            time -= Time.deltaTime;
            if(time>=0)
                currentHp = Mathf.Clamp(currentHp += hpPerSec*Time.deltaTime,hpPerSec*Time.deltaTime,maxHp);
        }

        float HpPercent()
        {
            return 1f / maxHp * currentHp;
        }

        public virtual void BeAttack(Vector3 point, Vector3 dir, string effectName, float damage,float f)
        {
            
        }
             
    }
}