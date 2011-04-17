using System.Runtime.Serialization;

namespace IDEService.Core
{
    /// <summary>
    /// перечисление типов ядер
    /// </summary>
    [DataContract]
    public enum KernelTypes
    {
        /// <summary>
        /// Ядро сервиса
        /// </summary>
        [EnumMember]
        ServiceKernel,

        /// <summary>
        /// Ядро клиента
        /// </summary>
        [EnumMember]
        ClientKernel
    }
}
