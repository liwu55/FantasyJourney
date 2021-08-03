using System.Collections.Generic;
using Game.bean;
using Game.Interface;

namespace Game
{
    public class TestDataBaseManager:IDataBaseManager
    {
        public void SaveInfo(UserInfo userinfo)
        {
        }

        public UserInfo GetUserInfo(string name, string psw)
        {
            if (name == "test" && psw == "test")
            {
                return new UserInfo();
            }
            else
            {
                return null;
            }
        }

        public List<UserInfo> GetRankList(int num)
        {
            return new List<UserInfo>();
        }
    }
}