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
using IDEClient.ServiceReference1;

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

        private void Enter(object sender, RoutedEventArgs e)
        {
            Kernel.GetKernel.SendMessage(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Access, SubsystemType.Access,  
                                              AccessMessages.Login,new object[] {_loginBox.Text, _passwdBox.Password}));
          
        }


        public void SendMessage(ServiceMessage message)
        {
            var msgType = AccessMessages.Undefined;
            try
            {
                msgType = (AccessMessages) message.Type;
            }
            catch (Exception)
            {
              return;
            }
            switch (msgType)
            {
                case AccessMessages.CheckLogin:
                    return;
                case AccessMessages.GetInfo:
                    return;
                case AccessMessages.Login:
                    {
                        var result = message.Message[0].ToString();
                        MessageBox.Show(result.ToString());
                        return;
                    }
                case AccessMessages.Register:
                    return;
                case AccessMessages.Logout:
                    return;
                default:
                    throw new SubsystemWorkingException("Неопознанный тип сообщения.");
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

        }

    }
}
