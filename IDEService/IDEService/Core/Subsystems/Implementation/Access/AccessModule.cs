using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mindscape.LightSpeed;

namespace IDEService.Core
{
    public class AccessModule : IAccessModule
    {
        /// <summary>
        /// Подсистема, в которой работает модуль
        /// </summary>
        private AccessSubsystem _accessSubsystem;

        /// <summary>
        /// Время входа пользователя в систему
        /// </summary>
        private DateTime _loginTime;

        private DBModelUnitOfWork _context;

        public AccessModule(ISubsystem accessSubsystem)
        {
            if (accessSubsystem.Type() != SubsystemType.Access)
                throw new ModuleLoadException("Неправильный тип подсистемы");
            _accessSubsystem = (AccessSubsystem)accessSubsystem;
            var result = Kernel.GetKernel.
                SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Access, SubsystemType.DataBase,
                                               DbSubsystemMessages.GetContext, new object[] {}));
            _context = (DBModelUnitOfWork)result.Message[0];
        }

        public int Login(string uname, string upasswd)
        {
            try
            {
                //TODO: тестирование.
                var user = _context.Users.Where(u => (u. Login == uname) && (u.Password == upasswd)).First();
                user.LastAccess = DateTime.Now;
                _loginTime = DateTime.Now;
                user.Userlogs.Add(new Userlog(){ Date = DateTime.Now, Message = "Вход в систему"});
                var cache = new UserCache(user);
                Kernel.GetKernel.Clients.Add(cache.GetHashCode(), cache);
                return cache.GetHashCode();
            }
            catch(Exception)
            {
                return 0;
            }
        }

        public bool Register(User user)
        {
            if(CheckLogin(user.Login) == LoginStatus.Busy)
                return false;
            var newuser = new User()
                              {
                                  Login = user.Login,
                                  Password = user.Password,
                                  Email = user.Email,
                                  Name = user.Name,
                                  Sname = user.Sname,
                                  LastAccess = DateTime.Now,
                                  Registred = DateTime.Now,
                              };
            _context.Add(newuser);
            var prodjectsDir = Configure.Cfg.Read<string>(SubsystemType.Project.ToString(), "ProdjectDirectory", "Path");
            var userDir = prodjectsDir + "\\" + newuser.Login;
            try
            {
                Directory.CreateDirectory(userDir);
            }
            catch(Exception)
            {
                Logger.Log.Write(LogLevels.Emerg, "Неудалось создать дирекотрию пользователя."+userDir);
                throw new AccessFolderExcepton("Неудалось создать дирекотрию пользователя."+userDir);
            }
            newuser.Userlogs.Add(new Userlog(){Date = DateTime.Now, Message = "Регистрация"});
            Kernel.GetKernel.
                SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Access, SubsystemType.DataBase,
                                               DbSubsystemMessages.SaveContext, new object[] {}));
            return true;
        }

        public void Logout(User user)
        {
            user.Userlogs.Add(new Userlog(){Date = DateTime.Now, Message = "Выход из системы"});
            Kernel.GetKernel.
               SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Access, SubsystemType.DataBase,
                                              DbSubsystemMessages.SaveContext, new object[] { }));
        }

        public LoginStatus CheckLogin(string login)
        {
            if (_context.Users.Where(user => user.Login == login).Count() > 0)
                return LoginStatus.Busy;
            return LoginStatus.Free;
        }

        public void Dispose()
        {
            
        }
    }
}
