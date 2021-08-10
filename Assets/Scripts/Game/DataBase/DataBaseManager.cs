using System;
using System.Collections.Generic;
using Game.bean;
using Game.Interface;
using MySql.Data.MySqlClient;
using UnityEngine;
namespace Game
{
    public class DataBaseManager : SingleTonMonoAuto<DataBaseManager>, IDataBaseManager
    {
        private MySqlAccess mySqlAccess;
        private string _connectionStr;
        private MySqlConnection _connection;
        private MySqlCommand _command;
        Dictionary<string, UserInfo> _dictionary = new Dictionary<string, UserInfo>();

        /// <summary>
        /// 保存UserInfo数据
        /// </summary>
        /// <param name="userinfo"></param>
        public void SaveInfo(UserInfo userinfo)
        {
            int userId = userinfo.id;
            string username = userinfo.username;
            string password = userinfo.password;
            int money = userinfo.money;
            int honor = userinfo.honor;
            string heroList = userinfo.ownedHero;
            if ( !_dictionary.ContainsKey(userinfo.username))
            {
                //把用户数据存入字典
                _dictionary.Add(userinfo.username, userinfo);
            }
            
            UpdateUserInfo(userId, username, password, money, honor, heroList);
        }

        /// <summary>
        /// Insert一个用户（此方法有bug，暂时不要调用）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="money"></param>
        /// <param name="honor"></param>
        /// <param name="ownedHero"></param>
        private void  UpdateUserInfo(int @ID,string @username,string @password,int @money,int @honor,string @ownedHero)
        {
            _command.Parameters.Add(new MySqlParameter("@ID", @ID));
            _command.Parameters.Add(new MySqlParameter("@username", @username));
            _command.Parameters.Add(new MySqlParameter("@password", @password));
            _command.Parameters.Add(new MySqlParameter("@money", @money));
            _command.Parameters.Add(new MySqlParameter("@honor", @honor));
            _command.Parameters.Add(new MySqlParameter("@ownedHero", @ownedHero));
            _command.CommandText = "Insert INTO CustomerInfo VALUES(@ID,@username,@password, @money,@honor, @ownedHero)";
            int i = _command.ExecuteNonQuery();
            Debug.Log("产生影响"+ i);
        }
        private DataBaseManager()
        {
            Debug.Log("DataBaseManager实例化");
            InitDataBase();
        }

        //初始化数据库
        private void InitDataBase()
        {
            mySqlAccess = new MySqlAccess("10.9.72.192","3306","root","123456","dbcustomerinfo");
            _connection = mySqlAccess.mySqlConnection;
            _command = _connection.CreateCommand();

            SaveInDic();
        }

        private void SaveInDic()
        {
            _command.CommandText = "Select * From CustomerInfo";
            MySqlDataReader _reader = _command.ExecuteReader();
            //将数据传入字典
            while (_reader.Read())
            {
                object id = _reader.GetValue(0);
                object username = _reader.GetValue(1);
                object password = _reader.GetValue(2);
                object money = _reader.GetValue(3);
                object honor = _reader.GetValue(4);
                object ownedHero = _reader.GetValue(5);
                if (!_dictionary.ContainsKey((string) username))
                {
                    _dictionary.Add((string) username,
                        new UserInfo(Convert.ToInt32(id), (string) username, (string) password, Convert.ToInt32(money),
                            Convert.ToInt32(honor), (string) ownedHero));
                }
            }
            _reader.Close();
        }
        /// <summary>
        /// 通过用户名获取UserInfo
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(string name, string pwd)
        {
            _command.CommandText = "Select * From CustomerInfo where username = " + "'" + name + "'";
            if (_command.ExecuteScalar() == null)
            {
                Debug.Log("用户名不存在");
                //用户名不存在
                return null;
            }
            else //用户名存在
            {
                //TODO sql防注入
                //判断是否与密码匹配
                UserInfo _user;
                _dictionary.TryGetValue(name, out _user);
                if (_user != null)
                {
                    bool pwdIsSame = _user.password == pwd ? true : false;
                    if (pwdIsSame)
                    {
                        Debug.Log("登陆成功");
                        return _user;
                    }
                    else
                    {
                        Debug.Log("账户名或密码错误");
                        return null;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取排行榜
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<UserInfo> GetRankList(int num)
        {
            if (num > _dictionary.Count)
            { 
                Debug.LogError("查询范围超出！");
                return null;
            }
            List<UserInfo> rankList = new List<UserInfo>();
            //排序的SQL语句
            _command.CommandText = "Select * from CustomerInfo ORDER BY honor DESC , money DESC limit " + num;
            MySqlDataReader _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                UserInfo _userInfo;
                _dictionary.TryGetValue((string) _reader.GetValue(1), out _userInfo);
                rankList.Add(_userInfo);
            }
            _reader.Close();
            return rankList;
        }

        private void OnApplicationQuit()
        {
            Debug.Log("关闭数据库连接");
            if (mySqlAccess.mySqlConnection!=null)
            {
                mySqlAccess.mySqlConnection.Close();
            }
        }
    }
}