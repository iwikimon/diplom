using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IDEService.Core
{
    [DataContract]
    public class UserCache
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public User Client { get; set; }
        [DataMember]
        public DateTime LoginTime { get; set; }
        [DataMember]
        public DateTime DisconnectTime { get; set; }
        [DataMember]
        public string IP { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя в текущей сессии
        /// </summary>
        [DataMember]
        public int UID { get; set; }
        [DataMember]
        public LinkedList<ChatMessage> ChatMessages { get; set; }
        [DataMember]
        public LinkedList<TextMessage> TextMessages { get; set; }

        public UserCache(User user)
        {
            ChatMessages = new LinkedList<ChatMessage>();
            TextMessages = new LinkedList<TextMessage>();
            LoginTime = DateTime.Now;
            Client = user;
        }

    }
}
