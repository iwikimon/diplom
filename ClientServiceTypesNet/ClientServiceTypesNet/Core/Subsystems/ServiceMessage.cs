using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ClientServiceTypesNet.Core;

namespace IDEService.Core
{
    /// <summary>
    /// Сообщения, передаваемые между сервисом и клиентом
    /// </summary>
    [DataContract]
    [KnownType(typeof(AccessMessages))]
    [KnownType(typeof(NetworkMessages))]
    [KnownType(typeof(ChatMessages))]
    [KnownType(typeof(ProdjectMessages))]
    [KnownType(typeof(ReportMessages))]
    [KnownType(typeof(UserlogDto))]
    [KnownType(typeof(DateTime))]
    [KnownType(typeof(List<UserlogDto>))]
    public class ServiceMessage
    {

        /// <summary>
        /// Тип ядра-получателя
        /// </summary>
        [DataMember]
        public KernelTypes Handler { get; set; }
        
        /// <summary>
        /// От какой подсистемы сообщение
        /// </summary>
        [DataMember]
        public SubsystemType From { get; set; }

        /// <summary>
        /// К какой подсистеме сообщение
        /// </summary>
        [DataMember]
        public SubsystemType To { get; set; }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [DataMember]
        public System.Enum Type { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [DataMember]
        public object[] Message { get; set; }

        public ServiceMessage(KernelTypes handler,SubsystemType from, SubsystemType to, Enum type, object[] msg)
        {
            Handler = handler;
            From = from;
            To = to;
            Type = type;
            Message = msg;
        }

        public override string ToString()
        {
            var msg = new StringBuilder();
            foreach (var obj in Message)
                msg.Append(" "+obj.ToString());
            return string.Format("From: {0}, To: {1}, Type: {2}, Content: {3} ",  From, To, Type,
                                 msg.ToString());
        }
    }
}
