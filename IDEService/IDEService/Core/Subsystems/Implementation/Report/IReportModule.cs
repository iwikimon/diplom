using System;
using System.Collections.Generic;

using IDEService.Contracts.Data;

namespace IDEService.Core
{
    interface IReportModule :IDisposable
    {
        void UserReport(User user, DateTime from, DateTime to);

        void ProdjectReport(Project prodject, DateTime from, DateTime to);

        List<UserlogDto> GetUserlog(User u);

    }
}
