using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberBotWPF_Final
{
   
    public partial class MainWindow : Window
    {
        private User user;
        private Bot bot;
        private TaskManager taskManager = new TaskManager();

        public MainWindow()
        {
            InitializeComponent();
            StartChat();
        }

        private void StartChat()
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Welcome to the Cybersecurity Bot! What is your name?", "CBot");
            if (string.IsNullOrWhiteSpace(name)) name = "User";

            user = new User(name);
            bot = new Bot(user);
            bot.OnKeywordResponse += DisplayBotMessage;

            DisplayBotMessage("", $"Nice to meet you, {user.Name}! You can ask me questions about cybersecurity or type 'Add task: your task here' to start managing your tasks.");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            DisplayUserMessage(input);
            UserInputBox.Clear();

            //exit handling
            if (input.ToLower().Contains("exit"))
            {
                DisplayBotMessage("", "Goodbye! Stay safe online.");
                Application.Current.Shutdown();
                return;
            }

            //NLP - Add task detection
            if (input.ToLower().StartsWith("add task:") || input.ToLower().StartsWith("add task -"))
            {
                HandleAddTask(input);
                return;
            }

            //general chatbot interaction
            bot.Respond(input);
        }

        private void HandleAddTask(string input)
        {
            string[] parts = input.Split(new[] { "add task:" }, StringSplitOptions.RemoveEmptyEntries);
            string taskTitle = input.Substring(input.IndexOf("add task", StringComparison.OrdinalIgnoreCase) + 9).TrimStart(':', '-', ' ');


            if (string.IsNullOrWhiteSpace(taskTitle))
            {
                DisplayBotMessage("","Please provide a task title after 'Add task:'");
                return;
            }

            string description = Microsoft.VisualBasic.Interaction.InputBox($"Enter a description for the task '{taskTitle}':", "Task Description");
            string reminderInput = Microsoft.VisualBasic.Interaction.InputBox("Would you like to set a reminder? Enter number of days from now or leave blank:", "Reminder (Optional)");

            DateTime? reminderDate = null;
            if (int.TryParse(reminderInput, out int days))
            {
                reminderDate = DateTime.Now.AddDays(days);
            }

            TaskItem task = new TaskItem
            {
                Title = taskTitle,
                Description = description,
                ReminderDate = reminderDate,
                IsCompleted = false
            };

            taskManager.AddTask(task);
            DisplayBotMessage("",$"Task added: '{task.Title}'.{(reminderDate.HasValue ? $" I’ll remind you on {reminderDate.Value.ToShortDateString()}." : "")}");
        }

        private void DisplayUserMessage(string message)
        {
            ChatPanel.Children.Add(new TextBlock
            {
                Text = $"{user.Name}: {message}",
                Margin = new Thickness(0, 5, 0, 5),
                Foreground = System.Windows.Media.Brushes.Cyan
            });
        }

        private void DisplayBotMessage(string _, string message)
        {
            ChatPanel.Children.Add(new TextBlock
            {
                Text = $"CBot: {message}",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 5, 0, 5),
                Foreground = System.Windows.Media.Brushes.Yellow
            });
        }

        private void ViewTasks_Click(object sender, RoutedEventArgs e)
        {
            ViewTasksWindow taskWindow = new ViewTasksWindow(taskManager);
            taskWindow.ShowDialog();
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            QuizWindow quizWindow = new QuizWindow();
            quizWindow.ShowDialog();
        }
    }
}