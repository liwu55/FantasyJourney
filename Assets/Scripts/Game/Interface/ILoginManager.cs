using System;

namespace Game.Interface
{
    public interface ILoginManager
    {
        public void Login(string name, string pwd, Action<LoginResult> loginResult);
    }
}