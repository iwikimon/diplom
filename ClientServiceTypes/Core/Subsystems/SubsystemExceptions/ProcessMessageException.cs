namespace IDEService.Core
{
    class ProcessMessageException :SubsystemException
    {
        public ProcessMessageException(string errorMsg) : base(errorMsg) { }
    }
}
