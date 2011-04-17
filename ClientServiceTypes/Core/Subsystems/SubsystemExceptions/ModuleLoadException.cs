namespace IDEService.Core
{
    class ModuleLoadException :SubsystemException
    {
        public ModuleLoadException(string errorMsg): base(errorMsg){ }
    }
}
