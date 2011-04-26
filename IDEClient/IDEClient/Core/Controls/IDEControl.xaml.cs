using System;
using System.Windows;
using System.Windows.Controls;
using IDEService.Core;


namespace IDEClient.Core.Controls
{
    public partial class IDEControl : UserControl
    {
        private UserDialog dialog;

        public IDEControl()
        {
            InitializeComponent();
            LogControl lc = new LogControl();
            Grid.SetRow(lc,2);
            Grid.SetColumnSpan(lc,2);
            chatTab.Content = new ChatControl();
            prjTab.Content = new ProjectControl();
            LayoutRoot.Children.Add(lc);
        }

        private void AddFileClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ((ProjectControl)prjTab.Content).AddFile(null);
        }

        private void AddFolderClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ((ProjectControl)prjTab.Content).AddFolder(null);
        }

        private void LogoutClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ClientKernel, SubsystemType.Access, SubsystemType.Access,
                             AccessMessages.Logout, new object[] { }));
        }

        private void ExitClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void CreateProjectClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            dialog = new UserDialog("Создать проект", "Введите имя проекта");
            dialog.Closed += dialog_Closed;
            dialog.Show();
        }

        void dialog_Closed(object sender, EventArgs e)
        {
            if (dialog.DialogResult == true)
            {
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.CreateProject, new object[] { dialog.inputBox.Text }));
            }
        }
    }
}
