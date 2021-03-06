﻿
using System;
using System.Collections.Generic;
using System.Linq;

using IDEService.Contracts.Data;

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

        public List<UserlogDto> GetUserlog(User u)
        {
            return u.Userlogs.Select(ul => new UserlogDto() { Date = ul.Date, Message = ul.Message }).OrderByDescending(x => x.Date).Take(10).OrderBy(x => x.Date).ToList();
        }

        public void Dispose()
        {
            
        }
    }
}
