using System;
using System.Collections.Generic;
using System.Text;
using Game.bean;
using Game.Interface;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Game
{
    public class DataBaseManager : SingleTonObj<DataBaseManager>, IDataBaseManager
    {
        private string _connectionStr;
        private SqliteConnection _connection;

        private SqliteCommand _command;

        //private SqliteDataReader _reader;
        //private int limitRankList = 3;
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
            /*string sql = @"Insert INTO CustomerInfo VALUES({0},'{1}','{2}',{3},{4},'{5}')";
            sql = String.Format(sql,userId.ToString(),username,password,money.ToString(),honor.ToString(),heroList);*/
            //Debug.Log("sql= "+sql);
        }

        /// <summary>
        /// 防止SQL注入 使用参数代替字符串更新信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="money"></param>
        /// <param name="honor"></param>
        /// <param name="ownedHero"></param>
        private void  UpdateUserInfo(int @ID,string @username,string @password,int @money,int @honor,string @ownedHero)
        {
            
           // string sql = @"Insert INTO CustomerInfo VALUES({0},'{1}','{2}',{3},{4},'{5}')";
            _command.Parameters.Add(new SqliteParameter("@ID", @ID));
            _command.Parameters.Add(new SqliteParameter("@username", @username));
            _command.Parameters.Add(new SqliteParameter("@password", @password));
            _command.Parameters.Add(new SqliteParameter("@money", @money));
            _command.Parameters.Add(new SqliteParameter("@honor", @honor));
            _command.Parameters.Add(new SqliteParameter("@ownedHero", @ownedHero));
            _command.CommandText = "Insert INTO CustomerInfo VALUES(@ID,@username,@password, @money,@honor, @ownedHero)";
            int i = _command.ExecuteNonQuery();
            Debug.Log("产生影响"+ i);
        }  
        public DataBaseManager()
        {
            InitDataBase();
        }

        //初始化数据库
        private void InitDataBase()
        {
            _connectionStr = "Data Source = " + Application.streamingAssetsPath + "/DBCustomerInfo.db";
            _connection = new SqliteConnection(_connectionStr);
            _command = _connection.CreateCommand();
            if (_connection != null)
            {
                Debug.Log("可以找到数据库");
                //打开连接
                _connection.Open();
            }
            SaveInDic();
        }

        private void SaveInDic()
        {
            _command.CommandText = "Select * From CustomerInfo";
            SqliteDataReader _reader = _command.ExecuteReader();
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
            SqliteDataReader _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                UserInfo _userInfo;
                _dictionary.TryGetValue((string) _reader.GetValue(1), out _userInfo);
                rankList.Add(_userInfo);
            }
            return rankList;
        }
    }
}