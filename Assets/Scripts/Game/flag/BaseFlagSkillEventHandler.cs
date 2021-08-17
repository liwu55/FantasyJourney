using System;
using System.Collections.Generic;

namespace Game.flag
{
    public class BaseFlagSkillEventHandler:BaseSkillEventHandler
    {
        protected void Check(float damage, string effectName, float hitBackRate, Func<IHeroController, bool> CheckIfInRange)
        {
            List<IHeroController> enemies = SceneHeroes.Instance.GetAllAdversary(thisHeroController.photonView.Owner);
            base.Check(enemies,damage, effectName, hitBackRate, CheckIfInRange);
        }
    }
}