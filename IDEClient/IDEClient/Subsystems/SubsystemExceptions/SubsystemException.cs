using System;

namespace IDEClient
{
    class SubsystemException : Exception
    {
        public SubsystemException(string errorMsg) : base(errorMsg) { }
    }
}
