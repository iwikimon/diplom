using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IDEClient.Core;
using IDEService.Core;

namespace IDEClient
{
    public class ClientWorker
    {
        private string _ip { get; set; }

        private int _port { get; set; }

        private Socket Sock;
        private SocketAsyncEventArgs SockAsyncArgs;
        private byte[] buff;

        public ClientWorker(string ip, int port)
        {
            _ip = ip;
            _port = port;
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            SockAsyncArgs = new SocketAsyncEventArgs();
            //SockAsyncArgs.SocketClientAccessPolicyProtocol = SocketClientAccessPolicyProtocol.Http;
          
            SockAsyncArgs.Completed += ConnectCompleted;
        }

        public void ConnectAsync()
        {
                SockAsyncArgs.RemoteEndPoint = new DnsEndPoint(_ip, _port);
                ConnectAsync(SockAsyncArgs);
        }

        private void ConnectAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.ConnectAsync(e);
            if (!willRaiseEvent)
                ProcessConnect(e);
        }

        void ConnectCompleted(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e);
                    break;
               case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
            }
        }



        private void ProcessConnect(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                buff = new byte[1024 * 1024];//1Mb
                SockAsyncArgs.SetBuffer(buff, 0, buff.Length);
            }
        }

        public void SendAsync(byte[] data)
        {
           // if(!Sock.Connected)
            //{
            //    MessageBox.Show("Нет соединения с сервером. Будет произведена попытка переподключиться. ");
                
            //}
            //if (Sock.Connected && data != null)
            {
                var e = new SocketAsyncEventArgs();
               // e.SocketClientAccessPolicyProtocol = SocketClientAccessPolicyProtocol.Http;
                e.SetBuffer(data, 0, data.Length);
                e.Completed += ConnectCompleted;
                SendAsync(e);
            }
        }

        private void SendAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.SendAsync(e);
            if (!willRaiseEvent)
                ProcessSend(e);
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                ReceiveAsync(SockAsyncArgs);
            }
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                var data = new byte[e.BytesTransferred];
                Array.Copy(e.Buffer, data, e.BytesTransferred);
                Kernel.GetKernel.SendMessage(data);
            }

        }

        private void ReceiveAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.ReceiveAsync(e);
            if(!willRaiseEvent)
                ProcessReceive(e);
        }
        
    }
}
