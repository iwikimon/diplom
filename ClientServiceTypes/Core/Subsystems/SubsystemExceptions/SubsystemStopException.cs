namespace IDEService.Core
{
    class SubsystemStopException :SubsystemException
    {
        public SubsystemStopException(string errorMsg) : base(errorMsg) { }
    }
}
