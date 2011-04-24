using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using IDEService.Core;

namespace IDEService.service
{
    class ServerWorker :IDisposable
    {
        private string _host { get; set; }

        private int port { get; set; }

        private TcpListener listener;

        private Thread work { get; set; }

        public List<ClientWorker> clients = new List<ClientWorker>();
        private Thread Watcher;
        public ServerWorker(string host, int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            work = new Thread(Work);
            work.Start();
            Watcher = new Thread(ClientWatcher);
            Watcher.Start();
        }

        private void ClientWatcher()
        {
            while (true)
            {
                for (int i = 0; i < clients.Count; ++i)
                    if (!clients[i].IsConnected())
                    {
                        Kernel.GetKernel.DisconnectUser(clients[i].IP);
                        clients.Remove(clients[i]);
                        --i;
                    }
                Thread.Sleep(1000);
            }
        }

        private void Work()
        {
            while (true)
            {
                var client = listener.AcceptSocket();
                clients.Add(new ClientWorker(client));
                Console.WriteLine("Accepted new client");
            }
        }

        public void Dispose()
        {
            clients.ForEach(x =>x.Dispose());
            work.Abort();
            Watcher.Abort();
        }
    }
}
