using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using ClientServiceTypes.Core;

namespace IDEService.Core
{
    class NetworkModule :INetworkModule
    {
        private ISubsystem _networkSubsystem;

        private string aesPasswd = "password";

        private byte[] aesSalt = new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 };

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
            var decodedData = AESCrypto.AES.Decrypt(msg, aesPasswd, aesSalt);
            return DeserializeData(decodedData);
        }

        /// <summary>
        /// Шифрование сообщения
        /// </summary>
        /// <param name="msg">Сервисное сообщение</param>
        /// <returns>Зашированный поток байт</returns>
        public byte[] Encode(ServiceMessage msg)
        {
            var toDecode = SerializeData(msg);
            Logger.Log.Write(LogLevels.Debug, "отправляем клиенту: " + Encoding.UTF8.GetString(toDecode));
            return AESCrypto.AES.Encrypt(toDecode, aesPasswd, aesSalt);
        }

        private static ServiceMessage DeserializeData(byte[] data)
        {
            var reader = XmlDictionaryReader.CreateTextReader(data, XmlDictionaryReaderQuotas.Max);
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
