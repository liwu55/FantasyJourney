using System.Collections.Generic;

namespace Game.Interface
{
    public interface IHeroManager
    {
        /// <summary>
        /// 获取所有的英雄，用于仓库所有英雄展示
        /// </summary>
        /// <returns></returns>
        List<HeroInfo> GetAllHero();
    }
}