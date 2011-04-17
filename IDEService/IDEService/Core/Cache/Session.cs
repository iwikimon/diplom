using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEService.Core
{
    class Session
    {
        private static Session _session = new Session();

        public LinkedList<UserCache> Cache;

        private Session()
        {
            Cache = new LinkedList<UserCache>();
        }

        public static Session Get
        {
            get
            {
                return _session; 
            }
        }
    }
}
