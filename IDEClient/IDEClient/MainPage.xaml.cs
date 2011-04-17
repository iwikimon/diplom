using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using IDEClient.Core;
using IDEService.Core;

namespace IDEClient
{
    public partial class MainPage : UserControl
    {

        public delegate void GetResultCallback(string text);

        public MainPage()
        {
            //Kernel.GetKernel.ToString();
            BootLoader.Init();
            Kernel.GetKernel.MainWindow = this;
            InitializeComponent();
            LayoutRoot.Children.Add(new AccessControl());
        }

        // A single callback is used for all socket operations. 
        // This method forwards execution on to the correct handler 
        // based on the type of completed operation
        

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Access, SubsystemType.Access, AccessMessages.CheckLogin, new object[] { "Vasya" }));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           
        }


       

        private void WriteResult(string result)
        {
            if (this.Dispatcher.CheckAccess())
                MessageBox.Show(result);
            else
            {
                this.Dispatcher.BeginInvoke(new MainPage.GetResultCallback(WriteResult), result);
            }
        }

        private void stackPanel1_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }
        
    }
}
