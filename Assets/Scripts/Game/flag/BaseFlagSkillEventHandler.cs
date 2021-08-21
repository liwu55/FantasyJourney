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
            List<IHeroController> enemies = SceneHeroes.Instance.GetAllAdversary(thisHeroController.photonView.Owner);
            base.Check(enemies,damage, effectName, hitBackRate, CheckIfInRange);
        }
    }
}