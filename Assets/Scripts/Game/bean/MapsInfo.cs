using Frame.Utility;
using System.Text;
using UnityEngine;

namespace Game.bean
{
    /// <summary>
    /// 地图信息
    /// </summary>
    public class MapsInfo : MonoBehaviour
    {
        public static MapsInfo Instance;
        public Maps maps;
        private void Awake()
        {
            Instance = this;
            maps = JsonParser.Instance.ParseJsonFile<Maps>("Map/MapInfo");
        }
        /// <summary>
        /// 获取地图总数
        /// </summary>
        /// <returns></returns>
        public int getMapLength()
        {
            return maps.mapInfo.Length;
        }
        /// <summary>
        /// 获取地图ID
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string getMapID(int i)
        {
            return maps.mapInfo[i].mapID;
        }
        /// <summary>
        /// 获取地图名字
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string getMapName(int i)
        {
            return maps.mapInfo[i].name;
        }
        /// <summary>
        /// 获取地图介绍
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string getMapIntroduce(int i)
        {
            return maps.mapInfo[i].introduce;
        }
        /// <summary>
        /// 获取地图缩略图
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Sprite getMapPreview(int i)
        {
            return Resources.Load<Sprite>(addPath(maps.mapInfo[i].preview));
        }
        /// <summary>
        /// 获取地图介绍用图片
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Sprite getMapIntroducePicture(int i)
        {
            return Resources.Load<Sprite>(addPath(maps.mapInfo[i].introducePicture));
        }
        /// <summary>
        /// 根据地图id寻找maps的位置，若未找到返回值为-1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int getMapNmb(string str)
        {
            for(int i=0;i<getMapLength();i++)
            {
                if (str == getMapID(i))
                    return i;
            }
            return -1;
        }
        public string addPath(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Map/" + str);
            return stringBuilder.ToString();
        }
    }
    [System.Serializable]
    public class Maps
    {
        public MapInfo[] mapInfo;
    }
}