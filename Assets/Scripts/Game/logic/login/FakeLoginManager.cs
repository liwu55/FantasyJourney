using System;
using Game.bean;
using Game.Interface;

namespace Game
{
    public class FakeLoginManager:ILoginManager
    {
        public void Login(string name, string pwd, Action<LoginResult> loginResult)
        {
            LoginResult result = new LoginResult();
            result.userInfo = GetFakeUser(name,pwd);
            result.suc = true;
            loginResult(result);
        }

        private UserInfo GetFakeUser(string name, string pwd)
        {
            return new UserInfo(1,name,pwd,0,2048,"0,1,2",0);
        }
    }
}