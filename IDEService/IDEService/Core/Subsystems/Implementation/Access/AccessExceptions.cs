using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEService.Core
{
    class AccessException :Exception
    {
        public AccessException(string errMsg):base(errMsg){ }
    }

    class AccessFolderExcepton : AccessException
    {
        public AccessFolderExcepton(string errMsg) : base(errMsg){ }
    }
}
