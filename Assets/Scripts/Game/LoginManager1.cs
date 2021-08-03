using System;
using Game.Interface;

namespace Game
{
    /// <summary>
    /// 登录实现实例
    /// </summary>
    public class LoginManager1:ILoginManager
    {
        public void Login(string name, string pwd, Action<LoginResult> callBack)
        {
            LoginResult result =new LoginResult();
            //查数据库
            //...
            //查到了
            bool check = true;
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