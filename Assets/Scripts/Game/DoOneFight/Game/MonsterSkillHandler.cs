using System.Xml.Serialization;
using Frame.Utility;
using Game.flag;
using Photon.Pun;
using UnityEngine;

namespace Game.DoOneFight.State
{
    public class MonsterSkillHandler:BaseSkillEventHandler
    {
        float attackLength = 3f;
        private float attackAngle = 180f;
        
        private void AttackCheck()
        {    
            Check(SceneHeroes.Instance.GetOthers(thisHeroController.GetPhotonView().Owner),15f,"CatHit",0.5f, 
                (hero) => AttackJudge.SectorAttack(transform,
                    hero.GetTransform(), attackLength, attackAngle));
        }
        private void HeavySkillCheck()
        {
           
            GameObject go = ObjectPool.Instance.SpawnObj("MonsPillarBlast");
            go.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            go.transform.position = thisHeroController.GetTransform().position;
            Check(SceneHeroes.Instance.GetOthers(thisHeroController.GetPhotonView().Owner),20f,"CatHit",0.5f, 
                (hero) => AttackJudge.CircleAttack(transform,
                    hero.GetTransform(), attackLength));
        }


        private void SkillSecCheck()
        {
            GameObject go = ObjectPool.Instance.SpawnObj("MonsSphereBlast");
            go.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            go.transform.position = thisHeroController.GetTransform().position;
            Check(SceneHeroes.Instance.GetOthers(thisHeroController.GetPhotonView().Owner),30f,"CatHit",0.5f, 
                (hero) => AttackJudge.CircleAttack(transform,
                    hero.GetTransform(), attackLength * 1.75f));
        }
    }
}