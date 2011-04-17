using System;

namespace IDEService.Core
{
    interface IAccessModule :IDisposable
    {
        int Login(string uname, string upasswd);

        bool Register(User user);

        Userinfo GetInfo(User u);

        LoginStatus CheckLogin(string login);

        void Logout(User user);
    }
}
