using System;
using System.Windows;
using System.Windows.Threading;
using IDEClient.Core;
using IDEService.Core;

namespace IDEClient
{
    public class AccessSubsystem :ISubsystem
    {
        public AccessControl AccControl { get; set; }
        public RegistrationControl RegControl { get; set; }


        public void Dispose()
        {
            
        }

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            return true;
        }

        public bool Reload()
        {
            
            Stop();
            return Start();
        }

        public ServiceMessage SendMessage(ServiceMessage message)
        {
            var type = (AccessMessages) message.Type;
            if (type == AccessMessages.Register || type == AccessMessages.CheckLogin)
                RegControl.SendMessage(message);
            if(type == AccessMessages.Login)
                AccControl.SendMessage(message);
            if (type == AccessMessages.Logout)
                AccControl.SendMessage(message);
            return null;
        }

        public SubsystemType Type()
        {
            return SubsystemType.Access;
        }

        public SubsystemState GetState()
        {
            throw new NotImplementedException();
        }

        public SubsystemInfo GetInfo()
        {
            throw new NotImplementedException();
        }
    }
}
