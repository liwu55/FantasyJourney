using System.Collections.Generic;

namespace Game.bean
{
    /// <summary>
    /// 主页需要的数据
    /// </summary>
    public class MainPageInfo
    {
        /// <summary>
        /// 用户数据
        /// </summary>
        public UserInfo userInfo;
        /// <summary>
        /// 地图信息，轮播
        /// </summary>
        public List<MapInfo> maps;
    }
}