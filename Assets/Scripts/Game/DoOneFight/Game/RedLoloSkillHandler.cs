using Frame.Utility;
using Game.flag;
using Photon.Pun;
using UnityEngine;

namespace Game.DoOneFight.State
{
    public class RedLoloSkillHandler: BaseSkillEventHandler
    {
        float attackLength = 3f;
        private float attackAngle = 180f;
        private void AttackCheck()
        {
            
            Check(SceneHeroes.Instance.GetOthers(thisHeroController.GetPhotonView().Owner),5f,"CatHit",0.5f, 
                (hero) => AttackJudge.SectorAttack(transform,
                    hero.GetTransform(), attackLength, attackAngle));
        }
        private void HeavyAttackCheck()
        {
            Check(SceneHeroes.Instance.GetOthers(thisHeroController.GetPhotonView().Owner),10f,"CatHit",0.5f, 
                (hero) => AttackJudge.SectorAttack(transform,
                    hero.GetTransform(), attackLength*1.5f, attackAngle));
        }
        private void HeavySkillCheck()
        {
        
            Check(SceneHeroes.Instance.GetOthers(thisHeroController.GetPhotonView().Owner),10f,"CatHit",0.5f, 
                (hero) => AttackJudge.SectorAttack(transform,
                    hero.GetTransform(), attackLength, attackAngle));
        }
        private void HeavySkillSecCheck()
        {
            GameObject go = ObjectPool.Instance.SpawnObj("NiuNiuHit");
            go.transform.position = thisHeroController.GetTransform().position;
            go.transform.forward = Vector3.up;
            go.transform.localScale = new Vector3(5f,5f,5f);
            Check(SceneHeroes.Instance.GetOthers(thisHeroController.GetPhotonView().Owner),5f,"CatHit",0.5f, 
                (hero) => AttackJudge.CircleAttack(transform,
                    hero.GetTransform(), attackLength));
        }
    }
}