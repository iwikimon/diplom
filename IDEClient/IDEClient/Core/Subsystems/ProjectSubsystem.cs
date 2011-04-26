using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IDEClient.Core.Controls;
using IDEService.Core;

namespace IDEClient.Core.Subsystems
{
    public class ProjectSubsystem :ISubsystem
    {

        public ProjectControl control { get; set; }
        public void Dispose()
        {
            
        }

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            return true;
        }

        public bool Reload()
        {
            Stop();
            return Start();
        }

        public ServiceMessage SendMessage(ServiceMessage message)
        {
            switch ((ProjectMessages)message.Type)
            {
                case  ProjectMessages.GetProjectList:
                    control.SendMessage(message);
                    return null;
                case ProjectMessages.GetStructure:
                    control.SendMessage(message);
                    return null;
            }
            return null;
        }

        public SubsystemType Type()
        {
            return SubsystemType.Project;
        }

        public SubsystemState GetState()
        {
            throw new NotImplementedException();
        }

        public SubsystemInfo GetInfo()
        {
            throw new NotImplementedException();
        }
    }
}
