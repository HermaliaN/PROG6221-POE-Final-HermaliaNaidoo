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
using static System.Formats.Asn1.AsnWriter;

namespace CyberBotWPF_Final
{
    
    public partial class QuizWindow : Window
    {
        private QuizManager quizManager;

        public QuizWindow()
        {
            InitializeComponent();
            quizManager = new QuizManager();
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            var question = quizManager.GetCurrentQuestion();
            QuestionTextBlock.Text = question.QuestionText;
            OptionsListBox.Items.Clear();
            FeedbackTextBlock.Text = "";

            foreach (var option in question.Options)
            {
                OptionsListBox.Items.Add(option);
            }
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (OptionsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            bool hasNext = quizManager.SubmitAnswer(OptionsListBox.SelectedIndex, out string explanation, out bool correct);
            FeedbackTextBlock.Text = explanation;

            if (hasNext)
            {
                //wait before loading next question
                Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Delay(1800);
                    LoadQuestion();
                });
            }
            else
            {
                MessageBox.Show($"Quiz Complete!\n\nScore: {quizManager.Score}/{quizManager.TotalQuestions}\n" +
                    (quizManager.Score >= 8 ? "Great job! You're a cybersecurity pro!" : "Keep learning to stay safe online!"));

                //logs that the quiz has been completed along with the score
                ActivityLog.Log($"Quiz completed - Score: {quizManager.Score}/10");

                this.Close();
            }
        }
    }
}
