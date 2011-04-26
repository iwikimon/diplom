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
using IDEClient.Core.Subsystems;

namespace IDEClient.Core
{
    public class BootLoader
    {
        public static void Init()
        {
            Kernel.GetKernel.RegisterSubsystem(new NetworkSubsystem());
            Kernel.GetKernel.RegisterSubsystem(new AccessSubsystem());
            Kernel.GetKernel.RegisterSubsystem(new ReportSubsystem());
            Kernel.GetKernel.RegisterSubsystem(new ProjectSubsystem()); 
        }
    }
}
