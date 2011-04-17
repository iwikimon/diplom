using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Xml;
using IDEService.Core;

namespace IDEService.service
{
    /// <summary>
    /// Сетевой уровень приложения. 
    /// Отвечает за прием/передачу сообщений 
    /// между клиентом и сервером.
    /// Представляет клиента ядру
    /// </summary>
    public class ClientWorker :IDisposable
    {
        private Socket _client;

        private Thread _work;

        private SocketAsyncEventArgs eventArgs;

        /// <summary>
        /// Буфер для приема-передачи сообщений
        /// </summary>
        private byte[] buffer;

        public ClientWorker(Socket client)
        {
            _client = client;
            
            _work = new Thread(Work);
            _work.Start();
        }

        public void Dispose()
        {
            _work.Abort();
        }

        private void eventArgsCompleted(object sender, SocketAsyncEventArgs e)
        {
            //разрыв соединения в связи с ошибкой сети
            switch (e.SocketError)
            {
                case SocketError.ConnectionReset:
                    ProcessDisconnect(e);
                    return;
                case SocketError.ConnectionAborted:
                    ProcessDisconnect(e);
                    return;
                case SocketError.ConnectionRefused:
                    ProcessDisconnect(e);
                    return;
            }
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Disconnect:
                    ProcessDisconnect(e);
                    break;
            }
        }

        private void ProcessDisconnect(SocketAsyncEventArgs e)
        {
            //TODO: реализация отключения клиента
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                if(e.BytesTransferred > 950*1024)
                {
                    throw new Exception("Длинное сообщение. Необходимо увеличить буфер приема/передачи сообщений");
                }
                var answer = Kernel.GetKernel.SendMessage(e.Buffer, _client.RemoteEndPoint.ToString());

                ProcessSend(e);
            }
            if(e.SocketError == SocketError.ConnectionReset)
                ProcessDisconnect(e);
        }

        private void ReceiveAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = _client.ReceiveAsync(e);
            if (!willRaiseEvent)
                ProcessReceive(e);
        }

        public void SendAsync(byte[] msg)
        {
            if (_client.Connected && msg != null)
            {
                if (msg.Length > 950 * 1024)
                {
                    throw new Exception("Длинное сообщение. Необходимо увеличить буфер приема/передачи сообщений");
                }
                var e = new SocketAsyncEventArgs();
                e.SetBuffer(msg, 0, msg.Length);
                e.Completed += eventArgsCompleted;
                SendAsync(e);
            }
        }

        private void SendAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = _client.SendAsync(e);
            if (!willRaiseEvent)
                ProcessSend(e);
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                ReceiveAsync(eventArgs);
            }
        }

        private void Work()
        {
            eventArgs = new SocketAsyncEventArgs();
            buffer = new byte[1024*1024];//1Mb
            eventArgs.SetBuffer(buffer,0,buffer.Length);
            eventArgs.Completed+=eventArgsCompleted;
            _client.ReceiveAsync(eventArgs);
        }
    }
}
