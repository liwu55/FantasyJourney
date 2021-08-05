using System.Collections.Generic;
using Frame.Utility;
using Game.bean;
using Game.Interface;

namespace Game
{
    public class HeroManager:IHeroManager
    {
        public List<HeroInfos.Hero> GetAllHero()
        {
            return ConfigurationManager.Instance.GetHeroInfos();
        }
    }
}