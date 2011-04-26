using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PolicyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            PolicyServer Server = new PolicyServer();
            Server.Start();
            Console.ReadKey();
            Server.Stop();
        }
    }
}
