using Game.bean;

namespace Game.Interface
{
    public interface IUIController
    {
        /// <summary>
        /// 展示登录页面
        /// </summary>
        void ShowLogin();

        /// <summary>
        /// 主页面
        /// </summary>
        void ShowMain(MainPageInfo mainPageInfo);

        /// <summary>
        /// 仓库
        /// </summary>
        void ShowStore();

        /// <summary>
        /// 登录失败
        /// </summary>
        void ShowLoginError();

        /// <summary>
        /// 设置页面
        /// </summary>
        void ShowSetting();

        /// <summary>
        /// 展示地图玩法说明
        /// </summary>
        void ShowMapDetail();

        /// <summary>
        /// 显示大厅
        /// </summary>
        void ShowLobby();

        /// <summary>
        /// 显示房间信息
        /// </summary>
        void ShowRoomInfo();
    }
}