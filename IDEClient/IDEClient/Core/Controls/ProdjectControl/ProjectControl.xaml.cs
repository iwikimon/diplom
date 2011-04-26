using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IDEClient.Core.Subsystems;
using IDEService.Core;
using Telerik.Windows.Controls;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;


namespace IDEClient.Core.Controls
{
    public partial class ProjectControl : UserControl
    {
        public Elem CurrElem { get; private set; }
        public Elem Structure { get; private set; }
        
        private UserDialog _questionDialog;
        private ConfirmDialog _confirmDialog;

        public ProjectControl()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            try
            {
                var subs = Kernel.GetKernel.Subsystems.Where(x => x.Type() == SubsystemType.Project);
                if (subs.Count() != 0)
                    ((ProjectSubsystem)subs.First()).control = this;
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.GetProjectList, new object[] { }));
            }
            catch
            {
                
            }
        }

        public delegate void IncomingMessage(ServiceMessage msg);

        public void SendMessage(ServiceMessage msg)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new IncomingMessage(SendMessage), msg);
                return;
            }
            switch((ProjectMessages)msg.Type)
            {
                case ProjectMessages.GetProjectList:
                    {
                        foreach (var pInfo in (List<ProjectInfo>) msg.Message[0])
                        {
                            prjList.Items.Add(pInfo.Name);
                        }
                        break;
                    }
                case ProjectMessages.GetStructure:
                    {
                        var structure = (ProjectStructure)msg.Message[0];
                        var elem = Elem.Build(structure);
                        Structure = elem;
                        project.ItemsSource = new List<Elem>() {elem};
                        return;
                        
                    }
            }
            return;
        }

        private void prjList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pname = prjList.SelectedItem.ToString();   
            Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel,SubsystemType.Project,SubsystemType.Project,ProjectMessages.GetStructure, new object[]{pname}));
        }


        private void RadContextMenu_Opened(object sender, RoutedEventArgs e)
		{
			var item = cMenu.GetClickedElement<RadTreeViewItem>();
            if (item != null)
			{
                CurrElem = (Elem)item.Item;
                if (CurrElem.Type == ElemType.Folder)
                {
                    CurrElem = (Elem) item.Item;
                    cMenu.ItemsSource = folderMenu.Items;
                }
                else
                    cMenu.ItemsSource = fileMenu.Items;
			}
			else
			{
			    cMenu.Visibility = System.Windows.Visibility.Collapsed;
			}
        }

        

        private void addFile(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            AddFile(CurrElem);
        }

        /// <summary>
        /// Добавление файла
        /// </summary>
        /// <param name="elem">Элемент, в который добавляем. Если null - добавление происходит в корень</param>
        public void AddFile(Elem elem)
        {
            if (elem == null)
                CurrElem = Structure;
            else
                CurrElem = elem;
            _questionDialog = new UserDialog("Добавление файла", "Введите имя файла");
            _questionDialog.Closed += fileAdded;
            _questionDialog.Show();
        }

        void fileAdded(object sender, EventArgs e)
        {
            if (_questionDialog.DialogResult == true)
            {
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project, 
                    SubsystemType.Project, ProjectMessages.AddFile, new object[] { CurrElem.Folder, new FileDto() 
                    { Name = _questionDialog.inputBox.Text, Path = CurrElem.Folder.Path+ "\\"+CurrElem.Folder.Name } })); var pname = prjList.SelectedItem.ToString();
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.GetStructure, new object[] { pname }));
        
            }
        }

        private void addFolder(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            AddFolder(CurrElem);
        }

        /// <summary>
        /// Добавление папки
        /// </summary>
        /// <param name="elem">Элемент, в который добавляем. Если null - добавление происходит в корень</param>
        public void AddFolder(Elem elem)
        {
            if (elem == null)
                CurrElem = Structure;
            else
                CurrElem = elem;
            _questionDialog = new UserDialog("Добавление папки", "Введите имя папки");
            _questionDialog.Closed += folderAdded;
            _questionDialog.Show();
        }

        void folderAdded(object sender, EventArgs e)
        {
            if (_questionDialog.DialogResult == true)
            {
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project,
                    SubsystemType.Project, ProjectMessages.AddFolder, new object[] { CurrElem.Folder, new FolderDto() 
                    { Name = _questionDialog.inputBox.Text, Path = CurrElem.Folder.Path + "\\"+CurrElem.Folder.Name  } }));
                var pname = prjList.SelectedItem.ToString();
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project, SubsystemType.Project, ProjectMessages.GetStructure, new object[] { pname }));

            }
        }

        private void removeFolder(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            _confirmDialog = new ConfirmDialog("Удаление папки","Вы действительно хотите удалить папку " +CurrElem.Name +" и все её содержимое? ");
            _confirmDialog.Closed += folderRemoved;
            _confirmDialog.Show();
        }

        void folderRemoved(object sender, EventArgs e)
        {
            if (_confirmDialog.DialogResult == true)
            {
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project,
                                                                 SubsystemType.Project, ProjectMessages.RemoveFolder,
                                                                 new object[] {CurrElem.Folder}));
                var pname = prjList.SelectedItem.ToString();
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project,
                                                                 SubsystemType.Project, ProjectMessages.GetStructure,
                                                                 new object[] {pname}));
            }
        }

        private void removeFile(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            _confirmDialog = new ConfirmDialog("Удаление файла", "Вы действительно хотите удалить файл " + CurrElem.Name + "? ");
            _confirmDialog.Closed += fileRemoved;
            _confirmDialog.Show();
        }

        void fileRemoved(object sender, EventArgs e)
        {
            if (_confirmDialog.DialogResult == true)
            {
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project,
                                                                 SubsystemType.Project, ProjectMessages.RemoveFile,
                                                                 new object[] { CurrElem.Object }));
                var pname = prjList.SelectedItem.ToString();
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Project,
                                                                 SubsystemType.Project, ProjectMessages.GetStructure,
                                                                 new object[] { pname }));
            }
        }
    }

}
