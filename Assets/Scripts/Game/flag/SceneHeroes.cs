﻿using System.Collections.Generic;
using Photon.Realtime;

namespace Game.flag
{
    public class SceneHeroes:SingleTonObj<SceneHeroes>
    {
        private List<IHeroController> heroes;

        private SceneHeroes()
        {
            heroes=new List<IHeroController>();
        }
        
        public void Add(IHeroController hero)
        {
            heroes.Add(hero);
        }

        public List<IHeroController> GetAll()
        {
            return heroes;
        }
        
        /// <summary>
        /// 获取所有敌人
        /// </summary>
        /// <returns></returns>
        public List<IHeroController> GetOthers(Player player)
        {
            List<IHeroController> adversaries=new List<IHeroController>();
            foreach (IHeroController ihc in heroes)
            {
                if (ihc.GetPhotonView().Owner != player)
                {
                    adversaries.Add(ihc);
                }
            }

            return adversaries;
        }

        /// <summary>
        /// 获取所有敌人
        /// </summary>
        /// <returns></returns>
        public List<IHeroController> GetAllAdversary(Player player)
        {
            string expectTeam=new PhotonPlayerWrap(player).GetTeam();
            List<IHeroController> adversaries=new List<IHeroController>();
            foreach (IHeroController ihc in heroes)
            {
                string team = new PhotonPlayerWrap(ihc.GetPhotonView().Owner).GetTeam();
                if (team != expectTeam)
                {
                    adversaries.Add(ihc);
                }
            }

            return adversaries;
        }
    }
}