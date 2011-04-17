using System;

namespace IDEService.Core
{
    class VcsSubsystem :Subsystem
    {
        private VcsModule _module;
        public VcsSubsystem() : base(SubsystemType.Vcs)
        {
        }

        public override bool Start()
        {
            _module = new VcsModule(this);
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
