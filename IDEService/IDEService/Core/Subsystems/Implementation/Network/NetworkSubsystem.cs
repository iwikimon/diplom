using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEService.Core
{
    class NetworkSubsystem :Subsystem
    {
        private NetworkModule _module;
        public NetworkSubsystem() : base(SubsystemType.Network)
        {
        }

        public override bool Start()
        {
            _module = new NetworkModule(this);
           // throw new NotImplementedException();
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
