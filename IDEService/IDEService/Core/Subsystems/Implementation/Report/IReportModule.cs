using System;

namespace IDEService.Core
{
    interface IReportModule :IDisposable
    {
        void UserReport(User user, DateTime from, DateTime to);

        void ProdjectReport(Project prodject, DateTime from, DateTime to);
    }
}
