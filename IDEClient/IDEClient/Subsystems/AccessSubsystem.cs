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
using IDEClient.ServiceReference1;

namespace IDEClient
{
    public class AccessSubsystem :ISubsystem
    {
        public AccessControl AccControl { get; set; }
        public RegistrationControl RegControl { get; set; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(ServiceMessage message)
        {
            MessageBox.Show(message.Message[0].ToString());
        }

        public SubsystemType Type()
        {
            return SubsystemType.Access;
        }
    }
}
