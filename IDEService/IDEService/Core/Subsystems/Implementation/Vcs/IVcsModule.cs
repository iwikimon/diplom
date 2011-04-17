using System;
using System.Collections.Generic;

namespace IDEService.Core
{
    interface IVcsModule :IDisposable
    {
        void CreateRepository();

        void DeleteRepository();

        void Commit(File file);

        void ViewRevision(File file, int number);

        List<Revision> GetRevisionList(File file);
    }
}
