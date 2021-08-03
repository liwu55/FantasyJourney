using System.Collections.Generic;

namespace Game.Interface
{
    public interface IMapManager
    {
        /// <summary>
        /// 获取所有地图，轮播用
        /// </summary>
        /// <returns></returns>
        List<MapInfo> GetAllMap();

        /// <summary>
        /// 获取随机一张地图，游戏开始时调用
        /// </summary>
        /// <returns></returns>
        MapInfo GetRandomMap();
    }
}