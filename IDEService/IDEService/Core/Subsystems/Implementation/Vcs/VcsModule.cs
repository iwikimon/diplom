using System;
using System.Collections.Generic;

namespace IDEService.Core
{
    class VcsModule :IVcsModule
    {
        private ISubsystem _vcsSubsystem;

        public VcsModule(ISubsystem vcsSubsystem)
        {
            if (vcsSubsystem.Type() != SubsystemType.Vcs)
                throw new ModuleLoadException("Неправильный тип подсистемы");
            _vcsSubsystem = vcsSubsystem;
        }

        public void CreateRepository()
        {
            throw new NotImplementedException();
        }

        public void DeleteRepository()
        {
            throw new NotImplementedException();
        }

        public void Commit(File file)
        {
            throw new NotImplementedException();
        }

        public void ViewRevision(File file, int number)
        {
            throw new NotImplementedException();
        }

        public List<Revision> GetRevisionList(File file)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
