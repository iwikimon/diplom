using System;
using System.Collections.Generic;

namespace IDEService.Core
{
    interface IProjectModule :IDisposable
    {
        void AddProject(User u,string pname);

        void DeleteProject(Project prodject);

        void SelectProject(Project prodject);

        List<ProjectInfo> GetProjectList(User u);

        ProjectStructure GetStructure(User u, string pname);

        void RunProject(Project prodject);

        void AddFolder(Folder folder);

        void AddFile(File file);

        void AddMember(User member);
        
        void RemoveFolder(Folder folder);

        void RemoveFile(File file);

        void RemoveMember(User member);

    }
}
