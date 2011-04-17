using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace IDEService.service
{
    class ServerWorker :IDisposable
    {
        private string _host { get; set; }

        private int port { get; set; }

        private TcpListener listener;

        private Thread work { get; set; }

        List<ClientWorker> clients = new List<ClientWorker>();
        
        public ServerWorker(string host, int port)
        {
            listener = new TcpListener(IPAddress.Parse(host), port);
            listener.Start();
            work = new Thread(Work);
            work.Start();
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
        }
    }
}
