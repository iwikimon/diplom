using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Xml;
using IDEService.Core;
using ClientServiceTypes.Core;

namespace IDEClient.Core.Subsystems
{
    public class NetworkSubsystem :ISubsystem
    {
        private string aesPasswd = "asdjashkfjashdfkjas";

        private byte[] aesSalt = new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 };

        public void Dispose()
        {
            
        }

        public bool Start()
        {
            byte[] a = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            var b = AESCrypto.AES.Encrypt(a, aesPasswd, aesSalt);
            var c = AESCrypto.AES.Decrypt(b, aesPasswd, aesSalt);
            int aa = 5;
            return aa == 5;
        }

        public bool Stop()
        {
            return true;
        }

        public bool Reload()
        {
            Stop();
            Start();
            return true;
        }

        public ServiceMessage SendMessage(ServiceMessage message)
        {
            var msgType = (NetworkMessages) message.Type;
            switch(msgType)
            {
                case NetworkMessages.Encode:
                    return new ServiceMessage(KernelTypes.ClientKernel,SubsystemType.Network,
                                              SubsystemType.Network,NetworkMessages.Decode,
                                              new object[] { Encode((ServiceMessage) message.Message[0])});
                case NetworkMessages.Decode:
                    return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Network,
                                              SubsystemType.Network, NetworkMessages.Decode,
                                              new object[] {Decode((byte[]) message.Message[0])});

            }
            throw new Exception("неверный тип сообщения");
        }

        public SubsystemType Type()
        {
            return SubsystemType.Network;
        }

        public SubsystemState GetState()
        {
            throw new NotImplementedException();
        }

        public SubsystemInfo GetInfo()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Дешифрование сообщения
        /// </summary>
        /// <param name="msg">Зашифрованный поток байт</param>
        /// <returns>Сервисное сообщение</returns>
        public ServiceMessage Decode(byte[] msg)
        {
            var decodedData = AESCrypto.AES.Decrypt(msg, aesPasswd, aesSalt);
            var result = DeserializeData(decodedData);
            return result;
        }

        /// <summary>
        /// Шифрование сообщения
        /// </summary>
        /// <param name="msg">Сервисное сообщение</param>
        /// <returns>Зашированный поток байт</returns>
        public byte[] Encode(ServiceMessage msg)
        {
            var toDecode = SerializeData(msg);
            var result =  AESCrypto.AES.Encrypt(toDecode, aesPasswd, aesSalt);
            return result;
        }

        private static ServiceMessage DeserializeData(byte[] data)
        {
            try
            {
                var reader = XmlDictionaryReader.CreateTextReader(data, XmlDictionaryReaderQuotas.Max);
                var deserializer = new DataContractSerializer(typeof (ServiceMessage));
                var result = (ServiceMessage) deserializer.ReadObject(reader, false);
                return result;
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return null;
            }
        }

        private static byte[] SerializeData(ServiceMessage msg)
        {
            var serializer = new DataContractSerializer(typeof(ServiceMessage));
            var writer = new MemoryStream();
            serializer.WriteObject(writer, msg);
            var result = writer.ToArray();
            return result;
        }

    }
}
