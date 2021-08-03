using System;
using Game.bean;

namespace Game.Interface
{
    public interface ILoginManager
    {
        public void Login(string name, string pwd, Action<LoginResult> loginResult);
    }
}