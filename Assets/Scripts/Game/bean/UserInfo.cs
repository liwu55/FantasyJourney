namespace Game.bean
{
    /// <summary>
    /// 用户信息，需要存储在数据库的信息
    /// </summary>
    public class UserInfo
    {
        
        public int id;
        public string username;
        public string password;
        //金币数
        public int money;
        //奖杯数
        public int honor;
        //已经拥有的英雄
        public string ownedHero;


        public UserInfo(int id, string username, string password, int money, int honor, string ownedHero)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.money = money;
            this.honor = honor;
            this.ownedHero = ownedHero;
        }
    }
}