using System.Collections.Generic;
using Frame.Utility;

namespace Game.bean
{
    /// <summary>
    /// 玩家数据，包括静态的用户数据和动态的数据，如颜色，队伍等
    /// </summary>
    public class PlayerInfo:SingleTonObj<PlayerInfo>
    {
        public UserInfo _userInfo;
        public HeroInfo chooseHero;

        public void Init()
        {
            chooseHero=new HeroInfo();
            chooseHero.id = 0;
        }

        public bool IsChoose(int heroId)
        {
            return chooseHero.id == heroId;
        }

        public void SetChooseHero(int heroId)
        {
            chooseHero.id = heroId;
        }

        public string GetChooseHeroPath()
        {
            List<HeroInfos.Hero> allHero = HeroManager.Instance.GetAllHero();
            for (int i = 0; i < allHero.Count; i++)
            {
                if(allHero[i].id==chooseHero.id)
                {
                    return ConfigurationManager.Instance.GetPathByName(allHero[i].model);
                }
            }
            return ConfigurationManager.Instance.GetPathByName("niuniu");
        }
    }
}