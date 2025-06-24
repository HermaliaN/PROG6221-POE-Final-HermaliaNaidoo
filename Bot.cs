using System;
using System.Collections.Generic;
using System.Linq;
using CyberBotWPF_Final;

namespace CyberBotWPF_Final
{
    public class Bot
    {
        private string currentTopic = null;
        private Random random = new Random();
        private User user;
        private MemoryManager memory = new MemoryManager();

        public delegate void KeywordResponseHandler(string keyword, string response);
        public event KeywordResponseHandler OnKeywordResponse;

        //added new delegate to handle normal responses (now allows for reponse to be displayed on WPF and not in console)
        public delegate void SimpleResponseHandler(string response);
        public event SimpleResponseHandler OnSimpleResponse;

        public Bot(User user)
        {
            this.user = user;
            OnKeywordResponse += HandleKeywordResponse;
        }

        private Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>
        {
            { "password", new List<string> {
                 "Use strong, unique passwords for each account.",
                 "Avoid using personal information like birthdays in your passwords.",
                 "Consider using a password manager to generate and store passwords securely."
             }},
            { "scam", new List<string> {
                "Scams often come through email or social media. Be cautious of unsolicited messages.",
                "Never share personal details or OTPs with unknown contacts.",
                "When in doubt, verify through official channels before acting."
            }},
            { "privacy", new List<string> {
                "Check app permissions regularly and remove those that are not needed.",
                "Avoid oversharing on social media platforms.",
                "Use privacy-focused browsers and enable tracking protection features."
            }},
            { "how are you", new List<string> {
                "I'm doing well, I hope you are too!",
                "Ready to chat cybersecurity with you!",
                "Feeling great, can't wait to chat with you!"
            }},
            { "purpose", new List<string> {
                "I'm here to help spread awareness about cybersecurity by educating users.",
                "My mission is to keep you informed and safe from cyber threats.",
                "Cybersecurity matters! I'm here to guide you through it."
            }},
            { "phishing", new List<string> {
                "Phishing is an attack often carried out via email. Beware of suspicious links from unknown senders.",
                "Always double-check the sender's email address. Fraudulent ones often look similar to legitimate addresses.",
                "If an email pressures you to act urgently, be cautious—it may be a phishing attempt."
            }},
            { "safe browsing", new List<string> {
                "Install a firewall or antivirus extension. Look for 'https' in the URL.",
                "Avoid downloading files from untrusted sites.",
                "Consider using a browser with privacy protection, like Firefox or McAfee."
            }},
            { "what can i ask", new List<string> {
                "Ask me about my purpose, passwords, phishing, privacy, or scams."
            }},
            { "exit", new List<string> {
                "Goodbye!" }
            }
        };

        private void HandleKeywordResponse(string keyword, string response)
        {
            OnSimpleResponse?.Invoke($"CBot: {response}");

            if (memory.HasMemory("favouriteTopic") && keyword.Contains(memory.Recall("favouriteTopic").ToLower()))
            {
                OnSimpleResponse?.Invoke($"CBot: Since you're interested in {memory.Recall("favouriteTopic")}, this might be especially useful to you!");
            }
        }

        private bool IsFollowUp(string input)
        {
            string[] followUps = {
                "tell me more", "what else", "more info",
                "can you explain more", "what should i know", "why is that important"
            };

            return followUps.Any(phrase => input.ToLower().Contains(phrase));
        }

        public void Respond(string input)
        {
            try
            {
                string sentimentResponse = SentimentAnalysis.DetectSentiment(input);
                if (sentimentResponse != null)
                {
                    OnSimpleResponse?.Invoke($"CBot: {sentimentResponse}");
                    return;
                }

                if (input.Contains("i'm interested in") || input.Contains("i am interested in"))
                {
                    string[] words = input.Split(' ');
                    int topicIndex = Array.IndexOf(words, "in");
                    if (topicIndex != -1 && topicIndex + 1 < words.Length)
                    {
                        string favTopic = words[topicIndex + 1];
                        memory.Remember("favouriteTopic", favTopic);

                        OnSimpleResponse?.Invoke($"CBot: Great! I'll remember that you're interested in {favTopic}. It's a crucial part of staying safe online.");
                        return;
                    }
                }

                if (IsFollowUp(input) && currentTopic != null && responses.ContainsKey(currentTopic))
                {
                    var followUpResponses = responses[currentTopic];
                    string followUp = followUpResponses[random.Next(followUpResponses.Count)];
                    OnKeywordResponse?.Invoke(currentTopic, followUp);
                    return;
                }

                foreach (var keyword in responses.Keys)
                {
                    if (input.ToLower().Contains(keyword))
                    {
                        var possibleResponses = responses[keyword];
                        string chosenResponse = possibleResponses[random.Next(possibleResponses.Count)];

                        OnKeywordResponse?.Invoke(keyword, chosenResponse);
                        currentTopic = keyword;
                        return;
                    }
                }

                OnSimpleResponse?.Invoke("CBot: Sorry, I could not understand that. Please try rephrasing your question.");
            }
            catch (Exception ex)
            {
                OnSimpleResponse?.Invoke($"CBot: Oops! Something went wrong. [Error: {ex.Message}]");
            }
        }
    }
}
