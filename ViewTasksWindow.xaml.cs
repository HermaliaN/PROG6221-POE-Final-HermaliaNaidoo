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
   
    public partial class ViewTasksWindow : Window
    {
        private TaskManager taskManager;

        public ViewTasksWindow(TaskManager manager)
        {
            InitializeComponent();
            taskManager = manager;
            LoadTasks();
        }

        private void LoadTasks()
        {
            TaskListView.ItemsSource = null;
            TaskListView.ItemsSource = taskManager.GetTasks();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is TaskItem selectedTask)
            {
                taskManager.DeleteTask(selectedTask);
                LoadTasks();
            }
        }

        private void MarkComplete_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is TaskItem selectedTask)
            {
                taskManager.MarkAsCompleted(selectedTask);
                LoadTasks();
            }
        }
    }
}
