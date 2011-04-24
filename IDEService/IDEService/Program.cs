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
            var sw = new ServerWorker("192.168.1.34", 4505);
          
        }
    }
}