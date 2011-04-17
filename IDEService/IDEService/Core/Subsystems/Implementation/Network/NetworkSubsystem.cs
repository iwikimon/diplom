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
            Stop();
            return Start();
        }

        public override ServiceMessage SendMessage(ServiceMessage message)
        {
            switch ((NetworkMessages) message.Type)
            {
                case NetworkMessages.Decode:
                    return new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Network,
                                              SubsystemType.Network, NetworkMessages.Decode,
                                              new object[] {_module.Decode((byte[]) message.Message[0])});
                case NetworkMessages.Encode:
                    return new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Network,
                                              SubsystemType.Network, NetworkMessages.Decode,
                                              new object[] {_module.Encode((ServiceMessage) message.Message[0])});
                default:
                    throw new SubsystemWorkingException("Неопознанный тип сообщения.");
            }
            
        }
    }
}
