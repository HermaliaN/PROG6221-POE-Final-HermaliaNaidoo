using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CyberBotWPF_Final
{
    
    public partial class ViewActivityLogWindow : Window
    {
        public class LogEntry
        {
            public string Time { get; set; }
            public string Message { get; set; }
        }

        public ViewActivityLogWindow()
        {
            InitializeComponent();

            var recentLogs = ActivityLog.GetRecentLogs();

            List<LogEntry> parsedLogs = new List<LogEntry>();
            foreach (var log in recentLogs)
            {
              
                //assumes format with a dash
                int dashIndex = log.IndexOf(" - ");
                if (dashIndex != -1)
                {
                    string time = log.Substring(0, dashIndex);
                    string message = log.Substring(dashIndex + 3);
                    parsedLogs.Add(new LogEntry { Time = time, Message = message });
                }
            }

            LogListView.ItemsSource = parsedLogs;
        }
    }
}
