using System;
using System.Collections.Generic;
using Photon.Pun;

namespace Game.flag
{
    public class BaseFlagSkillEventHandler:BaseSkillEventHandler
    {
        protected void Check(float damage, string effectName, float hitBackRate, Func<IHeroController, bool> CheckIfInRange)
        {
            if (!PhotonNetwork.InRoom)
            {
                return;
            }
            //房主计算伤害
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            bool inFlag = GetComponent<ControllerInit>().inFlag;
            List<IHeroController> enemies;
            if(inFlag){
                enemies = SceneHeroes.Instance.GetAllAdversary(GetHeroController().photonView.Owner);
            }
            else
            {
                enemies = SceneHeroes.Instance.GetOthers(GetHeroController().photonView.Owner);
            }
            base.Check(enemies,damage, effectName, hitBackRate, CheckIfInRange);
        }
    }
}