using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace PolicyServer
{
    class ClientConnect
    {
        private static byte[] PolicyResponse;
        private static readonly string PolicyRequest = "<policy-file-request/>";

        private byte[] buff;
        private Socket Sock;
        private SocketAsyncEventArgs SockEventArgs;

        public ClientConnect(Socket sock)
        {
            Sock = sock;
            SockEventArgs = new SocketAsyncEventArgs();
            buff = new byte[512];
            SockEventArgs.SetBuffer(buff, 0, buff.Length);
            SockEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SockEventArgs_Completed);
        }

        void SockEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            { 
                case SocketAsyncOperation.Receive:
                    ReceiveProcess(e);
                    break;
                case SocketAsyncOperation.Send:
                    SendProcess(e);
                    break;
            }
        }


        private void SendPolicy()
        {
            SockEventArgs.SetBuffer(PolicyResponse, 0, PolicyResponse.Length);
            SendAsync(SockEventArgs);

        }
        private void SendAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.SendAsync(e);
            if (!willRaiseEvent)
                SendProcess(e);
        }
        private void SendProcess(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred < e.Count)
            {
                e.SetBuffer(e.BytesTransferred, e.Count - e.BytesTransferred);
                SendAsync(e);
            }
            else
            {
                Sock.Close();
            }
        }

        public void Start()
        {
            ReceiveAsync(SockEventArgs);
        }
        private void ReceiveAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.ReceiveAsync(e);
            if (!willRaiseEvent)
                ReceiveProcess(e);
        }
        private void ReceiveProcess(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                string str = Encoding.UTF8.GetString(e.Buffer);
                if (string.Compare(PolicyRequest, str) == 0)
                {
                    SendPolicy(); 
                }
            }
        }

        public static void SetPolicyResponse(byte[] buff)
        {
            PolicyResponse = buff;
        }

    }
}
