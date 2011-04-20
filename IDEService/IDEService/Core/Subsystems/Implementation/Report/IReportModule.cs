using System;
using System.Collections.Generic;
using ClientServiceTypesNet.Core;
using IDEService.Contracts.Data;

namespace IDEService.Core
{
    interface IReportModule :IDisposable
    {
        void UserReport(User user, DateTime from, DateTime to);

        void ProdjectReport(Project prodject, DateTime from, DateTime to);

        List<ClientServiceTypesNet.Core.UserlogDto> GetUserlog(User u);

    }
}
