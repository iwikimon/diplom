using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace IDEService.Core
{
    class NetworkModule :INetworkModule
    {
        private ISubsystem _networkSubsystem;

        public NetworkModule(ISubsystem networkSubsystem)
        {
            if (networkSubsystem.Type() != SubsystemType.Network)
                throw new ModuleLoadException("Неправильный тип подсистемыы");
            _networkSubsystem = networkSubsystem;
        }

        public void Dispose()
        {
            
        }

        /// <summary>
        /// Дешифрование сообщения
        /// </summary>
        /// <param name="msg">Зашифрованный поток байт</param>
        /// <returns>Сервисное сообщение</returns>
        public ServiceMessage Decode(byte[] msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Шифрование сообщения
        /// </summary>
        /// <param name="msg">Сервисное сообщение</param>
        /// <returns>Зашированный поток байт</returns>
        public byte[] Encode(ServiceMessage msg)
        {
            throw new NotImplementedException();
        }

        private static ServiceMessage DeserializeData(string data)
        {
            var reader = XmlDictionaryReader.CreateTextReader(Encoding.UTF8.GetBytes(data),
                                                                              new XmlDictionaryReaderQuotas());
            var deserializer = new DataContractSerializer(typeof(ServiceMessage));
            return (ServiceMessage)deserializer.ReadObject(reader, true);
        }

        private static byte[] SerializeData(ServiceMessage msg)
        {
            var serializer = new DataContractSerializer(typeof(ServiceMessage));
            var writer = new MemoryStream();
            serializer.WriteObject(writer, msg);
            return writer.ToArray();
        }
    }
}
