using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl()
        {
            ((AccessSubsystem)Kernel.GetKernel.Subsystems.Where(x => x.Type() == SubsystemType.Access).First()).
                RegControl = this;
            InitializeComponent();
            
        }

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
               /* var info = new Userinfo()
                               {
                                   _email = mailBox.Text,
                                   
                                   _name = nameBox.Text,
                                   _sname = snameBox.Text,
                                   _lastAccess = DateTime.Now,
                                   _registred = DateTime.Now,
                               };
                var user = new User()
                               {
                                   _login = loginBox.Text,
                                   _password = passwordBox1.Password,
                                
                                   
                               };
                user._userinfo._value = info;
                var msg = new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Access, SubsystemType.Access,
                                             AccessMessages.Register,new object[]{user});
                Kernel.GetKernel.SendMessage(msg);*/
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
            Kernel.GetKernel.SendMessage(new ServiceMessage(KernelTypes.ServiceKernel,SubsystemType.Access,
                SubsystemType.Access, AccessMessages.CheckLogin, new object[] { loginBox.Text } ));
        }

    }
}
