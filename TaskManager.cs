using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{
    //Declares the everything to do with a task as well methods
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

        //Method for adding a task
        public void AddTask(TaskItem task)
        {
            tasks.Add(task);
        }

        //Mehtod to retrieve tasks
        public List<TaskItem> GetTasks()
        {
            return tasks;
        }

        //Method to delete a task
        public void DeleteTask(TaskItem task)
        {
            tasks.Remove(task);
        }

        //Method to mark a task as complete
        public void MarkAsCompleted(TaskItem task)
        {
            task.IsCompleted = true;
        }
    }
}