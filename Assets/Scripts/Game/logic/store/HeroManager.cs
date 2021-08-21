using System.Collections.Generic;
using Frame.Utility;
using Game.bean;
using Game.Interface;

namespace Game
{
    public class HeroManager:SingleTonObj<HeroManager>,IHeroManager
    {
        private HeroManager(){}
        public List<HeroInfos.Hero> GetAllHero()
        {
            return ConfigurationManager.Instance.GetHeroInfos();
        }
    }
}