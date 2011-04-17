using System;

namespace IDEService.Core
{
    class WordProcessorSubsystem : Subsystem
    {
        private TextModule _module;
        public WordProcessorSubsystem() : base(SubsystemType.WordProcessor)
        {
        }

        public override bool Start()
        {
            _module = new TextModule(this);
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
