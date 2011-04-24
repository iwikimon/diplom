using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Mindscape.LightSpeed;

namespace IDEService.Core
{
    class ProjectModule :IProjectModule
    {
        private Projectubsystem _prodjectSubsystem;

        private DBModelUnitOfWork context;

        public ProjectModule(ISubsystem prodjectSubsystem)
        {
            if (prodjectSubsystem.Type() != SubsystemType.Project)
                throw new ModuleLoadException("Неправильный тип подсистемы");
            _prodjectSubsystem = (Projectubsystem)prodjectSubsystem;

            context = (DBModelUnitOfWork)Kernel.GetKernel.
                SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project, SubsystemType.DataBase,
                                               DbSubsystemMessages.GetContext, new object[] { })).Message[0];
        }

        public void AddProject(User user, string pname)
        {
            if(pname.ToCharArray().Contains('/'))
                return;
            if (user.ProjectsOwner.Where(p => p.Name == pname).Count() > 0)
                throw new CreateProdjectException("Проект с такими названием уже существует");
            string dir = _prodjectSubsystem.ProjectDir + "\\" + user.Login + "\\" + pname;
            try
            {
                Directory.CreateDirectory(dir);
            }
            catch (Exception)
            {
                Logger.Log.Write(LogLevels.Emerg,
                    String.Format("Невозможно создать директорию проекта {0}. Проверьте путь и права доступа.", dir));
                throw new CreateProdjectFolderException("Невозможно создать директорию проекта. Обратитесь к системному администратору");
            }
            var prj = new Project();
            context.Add(prj);
            prj.Owner = user;
            prj.Sourcedir = dir;
            prj.Folders.Add(new Folder(){Name = pname,Path = dir});
            prj.Name = pname;
            prj.Members.Add(user);
            user.Userlogs.Add(new Userlog(){Date = DateTime.Now, Message = "Создан проект "+pname});
            Kernel.GetKernel.SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project, SubsystemType.DataBase,
                                                            DbSubsystemMessages.SaveContext, new object[] {}));
            
        }

        public void DeleteProject(Project prodject)
        {
            throw new NotImplementedException();
        }

        public void SelectProject(Project prodject)
        {
            throw new NotImplementedException();
        }

        public List<ProjectInfo> GetProjectList(User u)
        {
            var prjLst = (from owner in u.ProjectsOwner
                          let members = owner.Members.Select(member => member.Login).ToList()
                          select new ProjectInfo(owner.Name, owner.Owner.Login, members)).ToList();
            return prjLst;
        }

        public ProjectStructure GetStructure(User u, string pname)
        {
            var project = u.ProjectsOwner.Where(x => x.Name == pname).First();
            var structure = new ProjectStructure();
            //TODO: Реализовать учет прав доступа.
            foreach (var folder in project.Folders)
            {
                structure.Folders.Add(new FolderDto(){Name = folder.Name, Path = folder.Path});
                foreach (var file in folder.Files)
                {
                    structure.Files.Add(new FileDto(){Name = file.Name, Path = file.Path});
                }
            }
            return structure;
        }

        public void RunProject(Project prodject)
        {
            throw new NotImplementedException();
        }

        public void AddFolder(Folder folder)
        {
            throw new NotImplementedException();
        }

        public void AddFile(File file)
        {
            throw new NotImplementedException();
        }

        public void AddMember(User member)
        {
            throw new NotImplementedException();
        }

        public void RemoveFolder(Folder folder)
        {
            throw new NotImplementedException();
        }

        public void RemoveFile(File file)
        {
            throw new NotImplementedException();
        }

        public void RemoveMember(User member)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
