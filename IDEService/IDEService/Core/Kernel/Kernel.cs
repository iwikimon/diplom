using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IDEService.service;

namespace IDEService.Core
{
    public sealed class Kernel
    {
        private  static object _syncObj = new object();
    
        /// <summary>
        /// Ядро - общее для всех
        /// </summary>
        private static Kernel _kernel;
        
        /// <summary>
        /// Список зарегистрированных в ядре подсистем
        /// </summary>
        public List<ISubsystem> Subsystems = new List<ISubsystem>();

        public SortedDictionary<int, UserCache> Clients = new SortedDictionary<int, UserCache>();
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
            Logger.Log.Write(LogLevels.Debug,"Запуск ядра...");
        }

 

        /// <summary>
        /// Обработка сообщения от клиента
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <returns>ответ</returns>
        public byte[] SendMessage(byte[] msg, string ip)
        {
            var network = Subsystems.Where(x => x.GetInfo().Type == SubsystemType.Network).First();
            //Десериализация и дешфрование сообщения
            var message = (ServiceMessage)network.SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Undefined,
                                                   SubsystemType.Network, NetworkMessages.Decode, new object[] {msg})).Message[0];
            try
            {
                var uid = (int)message.Message[0];
                UserCache cache;
                if (Clients.TryGetValue(uid, out cache))
                    if(cache.IP == ip)
                        message.Message[0] = cache;
            }
            catch (Exception)
            { /*пользователь не был найден, значит не прошел регистрацию и IP не записался.*/}
            var result = SendMessage(message);//Обработка сообщения
            if( (AccessMessages)message.Type == AccessMessages.Login)
            {
                try
                {
                    UserCache tmp;
                    Clients.TryGetValue((int) result.Message[0], out tmp);
                    tmp.IP = ip;
                }
                catch(Exception)
                { /*пользователь не был найден, значит не прошел регистрацию и IP не записался.*/ }
            }
            //Сериализация и шифрование результата
            var answer =(byte[])network.SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Undefined,
                                                       SubsystemType.Network, NetworkMessages.Encode,
                                                       new object[] {result})).Message[0];
            return answer;
        }

        /// <summary>
        /// Отправка сообщения сервису для обработки
        /// </summary>
        /// <param name="msg"></param>
        public ServiceMessage SendMessage(ServiceMessage msg)
        {
            Logger.Log.Write(LogLevels.Debug, "Пришло сообщение: " + msg.ToString());
            ISubsystem targetSubsystem = null;
            foreach (var subsystem in Subsystems)
            {
                if (subsystem.Type() == msg.To)//Ищем целевую подсистему
                    targetSubsystem = subsystem;
            }
            if (targetSubsystem == null)
            {
                Logger.Log.Write(LogLevels.Emerg,string.Format("Целевая подсистема {0} не зарегистрирвана",msg.To));
                return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Undefined, SubsystemType.Undefined, null, null);
            }
            var info = targetSubsystem.GetInfo();                              //Сбор информации  
            var startTime = DateTime.Now;                                      //О работе подсистемы  
            var answer = targetSubsystem.SendMessage(msg);                     //
            var endTime = DateTime.Now;                                        // 
            info.ProcessTime += (endTime - startTime).TotalMilliseconds;
            info.RecievedMessages++;
            info.MeanTimeToMessage = info.ProcessTime / info.RecievedMessages;
            return answer;
        }

        /// <summary>
        /// Регистрация подсистемы в ядре
        /// </summary>
        /// <param name="subsystem">Регистрируемая подсистема</param>
        public void RegisterSubsystem(ISubsystem subsystem)
        {
            if (Subsystems.Any(tmpsubsystem => tmpsubsystem.Type() == subsystem.Type()))
            {
                throw new SubsystemRegistredException("Такая подсистема уже зарегистрирована");
            }
            try
            {
                Subsystems.Add(subsystem);
                Logger.Log.Write(LogLevels.Debug, "Регитсрация подсистемы типа " + subsystem.Type().ToString());
                StartSubsystem(subsystem);
            }
            catch(SubsystemException ex)
            {
                Logger.Log.Write(LogLevels.Error, "Ошибка регистрации подсистемы" + subsystem.Type() + ":\n\n" + ex);
            }
           
        }

        public void DisconnectUser(string ip)
        {
            try
            {
                var v = Clients.Where(x => x.Value.IP == ip).First();
                SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Access, SubsystemType.Access,
                                               AccessMessages.Logout, new object[] { v.Value }));
                Clients.Remove(v.Key);
            }
            catch (Exception ex)
            {
                Logger.Log.Write(LogLevels.Debug, ex.ToString());
            }
        }

        /// <summary>
        /// Отключение подсистемы из ядра
        /// </summary>
        /// <param name="subsystem">Отключаемая подсистема</param>
        public void UnRegisterSubsystem(ISubsystem subsystem)
        {
            Subsystems.Remove(subsystem);
            subsystem.Stop();
            Logger.Log.Write(LogLevels.Debug, "Отключение подсистемы типа " + subsystem.Type().ToString());
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
