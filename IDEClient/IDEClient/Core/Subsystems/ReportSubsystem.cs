using System;
using IDEClient.Core.Controls;
using IDEService.Core;

namespace IDEClient.Core.Subsystems
{
    public class ReportSubsystem :ISubsystem
    {
        public LogControl logControl;

        public void Dispose()
        {
            return;
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
            var msgType = (ReportMessages) message.Type;
            switch(msgType)
            {
                case ReportMessages.GetLastUserLog:
                    logControl.SendMessage(message);
                    return null;
            }
            return null;
        }

        public SubsystemType Type()
        {
            return SubsystemType.Report;
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
