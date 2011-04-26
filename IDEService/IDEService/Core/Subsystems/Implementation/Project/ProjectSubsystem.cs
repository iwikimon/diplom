using System;
using System.IO;
using IDEService.Core;
namespace IDEService.Core
{
    class Projectubsystem :Subsystem
    {

        public ProjectModule Module { get; private set; }

        public string ProjectDir { get; private set; }

        public Projectubsystem() : base(SubsystemType.Project)
        {
        }

        public override bool Start()
        {
            ProjectDir = Configure.Cfg.Read<string>(Info.Type.ToString(), "ProjectDirectory", "Path");
            if(!Directory.Exists(ProjectDir))
            try
            {
                Directory.CreateDirectory(ProjectDir);
            }
            catch (Exception ex)
            {
                Logger.Log.Write(LogLevels.Emerg,"Невозможно установить директорию для файлов проектов "+ProjectDir);
                throw new Exception("Невозможно установить директорию для файлов проектов " + ProjectDir +" " + ex);
            }
            Module = BootLoader.LoadSubsystemModule<ProjectModule>(this);
            return true;
        }

        public override bool Stop()
        {
            Module.Dispose();
            return true;
        }

        public override bool Reload()
        {
            Stop();
            return Start();
        }

        public override ServiceMessage SendMessage(ServiceMessage message)
        {
            var cache = (UserCache)message.Message[0];
            var msgType =  ProjectMessages.Undefined;
            try
            {
                msgType = (IDEService.Core.ProjectMessages)Convert.ChangeType(message.Type, typeof(ProjectMessages));
            }
            catch (Exception)
            {
                Logger.Log.Write(LogLevels.Emerg,
                                 String.Format("Неправильный тип сообщения: {0} для подсистемы {1}",
                                                message.Type, this.Type()));
            }
            switch (msgType)
            {
                case ProjectMessages.CreateProject:
                    {
                        try
                        {
                            Module.AddProject(((UserCache)message.Message[0]), (string)message.Message[1]);
                            var answer = new  ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.Undefined, new object[] { true });
                            ((UserCache)message.Message[0]).LogMessages.Add((new UserlogDto() { Date = DateTime.Now, Message = "Создан проект " + (string)message.Message[1] }));
                            return answer;
                        }
                        catch(Exception ex)
                        {
                            return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project,SubsystemType.Project,ProjectMessages.Undefined,new object[]{ex.Message});
                        }
                    }
                case ProjectMessages.GetProjectList:
                    {
                        var lst = Module.GetProjectList(((UserCache)message.Message[0]).Client);
                        return new ServiceMessage(KernelTypes.ClientKernel,SubsystemType.Project,SubsystemType.Project,ProjectMessages.GetProjectList, new object[]{ lst});
                    }
                case  ProjectMessages.GetStructure:
                    {
                        var structure = Module.GetStructure(((UserCache) message.Message[0]),
                                                            (string) message.Message[1]);
                        return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.GetStructure, new object[] { structure });
                    }
                case ProjectMessages.AddFile:
                    {
                        var userCache = (UserCache) message.Message[0];
                        var parenFolder = (FolderDto) message.Message[1];
                        var file = (FileDto) message.Message[2];
                        try
                        {
                            Module.AddFile(userCache,parenFolder, file);
                        }
                        catch (Exception ex)
                        {
                            return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.AddFile, new object[] { false,ex.ToString() });
                        }
                        return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.AddFile, new object[] { true, "Файл успешно добавлен" });
                    }
                case ProjectMessages.AddFolder:
                    {
                        var userCache = (UserCache)message.Message[0];
                        var parenFolder = (FolderDto)message.Message[1];
                        var folder = (FolderDto)message.Message[2];
                        try
                        {
                            Module.AddFolder(userCache, parenFolder, folder);
                        }
                        catch (Exception ex)
                        {
                            return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.AddFolder, new object[] { false, ex.ToString() });
                        }
                        return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.AddFolder, new object[] { true, "Папка успешно добавлена" });
                    }
                case ProjectMessages.RemoveFile:
                    {
                        var userCache = (UserCache)message.Message[0];
                        var file = (FileDto)message.Message[1];
                        try
                        {
                            Module.RemoveFile(userCache,file);
                        }
                        catch(Exception ex)
                        {
                            return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.RemoveFile, new object[] { false, ex.ToString() });
                        } 
                        return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.RemoveFile, new object[] { true, "Файл успешно удален" });
                   }
                case ProjectMessages.RemoveFolder:
                    {
                        var userCache = (UserCache)message.Message[0];
                        var folder = (FolderDto)message.Message[1];
                        try
                        {
                            Module.RemoveFolder(userCache, folder);
                        }
                        catch (Exception ex)
                        {
                            return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.RemoveFolder, new object[] { false, ex.ToString() });
                        }
                        return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.RemoveFolder, new object[] { true, "Папка успешно удалена" });
                    }
            }
            throw new Exception();
        }

    }
}
