using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace PolicyServer
{
    class PolicyServer
    {
        public static readonly string PolicyFileName = "clientaccesspolicy.xml";

        private Socket Sock;
        private SocketAsyncEventArgs AcceptAsyncArgs;

        public PolicyServer()
        {
            using (FileStream fs = new FileStream(PolicyFileName, FileMode.Open))
            {
                byte[] PolicyBuffer = new byte[fs.Length];
                fs.Read(PolicyBuffer, 0, (int)fs.Length);
                ClientConnect.SetPolicyResponse(PolicyBuffer);
            }

            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            AcceptAsyncArgs = new SocketAsyncEventArgs();
            AcceptAsyncArgs.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptAsync_Completed);
        }

        public void Start()
        {
            Sock.Bind(new IPEndPoint(IPAddress.Any, 943));
            Sock.Listen(50);
            AcceptAsync(AcceptAsyncArgs);
        }

        private void AcceptAsync(SocketAsyncEventArgs AcceptAsyncArgs)
        {
            bool willRaiseEvent = Sock.AcceptAsync(AcceptAsyncArgs);
            if (!willRaiseEvent)
                AcceptAsync_Completed(Sock, AcceptAsyncArgs);
        }

        private void AcceptAsync_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                ClientConnect Client = new ClientConnect(e.AcceptSocket);
                Client.Start();
            }
            e.AcceptSocket = null;
            AcceptAsync(AcceptAsyncArgs);
        }

        public void Stop()
        {
            Sock.Close();
        }

    }
}
