using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract]
    public enum NetworkMessages
    {
        /// <summary>
        /// Шифрованиие сообщения
        /// </summary>
        [EnumMember]
        Encode,

        /// <summary>
        /// Дешифроване сообщения
        /// </summary>
        [EnumMember]
        Decode
    }
}
