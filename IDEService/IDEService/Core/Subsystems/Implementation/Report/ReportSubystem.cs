using System;

namespace IDEService.Core
{
    class ReportSubystem :Subsystem
    {
        private ReportModule _module;
        public ReportSubystem() : base(SubsystemType.Report)
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
            throw new NotImplementedException();
        }
    }
}
