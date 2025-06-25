using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{

    //All NLP is done by method of tokenizing (breaking sentences in this case input into words)
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

            //Pick up task reminder
            if (input.Contains("remind me to") || input.Contains("can you remind me"))
            {
                string taskTitle = ExtractAfter(input, "remind me to");
                if (string.IsNullOrWhiteSpace(taskTitle))
                    taskTitle = ExtractAfter(input, "can you remind me to");


                //If the user hasn't said what the task is 
                if (string.IsNullOrWhiteSpace(taskTitle))
                {
                    displayBotMessage("", "Please specify what you'd like me to remind you about.");
                    return true;
                }

                //Displays a separate input box for the user to say how many days from today they need to be reminded
                string reminderDays = Microsoft.VisualBasic.Interaction.InputBox("How many days from now should I remind you?", "Reminder Setup");
                DateTime? reminderDate = null;
                if (int.TryParse(reminderDays, out int days))
                    reminderDate = DateTime.Now.AddDays(days);

                var task = new TaskItem
                {
                    Title = taskTitle.TrimEnd('.'),
                    Description = "Reminder for task set via natural language.",
                    ReminderDate = reminderDate,
                    IsCompleted = false
                };

                taskManager.AddTask(task);
                displayBotMessage("", $"Reminder set for '{task.Title}'{(reminderDate.HasValue ? $" on {reminderDate.Value.ToShortDateString()}." : ".")}");

                //Log that a reminder has been set
                ActivityLog.Log($"Reminder set for '{task.Title}'{(reminderDate.HasValue ? $" on { reminderDate.Value.ToShortDateString()}." : ".")}");
                
                return true;
            }

            //Task 1: Add Task 
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

                //Logs that a task has been added
                ActivityLog.Log($"Task added: '{task.Title}'{(task.ReminderDate.HasValue ? $" (Reminder set for {task.ReminderDate.Value.ToShortDateString()})" : "")}");

                return true;
            }

         
            //Task 4: View activity log
            if (input.Contains("what have you done for me") || input.Contains("show activity log"))
            {
                ViewActivityLogWindow logWindow = new ViewActivityLogWindow(); 
                logWindow.ShowDialog();
                return true;
            }

            //Logs that NLP was triggered
            ActivityLog.Log("NLP triggered for processing.");

            return false;
        }

        //Helper method for extrating words/phrases (tokenizing)
        private string ExtractAfter(string input, string phrase)
        {
            int index = input.IndexOf(phrase);
            if (index == -1) return "";
            return input.Substring(index + phrase.Length).Trim();
        }
    }
}

/* References 
* Simplilearn, 2020. Natural Language Processing In 5 Minutes | What Is NLP And How Does It Work? [video online] Available at: <https://youtu.be/CMrHM8a3hqw?si=Vf_4yx27KqfegK9Y> [Accessed 24 June 2025].
* Bunny Labs, Tokenization Explained | Text Processing | Bunny Labs | LLM | NLU | NLP | Text. [video online] Available at: https://youtu.be/f0FuRvyTrLw?si=5r-Lq7XN2lYJmw0U [Accessed 24 June 2025].
*/



