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

namespace IDEClient.Core.Controls
{
    public partial class LogControl : UserControl
    {
        private ObservableCollection<UserlogDto> _logs = new ObservableCollection<UserlogDto>();
        public LogControl()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            try
            {
                var subs = Kernel.GetKernel.Subsystems.Where(x => x.Type() == SubsystemType.Report);
                if (subs.Count() != 0)
                    ((ReportSubsystem)subs.First()).logControl = this;
                Kernel.GetKernel.SendToServer(new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.Report,
                                                                 SubsystemType.Report, ReportMessages.GetLastUserLog,
                                                                 new object[] { }));
                logGrid.ItemsSource = _logs;
                _logs.CollectionChanged +=
                    new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_logs_CollectionChanged);
            }
            catch
            {
            }
        }

        void _logs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            logGrid.ScrollIntoView(_logs[_logs.Count-1], logGrid.Columns[0]);
        }
        public delegate void IncomingMessage(ServiceMessage msg);
        public void SendMessage(ServiceMessage msg)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new IncomingMessage(SendMessage), msg);
                return;
            }
            if((ReportMessages)msg.Type == ReportMessages.GetLastUserLog)
            {
                var res = (List<UserlogDto>)msg.Message[0];
                foreach(var log in res)
                {
                    _logs.Add(log);
                }
            } 
        }
    }
}
