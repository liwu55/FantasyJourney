using System.Collections.Generic;

namespace Game.Interface
{
    public interface IDataBaseManager
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="userinfo"></param>
        void SaveInfo(UserInfo userinfo);

        /// <summary>
        /// 获取用户数据，登录使用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="psw"></param>
        /// <returns>用户名密码正确，则返回用户信息，错误返回null</returns>
        UserInfo GetUserInfo(string name,string psw);

        /// <summary>
        /// 获取排行榜信息
        /// </summary>
        /// <returns></returns>
        List<UserInfo> GetRankList();
    }
}