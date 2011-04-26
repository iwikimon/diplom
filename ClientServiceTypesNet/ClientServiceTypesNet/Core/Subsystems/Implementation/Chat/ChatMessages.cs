using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IDEService.Core
{
    /// <summary>
    /// Типы сообщенийц, обрабатываемых подсистемой чата
    /// </summary>
    [DataContract]
    enum ChatMessages
    {
        /// <summary>
        /// Получить последние сообщения
        /// </summary>
        [EnumMember]
        GetMessages,

        /// <summary>
        /// Получить всю историю чата
        /// </summary>
        [EnumMember]
        GetHistory,

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        [EnumMember]
        Send,

        /// <summary>
        /// Получения сообщения
        /// </summary>
        [EnumMember]
        Recieve,

        /// <summary>
        /// Обновление информации
        /// </summary>
        [EnumMember]
        Update,

        /// <summary>
        /// Неопознанное сообщение
        /// </summary>
        [EnumMember]
        Undefined
    }
}
