using System;

namespace IDEService.Core
{
    interface INetworkModule :IDisposable
    {
        ServiceMessage Decode(byte[] msg);

        byte[] Encode(ServiceMessage msg);
    }
}
