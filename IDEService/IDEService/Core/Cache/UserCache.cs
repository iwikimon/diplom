using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IDEService.Core
{
    [DataContract]
    public class UserCache :Cache
    {
        [DataMember]
        public User Client { get; set; }

        public UserCache(User user)
        {
            Client = user;
        }
    }
}
