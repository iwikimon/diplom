namespace IDEClient
{
    class ProcessMessageException :SubsystemException
    {
        public ProcessMessageException(string errorMsg) : base(errorMsg) { }
    }
}
