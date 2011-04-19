using System;

namespace IDEService.Core
{
    class ReportSubystem : Subsystem
    {
        private ReportModule _module;
        public ReportSubystem()
            : base(SubsystemType.Report)
        {
        }

        public override bool Start()
        {
            _module = new ReportModule(this);
            return true;
        }

        public override bool Stop()
        {
            _module.Dispose();
            return true;
        }

        public override bool Reload()
        {
            throw new NotImplementedException();
        }

        public override ServiceMessage SendMessage(ServiceMessage message)
        {
            switch ((ReportMessages)message.Type)
            {
                case ReportMessages.GetLastUserLog:
                    return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Report, SubsystemType.Report,
                                              ReportMessages.GetLastUserLog, new object[]
                                                                             {
                                                                                 _module.GetUserLogs(
                                                                                     ((UserCache) message.Message[0]).
                                                                                         Client)
                                                                             });
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
