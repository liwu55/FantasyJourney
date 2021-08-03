using System;
using Game.bean;
using Game.Interface;

namespace Game
{
    /// <summary>
    /// 登录实现实例
    /// </summary>
    public class TestLoginManager:ILoginManager
    {
        private IDataBaseManager _dataBaseManager;

        public TestLoginManager()
        {
            _dataBaseManager=new TestDataBaseManager();
        }
        public void Login(string name, string pwd, Action<LoginResult> callBack)
        {
            LoginResult result =new LoginResult();
            //查数据库
            //...
            //查到了
            bool check = _dataBaseManager.GetUserInfo(name,pwd)!=null;
            if(check){
                result.suc = true;
                //result.userInfo =
                callBack(result);
            }else{
                //没查到
                result.suc = false;
                callBack(result);
            }
            
        }
    }
}