namespace IDEService.Core
{
    class ModuleProcessException :SubsystemException
    {
        public ModuleProcessException(string errorMsg) : base(errorMsg) { }
    }
}
