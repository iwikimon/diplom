namespace IDEClient
{
    class SubsystemStopException :SubsystemException
    {
        public SubsystemStopException(string errorMsg) : base(errorMsg) { }
    }
}
