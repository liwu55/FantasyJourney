using System;
using Game.bean;

namespace Game
{
    /// <summary>
    /// UI事件触发，所有有外部交互UI的触发都从这里发出，不用关心点击的后续
    /// </summary>
    public static class UIEvent
    {
        /// <summary>
        /// 登录点击触发，传入用户名，密码
        /// </summary>
        public static Action<String,String> LoginClick;
        
        /// <summary>
        /// 仓库点击
        /// </summary>
        public static Action StoreClick;

        /// <summary>
        /// 设置点击
        /// </summary>
        public static Action SettingClick;

        /// <summary>
        /// 点击了地图
        /// </summary>
        public static Action<MapInfo> MapClick;

        /// <summary>
        /// 快速开始
        /// </summary>
        public static Action QuickStartClick;

        /// <summary>
        /// 大厅
        /// </summary>
        public static Action LobbyClick;

        /// <summary>
        /// 切换英雄
        /// </summary>
        public static Action<HeroInfo> HeroChange;

        /// <summary>
        /// 点击了游戏开始
        /// </summary>
        public static Action<MapInfo> GameStart;
    }
}