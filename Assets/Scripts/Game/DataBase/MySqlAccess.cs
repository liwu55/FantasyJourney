using System;
using MySql.Data.MySqlClient;
using UnityEngine;


public class MySqlAccess
{
    //连接类对象
    public  MySqlConnection mySqlConnection;
    //IP地址
    private static string host;
    //端口号
    private static string port;
    //用户名
    private static string username;
    //密码
    private static string password;
    //数据库名称
    private static string databaseName;
    public MySqlAccess(string _host,string _port, string _username, string _password, string _databaseName)
    {
        host = _host;
        port = _port;
        username = _username;
        password = _password;
        databaseName = _databaseName;
        OpenSql();
    }
    private void OpenSql()
    {
        try {
            string mySqlString = string.Format("datasource={0};port={1};database={2};user={3};pwd={4};"
                , host, port, databaseName, username, password);
            Debug.Log(mySqlString);
            mySqlConnection = new MySqlConnection(mySqlString);
            mySqlConnection.Open();
        } catch (Exception e) {
            throw new Exception("服务器连接失败，请重新检查MySql服务是否打开。" + e.Message);
        }
    }
}
