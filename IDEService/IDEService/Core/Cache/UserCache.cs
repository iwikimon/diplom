using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IDEService.Core
{
    /// <summary>
    /// Серверная реализация кеша. 
    /// </summary>
    [DataContract]
    public class UserCache : Cache
    {
        /// <summary>
        /// Клиент, котрому соответствует кеш
        /// </summary>
        [DataMember]
        public User Client { get; set; }

        public UserCache(User user)
        {
            Client = user;
        }
    }
}
