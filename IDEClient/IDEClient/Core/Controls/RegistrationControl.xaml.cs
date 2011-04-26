using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using IDEClient.Core;
using IDEService.Core;


namespace IDEClient
{
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl()
        {
            ((AccessSubsystem)Kernel.GetKernel.Subsystems.Where(x => x.Type() == SubsystemType.Access).First()).
                RegControl = this;
            InitializeComponent();
            
        }
        public delegate void IncomingMessage(ServiceMessage msg);

        private bool  CheckLogin()
        {
            if (!Validator.Login.IsMatch(loginBox.Text))
            {
                infoLabel.Text = "Логин должен содержать от 3 до 14 символов";
                button1.IsEnabled = false;
                return false;
            }
            infoLabel.Text = " ";
            button1.IsEnabled = true;
            return true;
        }

        private bool CheckPasswd()
        {
            if (passwordBox1.Password != passwordBox2.Password)
            {
                infoLabel.Text = "Введенные пароли не совпадают";
                return false;
            }
            if(!Validator.Passwd.IsMatch(passwordBox1.Password))
            {
                infoLabel.Text = "Пароль должен содержать от 3 до 14 символов";
                return false;
            }
            infoLabel.Text = "";
            return true;
        }

        private bool CheckMail()
        {
            if (!Validator.Email.IsMatch(mailBox.Text))
            {
                infoLabel.Text = "Введите действительный адрес электронной почты";
                return false;
            }
            infoLabel.Text = "";
            return true;
        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            if (CheckLogin() && CheckPasswd() && CheckMail())
            {
                var msg = new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Access, SubsystemType.Access,
                                             AccessMessages.Register, 
                                             new object[] { mailBox.Text , nameBox.Text, snameBox.Text, 
                                                 loginBox.Text, passwordBox1.Password});
                Kernel.GetKernel.SendToServer(msg);
            }
        }

        private void loginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckLogin();
        }

        private void passwordBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckPasswd();
        }

        private void mailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckMail();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel,SubsystemType.Access,
                SubsystemType.Access, AccessMessages.CheckLogin, new object[] { loginBox.Text } ));
        }

        public void SendMessage(ServiceMessage msg)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new IncomingMessage(SendMessage), msg);
                return;
            }
            if ((AccessMessages)msg.Type == AccessMessages.Register)
                infoLabel.Text = (bool)msg.Message[0] ? 
                    "Пользователь успешно зарегистрирован" : 
                    "Пользователь c таким логином уже существует";
            if((AccessMessages)msg.Type == AccessMessages.CheckLogin)
                infoLabel.Text = msg.Message[0].ToString() != "Free" ?
                    "Пользователь c таким логином уже существует" :
                    "Логин свободен";
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            Kernel.GetKernel.MainWindow.LayoutRoot.Children.Clear();
            Kernel.GetKernel.MainWindow.LayoutRoot.Children.Add(new AccessControl());
        }

    }
}
