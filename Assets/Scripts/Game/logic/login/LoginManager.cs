using System;
using Game.bean;
using Game.Interface;
using Mono.Data.Sqlite;
using UnityEngine;


namespace Game
{
    /// <summary>
    /// 登录实现实例
    /// </summary>
    public class LoginManager:ILoginManager
    {
        private IDataBaseManager _dataBaseManager;
        public LoginManager()
        {
            _dataBaseManager = DataBaseManager.Instance;
        }
        public void Login(string name, string pwd, Action<LoginResult> callBack)
        {
            LoginResult result =new LoginResult();
            //查数据库
            UserInfo userInfo = _dataBaseManager.GetUserInfo(name, pwd);
            
            //查到了
            bool check = userInfo!=null;
            //且状态为未登录
            if(check){
                result.suc = true;
                result.userInfo = userInfo;
                callBack(result);
            }else{
                //没查到 或者账号已登录
                result.suc = false;
                callBack(result);
            }
        }

    }
}