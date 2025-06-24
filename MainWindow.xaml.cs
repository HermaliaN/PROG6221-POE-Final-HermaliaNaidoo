using System;
using System.Windows;
using System.Windows.Controls;

namespace CyberBotWPF_Final
{
    public partial class MainWindow : Window
    {
        private User user;
        private Bot bot;
        private TaskManager taskManager = new TaskManager();
        private NLPProcessor nlpProcessor;

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

            nlpProcessor = new NLPProcessor(taskManager, DisplayBotMessage, user);

            DisplayBotMessage("", $"Nice to meet you, {user.Name}! You can ask me questions about cybersecurity or say things like 'Remind me to update my password tomorrow'.");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            DisplayUserMessage(input);
            UserInputBox.Clear();

            // Exit handling
            if (input.ToLower().Contains("exit"))
            {
                DisplayBotMessage("", "Goodbye! Stay safe online.");
                Application.Current.Shutdown();
                return;
            }

            // NLP-based interpretation
            if (nlpProcessor.Process(input)) return;

            // Generic keyword-based responses
            bot.Respond(input);
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