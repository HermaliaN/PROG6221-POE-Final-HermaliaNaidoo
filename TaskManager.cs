using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TaskManager
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        public void AddTask(TaskItem task)
        {
            tasks.Add(task);
        }

        public List<TaskItem> GetTasks()
        {
            return tasks;
        }

        public void DeleteTask(TaskItem task)
        {
            tasks.Remove(task);
        }

        public void MarkAsCompleted(TaskItem task)
        {
            task.IsCompleted = true;
        }
    }
}