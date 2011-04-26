﻿using System;
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

        public string IP;
        private SocketAsyncEventArgs eventArgs;

        /// <summary>
        /// Буфер для приема-передачи сообщений
        /// </summary>
        private byte[] buffer;

        public ClientWorker(Socket client)
        {
            _client = client;
            eventArgs = new SocketAsyncEventArgs();
            buffer = new byte[1024 * 1024];//1Mb
            eventArgs.SetBuffer(buffer, 0, buffer.Length);
            eventArgs.Completed += eventArgsCompleted;
            _client.ReceiveAsync(eventArgs);
            IP = client.RemoteEndPoint.ToString();

        }

        public void Dispose()
        {

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
            
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                if(e.BytesTransferred > 950*1024)
                {
                    throw new Exception("Длинное сообщение. Необходимо увеличить буфер приема/передачи сообщений");
                }
                if (e.BytesTransferred > 0)
                {
                    var data = new byte[e.BytesTransferred];
                    Array.Copy(e.Buffer, data, e.BytesTransferred);
                    var answer = Kernel.GetKernel.SendMessage(data, _client.RemoteEndPoint.ToString());
                    if (answer != null)
                        SendAsync(answer);
                }
            }
        }

        private void ReceiveAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = _client.ReceiveAsync(e);
            if (!willRaiseEvent)
                ProcessReceive(e);
        }

        public void SendAsync(byte[] msg)
        {
            if (msg != null)
            {
                if (msg.Length > 950 * 1024)
                {
                    throw new Exception("Длинное сообщение. Необходимо увеличить буфер приема/передачи сообщений");
                }
                var e = new SocketAsyncEventArgs();
                e.Completed += eventArgsCompleted;
                string data = "";
                foreach (var b in msg)
                {
                    data += b.ToString() + " ";
                }
                Logger.Log.Write(LogLevels.Debug,"sended: "+msg.Length +" bytes: \t\t"+data);
                e.SetBuffer(msg, 0, msg.Length);
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

        public bool IsConnected()
        {
            return _client.Connected;
        }
    }
}
