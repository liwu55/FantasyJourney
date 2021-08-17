using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Game.flag
{  
    
    public class BaseSkillEventHandler:MonoBehaviour
    {
        protected SimpleHeroController thisHeroController;
    
        protected virtual void Awake()
        {
            thisHeroController=GetComponent<SimpleHeroController>();
        }

        protected void Check(float damage,string effectName,float hitBackRate,Func<IHeroController,bool> CheckIfInRange)
        {
            //房主计算伤害
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }
            List<IHeroController> heroes = SceneHeroes.Instance.GetAllAdversary(thisHeroController.photonView.Owner);
            foreach (IHeroController hero in heroes)
            {
                if (!CheckIfInRange(hero))
                {
                    continue;
                }
                Vector3[] data = FindHitPoint(hero);
                hero.GetPhotonView().RPC("BeAttack",RpcTarget.All,
                    new object[]{data[0],data[1],effectName,damage,hitBackRate});
            }
        }
        
        private Vector3[] FindHitPoint(IHeroController hero)
        {
            Vector3[] data=new Vector3[2];
            Ray ray = new Ray();
            ray.origin = transform.position+Vector3.up*1;
            ray.direction = (hero.GetTransform().position + Vector3.up * 1) - ray.origin;
            RaycastHit[] raycastHits = Physics.RaycastAll(ray, 10);
            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider.GetComponent<BaseSkillEventHandler>() == this)
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
    }
}