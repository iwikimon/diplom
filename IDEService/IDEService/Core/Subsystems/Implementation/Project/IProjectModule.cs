using System;
using System.Collections.Generic;

namespace IDEService.Core
{
    interface IProjectModule :IDisposable
    {
        void AddProject(UserCache u,string pname);

        void DeleteProject(Project prodject);

        void SelectProject(Project prodject);

        List<ProjectInfo> GetProjectList(User u);

        ProjectStructure GetStructure(UserCache u, string pname);

        void RunProject(Project prodject);

        void AddFolder(UserCache cache, FolderDto parentFolder, FolderDto folder);

        void AddFile(UserCache cache, FolderDto parentFolder, FileDto file);

        void AddMember(UserCache cache, User member);

        void RemoveFolder(UserCache cache, FolderDto folder);

        void RemoveFile(UserCache cache, FileDto file);

        void RemoveMember(UserCache cache, User member);

    }
}
