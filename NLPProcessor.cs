using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{
    public class NLPProcessor
    {
        private TaskManager taskManager;
        private Action<string, string> displayBotMessage;
        private User user;

        public NLPProcessor(TaskManager taskManager, Action<string, string> displayBotMessage, User user)
        {
            this.taskManager = taskManager;
            this.displayBotMessage = displayBotMessage;
            this.user = user;
        }

        public bool Process(string input)
        {
            input = input.ToLower();

            if (input.Contains("remind me to") || input.Contains("can you remind me"))
            {
                string taskTitle = ExtractAfter(input, "remind me to");
                if (string.IsNullOrWhiteSpace(taskTitle))
                    taskTitle = ExtractAfter(input, "can you remind me to");

                if (string.IsNullOrWhiteSpace(taskTitle))
                {
                    displayBotMessage("", "Please specify what you'd like me to remind you about.");
                    return true;
                }

                string reminderDays = Microsoft.VisualBasic.Interaction.InputBox("How many days from now should I remind you?", "Reminder Setup");
                DateTime? reminderDate = null;
                if (int.TryParse(reminderDays, out int days))
                    reminderDate = DateTime.Now.AddDays(days);

                var task = new TaskItem
                {
                    Title = taskTitle.TrimEnd('.'),
                    Description = "Reminder task set via natural language.",
                    ReminderDate = reminderDate,
                    IsCompleted = false
                };

                taskManager.AddTask(task);
                displayBotMessage("", $"Reminder set for '{task.Title}'{(reminderDate.HasValue ? $" on {reminderDate.Value.ToShortDateString()}." : ".")}");
                return true;
            }

            if (input.Contains("add a task to") || input.Contains("set a reminder for"))
            {
                string taskTitle = ExtractAfter(input, "add a task to");
                if (string.IsNullOrWhiteSpace(taskTitle))
                    taskTitle = ExtractAfter(input, "set a reminder for");

                string description = Microsoft.VisualBasic.Interaction.InputBox($"Enter a description for '{taskTitle}'", "Task Description");
                string reminderDays = Microsoft.VisualBasic.Interaction.InputBox("Do you want a reminder? Enter number of days from now or leave blank:", "Reminder (Optional)");

                DateTime? reminderDate = null;
                if (int.TryParse(reminderDays, out int days))
                    reminderDate = DateTime.Now.AddDays(days);

                var task = new TaskItem
                {
                    Title = taskTitle.TrimEnd('.'),
                    Description = description,
                    ReminderDate = reminderDate,
                    IsCompleted = false
                };

                taskManager.AddTask(task);
                displayBotMessage("", $"Task added: '{task.Title}'.{(reminderDate.HasValue ? $" I’ll remind you on {reminderDate.Value.ToShortDateString()}." : "")}");
                return true;
            }

            //To be implemented in task 4
            //if (input.Contains("what have you done for me") || input.Contains("show activity log"))
            //{
             //   ActivityLogWindow logWindow = new ActivityLogWindow(); // Task 4
              //  logWindow.ShowDialog();
             //   return true;
            //}

            return false;
        }

        private string ExtractAfter(string input, string phrase)
        {
            int index = input.IndexOf(phrase);
            if (index == -1) return "";
            return input.Substring(index + phrase.Length).Trim();
        }
    }
}

