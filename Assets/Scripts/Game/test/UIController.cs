using Game.bean;
using Game.Interface;
using UnityEngine;

namespace Game
{
    public class UIController:IUIController
    {
        public void ShowLogin()
        {
            UIManager.Instance.ShowModule("Login");
        }

        public void ShowLoginSuc(MainPageInfo mainPageInfo)
        {
            UIManager.Instance.ShowModule("LoginSuc", mainPageInfo.userInfo.username);
            Debug.Log("显示主页面");
        }

        public void ShowStore()
        {
            UIManager.Instance.ShowModule("Store");
        }

        public void ShowLoginError()
        {
            UIManager.Instance.GetModule<LoginModule>("Login").PlayMentionAnime();
            Debug.Log("登录失败");
        }

        public void ShowSetting()
        {
        }

        public void ShowMapDetail()
        {
        }

        public void ShowLobby()
        {
        }

        public void ShowRoomInfo()
        {
        }
    }
}