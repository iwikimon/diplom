using System;

namespace IDEService.Core
{
    class SubsystemException : Exception
    {
        public SubsystemException(string errorMsg) : base(errorMsg) { }
    }
}
