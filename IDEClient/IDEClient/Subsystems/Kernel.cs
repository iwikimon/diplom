using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using IDEClient.ServiceReference1;

namespace IDEClient
{
    public sealed class Kernel
    {
        private  static object _syncObj = new object();
    
        /// <summary>
        /// Ядро - общее для всех
        /// </summary>
        private static Kernel _kernel;
/*
        private static ServiceClient Client = new ServiceClient();
           
        
        /// <summary>
        /// Список зарегистрированных в ядре подсистем
        /// </summary>
        public List<ISubsystem> Subsystems = new List<ISubsystem>();

       
        /// <summary>
        /// Возвращает ядро
        /// </summary>
        public static Kernel GetKernel
        {
            get
            {
                if(_kernel == null)
                {
                    lock (_syncObj)
                    {
                        if(_kernel == null)
                            _kernel = new Kernel();
                    }
                }
                return _kernel;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        private Kernel()
        {
            Client.ProcessMesssageCompleted += new EventHandler<ProcessMesssageCompletedEventArgs>(RecievedMessage);
            Subsystems.Add(new AccessSubsystem());
        }

        void RecievedMessage(object sender, ProcessMesssageCompletedEventArgs e)
        {
            if(e.Result != null)
                SendMessage(e.Result);
            MessageBox.Show(e.Error.InnerException.ToString());
            
        }

        /// <summary>
        /// Отправка сообщения сервису для обработки
        /// </summary>
        /// <param name="msg"></param>
        public void SendMessage(ServiceMessage msg)
        {
            if (msg.Handler == KernelTypes.ClientKernel)
            {
                ISubsystem targetSubsystem = null;
                foreach (var subsystem in Subsystems)
                {
                    if (subsystem.Type() == msg.To) //Ищем целевую подсистему
                        targetSubsystem = subsystem;
                }
                if (targetSubsystem == null)
                {
                    return;
                }
                targetSubsystem.SendMessage(msg);
                return;
            }
            Client.ProcessMesssageAsync(msg);

        }*/

    }
}
