using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using IDEService.Core;
using IDEService.service;
using Mindscape.LightSpeed;

namespace IDEService
{
    

    class Program
    {
        public static void Main(String[] args)
        {
            BootLoader.Init();
           
            ServerWorker sw = new ServerWorker("192.168.1.34", 4532);
            bool notexit = true;
         sw.Dispose();
            //while (notexit)
            //{
            //    var line = Console.Re
            //    switch(line)
            //    {
            //        case "exit":
            //            notexit = false; break;
            //        case "info":
            //            {
            //                foreach(var subsystem  in Kernel.GetKernel.Subsystems)
            //                    Console.WriteLine(subsystem.GetInfo().ToString());
            //                break;
            //            }
            //        default:
            //            Console.WriteLine("exit - Выход, info - информация о подсистемах.");
            //            break;
            //    }
            //}
        }
    }
}