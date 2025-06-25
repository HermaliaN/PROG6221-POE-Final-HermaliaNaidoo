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
        
        //updated click to ensure that if answer is incorrect then the user is notified
        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (OptionsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            bool hasNext = quizManager.SubmitAnswer(OptionsListBox.SelectedIndex, out string explanation, out bool correct);

            // Add feedback prefix based on correctness
            if (correct)
            {
                FeedbackTextBlock.Text = $"Correct!\n{explanation}";
            }
            else
            {
                // Get the correct answer text from the question
                var currentQuestion = quizManager.GetLastQuestion(); // We'll write this helper next
                string correctAnswer = currentQuestion.Options[currentQuestion.CorrectOptionIndex];

                FeedbackTextBlock.Text = $"Incorrect. The correct answer is: \"{correctAnswer}\"\n{explanation}";
            }

            if (hasNext)
            {
                Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Delay(2000); //waits a little longer before loading next question
                    LoadQuestion();
                });
            }
            else
            {
                MessageBox.Show($"Quiz Complete!\n\nScore: {quizManager.Score}/{quizManager.TotalQuestions}\n" +
                    (quizManager.Score >= 8 ? "Great job! You're a cybersecurity pro!" : "Keep learning to stay safe online!"));

                ActivityLog.Log($"Quiz completed - Score: {quizManager.Score}/10");

                this.Close();
            }
        }
    }
}
