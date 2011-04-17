using System;
using IDEService.Core;
using IDEService.service;

namespace IDEService
{
    class Program
    {
        public static void Main(String[] args)
        {
            BootLoader.Init();
            var sw = new ServerWorker("127.0.0.1", 4532);
        }
    }
}