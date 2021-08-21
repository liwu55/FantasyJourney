using System;
using System.Collections.Generic;
using Frame.Utility;
using Photon.Pun;
using UnityEngine;

namespace Game.flag
{
    public class BaseSkillEventHandler : MonoBehaviour
    {
        protected IHeroController thisHeroController;

        protected virtual void Awake()
        {
            thisHeroController=GetComponent<IHeroController>();
        }

        protected IHeroController GetHeroController()
        {
            if (thisHeroController == null)
            {
                thisHeroController = GetComponent<SimpleHeroController>();
            }

            return thisHeroController;
        }

        protected void Check(List<IHeroController> enemies, float damage, string effectName, float hitBackRate,
            Func<IHeroController, bool> CheckIfInRange)
        {
            foreach (IHeroController hero in enemies)
            {
                if (!CheckIfInRange(hero))
                {
                    continue;
                }

                Vector3[] data = FindHitPoint(hero);
                if(IsInFlag()){
                    float lifeCur = hero.GetLifeCur();
                    bool dead = lifeCur > 0 && lifeCur <= damage;
                    FlagShowData.Instance.DamageSave(thisHeroController.GetPhotonView().Owner,
                        hero.GetPhotonView().Owner, damage, dead);
                }
                hero.GetPhotonView().RPC("BeAttack", RpcTarget.All,
                    new object[] {data[0], data[1], effectName, damage, hitBackRate});
            }
        }

        private bool IsInFlag()
        {
            return GetComponent<ControllerInit>().inFlag;
        }

        private Vector3[] FindHitPoint(IHeroController hero)
        {
            Vector3[] data = new Vector3[2];
            Ray ray = new Ray();
            ray.origin = transform.position + Vector3.up * 1;
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
                data[1] = -ray.direction;
                return data;
            }

            Debug.Log("没找到");
            return default;
        }

        protected GameObject ShowEffect(string name)
        {
            GameObject go = ObjectPool.Instance.SpawnObj(name);
            go.transform.position = transform.position;
            go.transform.forward = transform.forward;
            return go;
        }
    }
}