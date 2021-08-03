using System.Collections.Generic;
using Game.bean;
using Game.Interface;

namespace Game
{
    public class TestMapManager:IMapManager
    {
        public List<MapInfo> GetAllMap()
        {
            return new List<MapInfo>();
        }

        public MapInfo GetRandomMap()
        {
            return new MapInfo();
        }
    }
}