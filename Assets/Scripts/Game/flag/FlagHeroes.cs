using System.Collections.Generic;
using Photon.Realtime;

namespace Game.flag
{
    public class FlagHeroes:SingleTonObj<FlagHeroes>
    {
        private List<SimpleHeroController> heroes;

        private FlagHeroes()
        {
            heroes=new List<SimpleHeroController>();
        }
        
        public void Add(SimpleHeroController hero)
        {
            heroes.Add(hero);
        }

        public List<SimpleHeroController> GetAll()
        {
            return heroes;
        }

        /// <summary>
        /// 获取所有敌人
        /// </summary>
        /// <returns></returns>
        public List<SimpleHeroController> GetAllAdversary(Player player)
        {
            string expectTeam=new PhotonPlayerWrap(player).GetTeam();
            List<SimpleHeroController> adversaries=new List<SimpleHeroController>();
            foreach (SimpleHeroController shc in heroes)
            {
                string team = new PhotonPlayerWrap(shc.photonView.Owner).GetTeam();
                if (team != expectTeam)
                {
                    adversaries.Add(shc);
                }
            }

            return adversaries;
        }
    }
}