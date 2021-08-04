using Game.bean;
using Game.Interface;
using UnityEngine;

namespace Game
{
    public class TestUIController:IUIController
    {
        public void ShowLogin()
        {
            UIManager.Instance.ShowModule("Login");
        }

        public void ShowMain(MainPageInfo mainPageInfo)
        {
            Debug.Log("显示主页面");
        }

        public void ShowStore()
        {
            UIManager.Instance.ShowModule("Store");
        }

        public void ShowLoginError()
        {
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