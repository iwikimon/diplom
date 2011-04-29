using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using IDEClient.Core.Controls;
using IDEService.Core;

namespace IDEClient.Core
{
    public sealed class Kernel
    {
        private static object _syncObj = new object();
        private bool msgsend = false;
        /// <summary>
        /// Ядро - общее для всех
        /// </summary>
        private static Kernel _kernel;

        /// <summary>
        /// Список зарегистрированных в ядре подсистем
        /// </summary>
        public List<ISubsystem> Subsystems = new List<ISubsystem>();

        public MainPage MainWindow { get; set; }


        public int UID { get; set; }
        /// <summary>
        /// Возвращает ядро
        /// </summary>
        public static Kernel GetKernel
        {
            get
            {
                if (_kernel == null)
                {
                    lock (_syncObj)
                    {
                        if (_kernel == null)
                            _kernel = new Kernel();
                    }
                }
                return _kernel;
            }
        }

        public ClientWorker Client { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        private Kernel()
        {
            Client = new ClientWorker("127.0.0.1", 4505);
            Client.ConnectAsync();
        }


        /// <summary>
        /// Обработка сообщения от клиента
        /// </summary>
        /// <param name="msg">Сообщение</param>
        public void SendMessage(byte[] msg)
        {
            var message = RecieveFromServer(msg);
            
            
            var result = SendMessage(message); //Обработка сообщения
            if(result != null)
                SendToServer(result);
        }
    

        /// <summary>
        /// Отправка сообщения ядру для обработки
        /// </summary>
        /// <param name="msg"></param>
        public ServiceMessage SendMessage(ServiceMessage msg)
        {     
            ISubsystem targetSubsystem = null;
            foreach (var subsystem in Subsystems)
            {
                if (subsystem.Type() == msg.To)//Ищем целевую подсистему
                    targetSubsystem = subsystem;
            }
            if (targetSubsystem == null)
            {
                throw new Exception("target subsystem is null");
                //return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Undefined, SubsystemType.Undefined, null, null);
            }
            var answer = targetSubsystem.SendMessage(msg);                    
            return answer;
        }

        private ServiceMessage RecieveFromServer(byte[] msg)
        {
            msgsend = false;
            var network = Subsystems.Where(x => x.Type() == SubsystemType.Network).First();
            //Десериализация и дешфрование сообщения
            var message =
                (ServiceMessage)
                network.SendMessage(new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Undefined,
                                                       SubsystemType.Network, NetworkMessages.Decode, new object[] { msg }))
                    .Message[0];
            return message;
        }

        /// <summary>
        /// Оправляет сообщение серверу
        /// </summary>
        /// <param name="msg"></param>
        public void SendToServer(ServiceMessage msg)
        {
            while (msgsend)
            {
                Thread.Sleep(50);
            }
            msgsend = true;
            var network = Subsystems.Where(x => x.Type() == SubsystemType.Network).First();
            if(UID != 0)
            {
                var newMsg = new object[msg.Message.Length + 1];
                for (int i = 0; i < msg.Message.Length;++i )
                    newMsg[i + 1] = msg.Message[i];
                newMsg[0] = UID;
                msg.Message = newMsg;
            }
            var answer =
                    (byte[])network.SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Undefined,
                                                                    SubsystemType.Network, NetworkMessages.Encode,
                                                                    new object[] { msg })).Message[0];
            Client.SendAsync(answer);
        }

        /// <summary>
        /// Регистрация подсистемы в ядре
        /// </summary>
        /// <param name="subsystem">Регистрируемая подсистема</param>
        public void RegisterSubsystem(ISubsystem subsystem)
        {
            //try
            //{
                Subsystems.Add(subsystem);
                StartSubsystem(subsystem);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    throw ex;
            //}

        }

        public void LoginAccess()
        {
            MainWindow.LayoutRoot.Children.Clear();
            MainWindow.LayoutRoot.Children.Add(new IDEControl());
        }

        public void Logout()
        {
            MainWindow.LayoutRoot.Children.Clear();
            MainWindow.LayoutRoot.Children.Add(new AccessControl());
        }

        public void Reconnect()
        {
            Client = new ClientWorker("127.0.0.1", 5432);
        }
        
        /// <summary>
        /// Отключение подсистемы из ядра
        /// </summary>
        /// <param name="subsystem">Отключаемая подсистема</param>
        public void UnRegisterSubsystem(ISubsystem subsystem)
        {
            Subsystems.Remove(subsystem);
            subsystem.Stop();
        }

        private static void StartSubsystem(ISubsystem subsystem)
        {
            subsystem.Start();
        }

        private static void StopSubsystem(ISubsystem subsystem)
        {
            subsystem.Stop();
        }

        private static void ReloadSubsytem(ISubsystem subsystem)
        {
            subsystem.Reload();
        }
    }
}
