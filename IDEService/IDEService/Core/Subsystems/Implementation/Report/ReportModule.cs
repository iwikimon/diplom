
using System;

namespace IDEService.Core
{
    class ReportModule :IReportModule
    {
        private ISubsystem _reportSubsystem;

        public ReportModule(ISubsystem reportSubsystem)
        {
            if (reportSubsystem.Type() != SubsystemType.Report)
                throw new ModuleLoadException("Неправильный тип подсистемы");
            _reportSubsystem = reportSubsystem;
        }

        public void UserReport(User user, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public void ProdjectReport(Project prodject, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
