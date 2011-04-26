using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IDEService.Contracts.Data;
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

        public void AddProject(UserCache cache, string pname)
        {
            if(pname.ToCharArray().Contains('/'))
                return;
            if (cache.Client.ProjectsOwner.Where(p => p.Name == pname).Count() > 0)
                throw new CreateProdjectException("Проект с такими названием уже существует");
            string dir = _prodjectSubsystem.ProjectDir + "\\" + cache.Client.Login + "\\" + pname;
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
            prj.Owner = cache.Client;
            prj.Sourcedir = dir;
            prj.Folders.Add(new Folder() { Name = pname, Path = _prodjectSubsystem.ProjectDir + "\\" + cache.Client.Login });
            prj.Name = pname;
            prj.Members.Add(cache.Client);
            var log = new Userlog() {Date = DateTime.Now, Message = "Создан проект " + pname};
            cache.Client.Userlogs.Add(log);
            cache.LogMessages.Add(log.AsDto());
            cache.NewProjects.Add(prj.AsDto());
            SaveContext();
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

        public ProjectStructure GetStructure(UserCache u, string pname)
        {
            var project = u.Client.ProjectsOwner.Where(x => x.Name == pname).First();
            u.CurrentProject = project.AsDto();
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


        public void AddFolder(UserCache cache, FolderDto parentFolder, FolderDto folder)
        {
            Project p = null;
            cache.CurrentProject.CopyTo(p);
            p.Folders.Add(new Folder() {Name = folder.Name, Path = folder.Path});
            Directory.CreateDirectory(folder.Path+"\\"+folder.Name);
            SaveContext();
            var log = new Userlog() { Date = DateTime.Now, Message = "Создание папки " + folder.Name };
            cache.Client.Userlogs.Add(log);
            cache.LogMessages.Add(log.AsDto());
            cache.StructureChanges.Add(new StructureChange(Action.Added, folder));
        }

        public void AddFile(UserCache cache, FolderDto parentFolder ,FileDto file)
        {
            Project p = null;
            cache.CurrentProject.CopyTo(p);
            var folder = p.Folders.Where(x => x.Path == parentFolder.Path && x.Name == parentFolder.Name).First();
            folder.Files.Add(new File() {Name = file.Name, Path = file.Path});
            System.IO.File.Create(file.Path + "\\" + file.Name);
            SaveContext();
            var log = new Userlog() { Date = DateTime.Now, Message = "Создание файла " + file.Name };
            cache.Client.Userlogs.Add(log);
            cache.LogMessages.Add(log.AsDto());
            cache.StructureChanges.Add(new StructureChange(Action.Added, file));
        }

        public void AddMember(UserCache cache, User member)
        {
            throw new NotImplementedException();
        }

        public void RemoveFolder(UserCache cache, FolderDto folder)
        {
            Project p = null;
            cache.CurrentProject.CopyTo(p);
            var f = p.Folders.Where(x => x.Path == folder.Path && x.Name == folder.Name).First();
            p.Folders.Remove(f);
            Directory.Delete(f.Path + "\\" + f.Name, true);
            SaveContext();
            var log = new Userlog() { Date = DateTime.Now, Message = "Удаление папки " + folder.Name };
            cache.Client.Userlogs.Add(log);
            cache.LogMessages.Add(log.AsDto());
            cache.StructureChanges.Add(new StructureChange(Action.Removed, folder));
        }

        public void RemoveFile(UserCache cache, FileDto file)
        {
            Project p = null;
            cache.CurrentProject.CopyTo(p);
            foreach(var folder in p.Folders)
            {
                var f = folder.Files.Where(x => x.Path == file.Path && x.Name == file.Name);
                if(f.Count() > 0)
                {
                    folder.Files.Remove(f.First());
                    System.IO.File.Delete(file.Path+"\\"+file.Name);
                    SaveContext();
                    return;
                }
            }
            SaveContext();
            var log = new Userlog() {Date = DateTime.Now, Message = "Удаление файла " + file.Name};
            cache.Client.Userlogs.Add(log);
            cache.LogMessages.Add(log.AsDto());
            cache.StructureChanges.Add(new StructureChange(Action.Removed,file));
        }

        public void RemoveMember(UserCache cache, User member)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            
        }


        private void SaveContext()
        {
            Kernel.GetKernel.SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project, SubsystemType.DataBase,
                                                            DbSubsystemMessages.SaveContext, new object[] { }));
        }
    }
}
