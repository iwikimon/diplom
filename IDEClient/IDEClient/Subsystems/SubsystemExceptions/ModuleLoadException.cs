namespace IDEClient
{
    class ModuleLoadException :SubsystemException
    {
        public ModuleLoadException(string errorMsg): base(errorMsg){ }
    }
}
