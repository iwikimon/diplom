using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IDEClient.Core;
using IDEService.Core;

namespace IDEClient
{
    public partial class AccessControl : UserControl
    {
        public AccessControl()
        {
            InitializeComponent();
            ((AccessSubsystem) Kernel.GetKernel.Subsystems.Where(x => x.Type() == SubsystemType.Access).First()).
                AccControl = this;
        }

        public delegate void IncomingMessage(ServiceMessage msg);
        private void Enter(object sender, RoutedEventArgs e)
        {
            Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Access, SubsystemType.Access,  
                                              AccessMessages.Login,new object[] {_loginBox.Text, _passwdBox.Password}));
          
        }


        public void SendMessage(ServiceMessage message)
        {
            if(!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new IncomingMessage(SendMessage), message);
                return;
            }
            switch ((AccessMessages) message.Type)
            {
                case AccessMessages.CheckLogin:
                    return;
                case AccessMessages.GetInfo:
                    return;
                case AccessMessages.Login:
                    {
                        if (message.Message[0].ToString() == "0")
                            infoLabel.Text = "Пользователя с таким логином и/или паролем не существует";
                        else
                        {
                            Kernel.GetKernel.UID = int.Parse(message.Message[0].ToString());
                            Kernel.GetKernel.LoginAccess();
                        }
                        return;
                    }
                case AccessMessages.Register:
                    return;
                case AccessMessages.Logout:
                    {
                        Kernel.GetKernel.UID = 0;
                        Kernel.GetKernel.Logout();
                        return;
                    }
                default:
                    throw new Exception("Неопознанный тип сообщения.");
            }
        }

        public SubsystemType Type()
        {
            return SubsystemType.Access;
        }

        public void Dispose()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Kernel.GetKernel.MainWindow.LayoutRoot.Children.Clear();
            Kernel.GetKernel.MainWindow.LayoutRoot.Children.Add(new RegistrationControl());
        }

    }
}
