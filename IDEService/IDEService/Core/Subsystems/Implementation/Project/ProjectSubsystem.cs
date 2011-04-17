using System;
using System.IO;

namespace IDEService.Core
{
    class ProjectSubsystem :Subsystem
    {

        public ProjectModule Module { get; private set; }

        public string ProjectDir { get; private set; }

        public User CurrentUser = null;

        public ProjectSubsystem() : base(SubsystemType.Project)
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
          //TODO: переделать
           /* if (CurrentUser == null)
                CurrentUser =
                    (User)
                    Kernel.GetKernel.SendMessage(new ServiceMessage(SubsystemType.Prodject, SubsystemType.Access,
                                                                    AccessMessages.GetUser, new object[]{})).Message[0];
            */var msgType = ProjectMessages.Undefined;
            try
            {
                msgType = (ProjectMessages)Convert.ChangeType(message.Type, typeof(ProjectMessages));
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
                            Module.AddProject((string)message.Message[0]);
                            return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.Undefined, new object[] { "Проект успешно создан" });
                        }
                        catch(Exception ex)
                        {
                            return new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Project,SubsystemType.Project,ProjectMessages.Undefined,new object[]{ex.Message});
                        }
                    }
            }
            throw new Exception();
        }

    }
}
