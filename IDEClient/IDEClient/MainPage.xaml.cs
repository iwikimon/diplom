using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using IDEService.Core;

namespace IDEClient
{
    public partial class MainPage : UserControl
    {

        public delegate void GetResultCallback(string text);

        private Socket Sock;
        private SocketAsyncEventArgs SockAsyncArgs;
        private byte[] buff;

        public MainPage()
        {
            //Kernel.GetKernel.ToString();
            InitializeComponent();
            //this.stackPanel1.Children.Add(new AccessControl());
            buff = new byte[1024];
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SockAsyncArgs = new SocketAsyncEventArgs();
            SockAsyncArgs.Completed += ConnectCompleted;
        }

        // A single callback is used for all socket operations. 
        // This method forwards execution on to the correct handler 
        // based on the type of completed operation
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

        public void ConnectAsync(string Address, int Port)
        {
            SockAsyncArgs.RemoteEndPoint = new DnsEndPoint(Address, Port);
            ConnectAsync(SockAsyncArgs);
        }
        private void ConnectAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.ConnectAsync(e);
            if (!willRaiseEvent)
                ProcessConnect(e);
        }

        private void ProcessConnect(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                buff = new byte[1024*1024];//1Mb
                SockAsyncArgs.SetBuffer(buff, 0, buff.Length);
            }
        }

        public void SendAsync(ServiceMessage msg)
        {
            if (Sock.Connected && msg != null)
            {
                byte[] buff = SerializeData(msg);
                var e = new SocketAsyncEventArgs();
                e.SetBuffer(buff, 0, buff.Length);
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
                string str = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
                var msg = DeserializeData(str);
                WriteResult(str);
            }
        }

        private void ReceiveAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.ReceiveAsync(e);
            if (!willRaiseEvent)
                ProcessReceive(e);
        }

        private void WriteResult(string result)
        {
            if (this.Dispatcher.CheckAccess())
                MessageBox.Show(result);
            else
            {
                this.Dispatcher.BeginInvoke(new GetResultCallback(WriteResult), result);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SendAsync(new ServiceMessage(KernelTypes.ServiceKernel,SubsystemType.Access,SubsystemType.Access,AccessMessages.CheckLogin,new object[]{"Vasya"}));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ConnectAsync("192.168.1.34", 4532);
        }


        ServiceMessage DeserializeData(string data)
        {
            var reader = XmlDictionaryReader.CreateTextReader(Encoding.UTF8.GetBytes(data),
                                                              XmlDictionaryReaderQuotas.Max);
            var deserializer = new DataContractSerializer(typeof(ServiceMessage));
            return (ServiceMessage)deserializer.ReadObject(reader, true);
        }

        byte[] SerializeData(ServiceMessage msg)
        {
            var serializer = new DataContractSerializer(typeof(ServiceMessage));
            var writer = new MemoryStream();
            serializer.WriteObject(writer, msg);
            return writer.ToArray();
        }
    }
}
