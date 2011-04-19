using System;

namespace IDEService.Core
{
    public class AccessSubsystem : Subsystem
    {
        private IAccessModule _acessModule;

        public AccessSubsystem() : base(SubsystemType.Access) { }

        public override bool Start()
        {
            _acessModule = BootLoader.LoadSubsystemModule<AccessModule>(this);
            return true;
        }

        public override bool Stop()
        {
            Info.State = SubsystemState.Stopped;
            _acessModule.Dispose();
            return true;
        }

        public override bool Reload()
        {
            this.Stop();
            Kernel.GetKernel.UnRegisterSubsystem(this);
            BootLoader.LoadSubsystem(SubsystemType.Access);
            return true;
        }

        public override ServiceMessage SendMessage(ServiceMessage message)
        {
            var msgType = AccessMessages.Undefined;
            try
            {
                msgType = (AccessMessages)Convert.ChangeType(message.Type, typeof(AccessMessages));
            }
            catch (Exception)
            {
                Logger.Log.Write(LogLevels.Emerg,
                                 String.Format("Неправильный тип сообщения: {0} для подсистемы {1}",
                                                message.Type, this.Type()));
            }
            switch (msgType)
            {
                case AccessMessages.CheckLogin:
                    return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Access, SubsystemType.Access, AccessMessages.CheckLogin,
                                              new object[] { _acessModule.CheckLogin((string)message.Message[0]).ToString() });
                case AccessMessages.GetInfo:
                    {
                        throw new NotImplementedException();
                    }
                case AccessMessages.Login:
                    {
                        var login = (string)message.Message[0];
                        var passwd = (string)message.Message[1];
                        object result = _acessModule.Login(login, passwd);
                        return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Access, SubsystemType.Access, AccessMessages.Login,
                                                    new object[] { result });
                    }
                case AccessMessages.Register:
                    {
                        var u = new User
                                    {
                                        
                                        Login = message.Message[3].ToString(),
                                        Password = message.Message[4].ToString(),
                                        Email = message.Message[0].ToString(),
                                        Name = message.Message[1].ToString(),
                                        Sname = message.Message[2].ToString(),
                                        LastAccess = DateTime.Now,
                                        Registred = DateTime.Now,
                                    };

                        return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Access, SubsystemType.Access, AccessMessages.Register,
                                                    new object[] {_acessModule.Register(u)});
                    }
                case AccessMessages.Logout:
                    {
                        _acessModule.Logout(((UserCache)(message.Message[0])).Client);
                        return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Access, message.From, AccessMessages.Logout,
                            new object[] {  });
                    }
                default:
                    throw new SubsystemWorkingException("Неопознанный тип сообщения.");
            }
        }
    }
}