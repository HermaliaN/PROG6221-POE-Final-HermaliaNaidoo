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

        //Performed major updates to this method due to logical error found in testing
        public bool Process(string input)
        {
            input = input.ToLower();

            // Log that NLP is in use
            ActivityLog.Log("NLP triggered for processing.");

            //Reminder-based input
            if (input.Contains("remind me") || input.Contains("can you remind me"))
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
                    Description = "Reminder for task set via natural language.",
                    ReminderDate = reminderDate,
                    IsCompleted = false
                };

                taskManager.AddTask(task);

                string confirmation = $"Reminder set for '{task.Title}'{(reminderDate.HasValue ? $" on {reminderDate.Value.ToShortDateString()}." : ".")}";
                displayBotMessage("", confirmation);
                ActivityLog.Log(confirmation);

                return true;
            }

            //Task creation input
            if (
                input.Contains("add task") || input.Contains("add a task") ||
                input.Contains("set a reminder for") || input.Contains("add task to")
            )
            {
                string taskTitle = ExtractAfter(input, "add a task to");
                if (string.IsNullOrWhiteSpace(taskTitle))
                    taskTitle = ExtractAfter(input, "set a reminder for");
                if (string.IsNullOrWhiteSpace(taskTitle))
                    taskTitle = ExtractAfter(input, "add task");
                if (string.IsNullOrWhiteSpace(taskTitle))
                    taskTitle = ExtractAfter(input, "add a task");

                if (string.IsNullOrWhiteSpace(taskTitle))
                {
                    displayBotMessage("", "Please specify what the task is.");
                    return true;
                }

                string description = Microsoft.VisualBasic.Interaction.InputBox($"Enter a description for '{taskTitle}'", "Task Description");
                string reminderDays = Microsoft.VisualBasic.Interaction.InputBox("Do you want a reminder? Enter number of days from now or leave blank:", "Reminder (Optional)");

                DateTime? reminderDate = null;
                if (int.TryParse(reminderDays, out int days))
                    reminderDate = DateTime.Now.AddDays(days);

                var task = new TaskItem
                {
                    Title = taskTitle.TrimEnd('.'),
                    Description = string.IsNullOrWhiteSpace(description) ? "No description provided." : description,
                    ReminderDate = reminderDate,
                    IsCompleted = false
                };

                taskManager.AddTask(task);

                string confirmation = $"Task added: '{task.Title}'." +
                                      (reminderDate.HasValue ? $" I’ll remind you on {reminderDate.Value.ToShortDateString()}." : "");
                displayBotMessage("", confirmation);
                ActivityLog.Log(confirmation);

                return true;
            }

            //Activity Log Request
            if (input.Contains("what have you done for me") || input.Contains("show activity log") || input.Contains("activity log"))
            {
                ViewActivityLogWindow logWindow = new ViewActivityLogWindow();
                logWindow.ShowDialog();
                ActivityLog.Log("Viewed activity log via NLP.");
                return true;
            }

            // No recognized NLP command
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



