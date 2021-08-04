using System;
using System.Collections.Generic;
using System.Text;
using Game.bean;
using Game.Interface;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Game
{
    public class TestDataBaseManager: SingleTonObj<TestDataBaseManager>,IDataBaseManager
    {
        private string _connectionStr;
        private SqliteConnection _connection;
        private SqliteCommand _command;
        //private SqliteDataReader _reader;
        private int limitRankList = 3;
        Dictionary<string ,UserInfo> _dictionary= new Dictionary<string,UserInfo>();
        
        public void SaveInfo(UserInfo userinfo)
        {
            int userId = userinfo.id;
            string username = userinfo.username;
            string password = userinfo.password;
            _command.CommandText = "INSERT INTO CustomerInfo VALUES (" +"'"+userId+ "'"+","+ "'"+ username+ "'"+ ","+ "'"+ password+ "'" +")";
            int i =  _command.ExecuteNonQuery();
            Debug.Log("这次操作共对数据库的"+i+"条数据产生了影响");
        }
        public TestDataBaseManager()
        {
            InitDataBase();
            GetRankList(3);
        }
        //初始化数据库
        private void InitDataBase()
        {
            _connectionStr = "Data Source = " + Application.streamingAssetsPath + "/DBCustomerInfo.db";
            _connection = new SqliteConnection(_connectionStr);
            _command = _connection.CreateCommand();
            if (_connection!=null)
            {
                Debug.Log("可以找到数据库");
                Debug.Log(_connectionStr);
                //打开连接
                _connection.Open();
            }
            _command.CommandText = "Select * From CustomerInfo";
            SqliteDataReader _reader = _command.ExecuteReader();
            //将数据传入字典
            while (_reader.Read())
            {
                object id =  _reader.GetValue(0);
                object username = _reader.GetValue(1);
                object password = _reader.GetValue(2);
                object money = _reader.GetValue(3);
                object honor = _reader.GetValue(4);
                object ownedHero = _reader.GetValue(5);
                _dictionary.Add((string)username,new UserInfo(Convert.ToInt32(id), (string) username, (string) password, Convert.ToInt32(money),Convert.ToInt32(honor),(string)ownedHero));
            }
            _reader.Close();
        }
        public UserInfo GetUserInfo(string name, string pwd)
        {
            _command.CommandText = "Select * From CustomerInfo where username = " +"'"+name+"'";
            if (_command.ExecuteScalar()==null)
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
                if (_user!=null)
                {
                    bool pwdIsSame = _user.password == pwd ? true : false;
                    if (pwdIsSame)
                    {
                        //TODO UIEVENT
                        Debug.Log("name="+ name);
                        UIManager.Instance.ShowModule("LoginSuc",name);
                        Debug.Log("登陆成功");
                        return _user;
                    }
                    else
                    {
                        //TODO UIEVENT
                        Debug.Log("账户名或密码错误");
                        return null;
                    }
                }
              
            }
            return null;
        }
        public List<UserInfo> GetRankList(int num)
        {
            List<UserInfo> rankList = new List<UserInfo>();
            //排序的SQL语句
            _command.CommandText = "Select * from CustomerInfo ORDER BY honor DESC , money DESC limit "+num;
            SqliteDataReader _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                UserInfo _userInfo;
                _dictionary.TryGetValue((string)_reader.GetValue(1), out _userInfo);
                rankList.Add(_userInfo);
            }
            for (int i = 0; i < rankList.Count; i++)
            {
                Debug.Log(rankList[i].username);
            }
            return rankList;
        }
    }
}