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
        private ProjectSubsystem _prodjectSubsystem;

        private DBModelUnitOfWork context;

        public ProjectModule(ISubsystem prodjectSubsystem)
        {
            if (prodjectSubsystem.Type() != SubsystemType.Project)
                throw new ModuleLoadException("Неправильный тип подсистемы");
            _prodjectSubsystem = (ProjectSubsystem)prodjectSubsystem;

            context = (DBModelUnitOfWork)Kernel.GetKernel.
                SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project, SubsystemType.DataBase,
                                               DbSubsystemMessages.GetContext, new object[] { })).Message[0];
        }

        public void AddProject(string pname)
        {
            /*if (_prodjectSubsystem.CurrentUser.Owner.Where(p => p.Name == pname).Count() > 0)
                throw new CreateProdjectException("Проект с такими названием уже существует");
            string dir = _prodjectSubsystem.ProdjectDir + "\\" + _prodjectSubsystem.CurrentUser.Login + "\\" + pname;
            try
            {
                Directory.CreateDirectory(dir);
            }
            catch (Exception)
            {
                Logger.Log.Write(LogLevels.Emerg,
                    String.Format("Невозможно создать директорию проекта {0}. Проверьте путь и права доступа.", dir));
                throw new CreateProdjectFolderException("Невозможно создать директорию проекта. Проверьте путь и права доступа.");
            }
            var prj = Prodject.CreateProdject(0, pname, dir);
            context.ProdjectSet.AddObject(prj);
            prj.User = _prodjectSubsystem.CurrentUser;
            prj.Members = _prodjectSubsystem.CurrentUser;
            Kernel.GetKernel.SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Prodject, SubsystemType.DataBase,
                                                            DbSubsystemMessages.SaveContext, new object[] {}));
       */ }

        public void DeleteProject(Project prodject)
        {
            throw new NotImplementedException();
        }

        public void SelectProject(Project prodject)
        {
            throw new NotImplementedException();
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
