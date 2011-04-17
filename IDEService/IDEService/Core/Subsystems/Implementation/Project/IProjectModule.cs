using System;

namespace IDEService.Core
{
    interface IProjectModule :IDisposable
    {
        void AddProject(string pname);

        void DeleteProject(Project prodject);

        void SelectProject(Project prodject);

        void RunProject(Project prodject);

        void AddFolder(Folder folder);

        void AddFile(File file);

        void AddMember(User member);
        
        void RemoveFolder(Folder folder);

        void RemoveFile(File file);

        void RemoveMember(User member);

    }
}
