using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{
    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int currentIndex = 0;
        public int Score { get; private set; } = 0;

        public QuizManager()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            //Dictionary to hold all the questions, answers and feedback
            questions = new List<QuizQuestion>
            {
                new QuizQuestion("What should you do if you receive an email asking for your password?",
                    new List<string> { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    2, "Reporting phishing emails helps prevent scams."),

                new QuizQuestion("True or False: You should use the same password for multiple accounts.",
                    new List<string> { "True", "False" },
                    1, "Using different passwords improves security."),

                new QuizQuestion("What is a common sign of a phishing attempt?",
                    new List<string> { "An email with urgent language", "An email from a friend", "A newsletter", "A software update" },
                    0, "Scammers often try to pressure you into acting fast."),

                new QuizQuestion("What does 2FA stand for?",
                    new List<string> { "Two-Factor Authentication", "Two-Factor Authorization", "Twice Fast Access", "Two-Firewall Access" },
                    0, "2FA adds a second layer of login security."),

                new QuizQuestion("Which of these is the safest type of password?",
                    new List<string> { "Your pet's name", "123456", "A long string of random characters", "Your birthday" },
                    2, "Randomized strings are much harder to guess."),

                new QuizQuestion("True or False: HTTPS means the website is secure.",
                    new List<string> { "True", "False" },
                    0, "HTTPS ensures the data is encrypted."),

                new QuizQuestion("What should you check before clicking a link in an email?",
                    new List<string> { "The color of the link", "If it looks cool", "The sender and link address", "How long the link is" },
                    2, "Always verify who sent it and where it leads."),

                new QuizQuestion("Which is NOT a good cybersecurity practice?",
                    new List<string> { "Sharing your password with a friend", "Using a password manager", "Keeping software updated", "Using 2FA" },
                    0, "Never share passwords with anyone."),

                new QuizQuestion("What is social engineering?",
                    new List<string> { "Hacking software", "Designing websites", "Manipulating people to give information", "Building secure networks" },
                    2, "It’s about tricking people, not machines."),

                new QuizQuestion("What should you do if your account is hacked?",
                    new List<string> { "Tell no one", "Use the account as usual", "Change your password and report it", "Create a new account and ignore it" },
                    2, "Always change your credentials and notify the provider.")
            };
        }

        public QuizQuestion GetCurrentQuestion() => questions[currentIndex];

        //"Submits" the answer from the user, adds to score if correct and gives an explanation
        public bool SubmitAnswer(int selectedIndex, out string explanation, out bool isCorrect)
        {
            var q = questions[currentIndex];
            isCorrect = selectedIndex == q.CorrectOptionIndex;

            if (isCorrect) Score++;
            
            explanation = q.Explanation;
            
            currentIndex++;
            return currentIndex < questions.Count;
        }

        //Determines how many questions the user has answered 
        public int TotalQuestions => questions.Count;

        public QuizQuestion GetLastQuestion()
        {
            int lastIndex = Math.Max(0, currentIndex - 1);
            return questions[lastIndex];
        }
    }

 }

/* References
* OpenAI. 2025. ChatGPT(Version 4). [Large language model]. Available at: https://chatgpt.com/c/68050ee0-2cd0-8002-8684-bae79e6d18f9[Accessed: 24 June 2025].
*
* Used ChatGPT to generate the 10 questions and answers that are seen in the dictionary at the top of the class
*/