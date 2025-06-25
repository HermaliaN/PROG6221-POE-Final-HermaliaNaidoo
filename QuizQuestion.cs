using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{
    //stores the information per individual question
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
        public string Explanation { get; set; }

        public QuizQuestion(string question, List<string> options, int correctIndex, string explanation)
        {
            QuestionText = question;
            Options = options;
            CorrectOptionIndex = correctIndex;
            Explanation = explanation;
        }
    }
}
