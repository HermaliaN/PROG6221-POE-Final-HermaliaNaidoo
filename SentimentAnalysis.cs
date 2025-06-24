using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{
    public class SentimentAnalysis
    {

        //dictionary to store different sentiments and the responses to them
        private static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>
        {
            { "worried", "It's completely understandable to feel that way. Let me help you with some tips to stay safe." },
            { "frustrated", "Cybersecurity can be confusing sometimes. I'm here to guide you through it!" },
            { "curious", "Curiosity is a great start! Let’s explore cybersecurity together." }
        };

        //method used to select sentiments
        public static string DetectSentiment(string input)
        {
            input = input.ToLower();
            foreach (var sentiment in sentimentResponses.Keys)
            {
                if (input.Contains(sentiment))
                {
                    return sentimentResponses[sentiment];
                }
            }
            return null;
        }
    }
}
