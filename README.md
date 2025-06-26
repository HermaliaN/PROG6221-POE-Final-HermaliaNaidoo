
# Cybersecurity ChatBot WPF

PROG6221 Final POE


## Project Description
This project is a **GUI-based Cybersecurity Awareness Chatbot** built using **WPF and C#**. It’s designed to educate users about cybersecurity through chatbot conversations, task management, and interactive quizzes.

## Key Features
- **Smart Chatbot**: Responds to user queries about topics like phishing, password safety, and scams using keyword detection and basic NLP.
- **Task Assistant**: Users can add cybersecurity-related tasks (e.g., "Enable 2FA") with optional reminders.
- **Cybersecurity Quiz**: A fun 10-question quiz that tests the user’s knowledge with immediate feedback and scoring.
- **NLP Simulation**: Recognizes varied user commands like "Remind me to update my password tomorrow."
- **Activity Log**: Displays a history of key actions like tasks, reminders, and quiz attempts in a neat, timestamped list.

### Features from Part 2
- Delegate Pattern Implementation: Event-driven response handling using C# delegates
- Memory System: Remembers user interests and preferences throughout the session
- Sentiment Analysis: Detects user emotions (worried, frustrated, curious) and responds appropriately
- Follow-up Question Support: Handles additional queries on the same topic

### Supported topics
- Password security
- Phishing
- Safe web browsing
- Privacy
- Scam recognition
- General cybersecurity awareness

## Getting Started

### Prerequisites:

- Visual Studio 2019 or later
- .NET Framework 4.7.2 or .NET Core 3.1+

### Setup Instructions:

1. Clone the repository.
2. Open the solution in **Visual Studio**.
3. Build the project.
4. Run the application.
5. Interact with the bot and try features like:
   - `Add task - Enable two-factor authentication`
   - `Remind me to update my password tomorrow`
   - `Start Quiz`
   - `What have you done for me?`
## Project Structure
CyberBotWPF_Final:
- MainWindow.xaml / MainWindow.xaml.cs - Main chatbot interface
- QuizWindow.xaml / QuizWindow.xaml.cs - Cybersecurity quiz game
- ViewTasksWindow.xaml / ViewTasksWindow.xaml.cs - View task screen
- ViewActivityLogWindow.xaml / .cs - Activity log screen

Models:
- TaskItem.cs
- User.cs
- QuizQuestion.cs

Logic:
- Bot.cs - Core chatbot logic (Part 1 and 2)
- TaskManager.cs - Manages task creation/deletion/reminders
- QuizManager.cs - Stores quiz questions and manages
- SentimentAnalysis.cs - Simple sentiment detection (Part 2)
- MemoryManager.cs - Stores users history
- NLPProcessor.cs - NLP recognition
- ActivityLog.cs - Logs significant user actions
  
## License

This project is for academic and educational purposes only.

## References 
Simplilearn, 2020. Natural Language Processing In 5 Minutes | What Is NLP And How Does It Work? [video online] Available at: <https://youtu.be/CMrHM8a3hqw?si=Vf_4yx27KqfegK9Y> [Accessed 24 June 2025].

Bunny Labs, Tokenization Explained | Text Processing | Bunny Labs | LLM | NLU | NLP | Text. [video online] Available at: https://youtu.be/f0FuRvyTrLw?si=5r-Lq7XN2lYJmw0U [Accessed 24 June 2025].

Microsoft Learn, 2025. Create a WPF app in Visual Studio. [online] Available at: <https://learn.microsoft.com/en-us/dotnet/desktop/wpf/get-started/create-app-visual-studio> [Accessed 24 June 2025].

## YouTube Video with Code Walkthrough & Demonstration
https://youtu.be/jyQYZm6SR0U
IIEVC School of Computer Science, 2021. PROG6221 - Building a WPF Application (with binding). [video online] Available at: <https://youtu.be/NZmaU4n7Hdo?si=0aizzCJyAUHkavka> [Accessed 24 June 2025].

IIEVC School of Computer Science, 2025.CI Pipeline Integration in Visual Studio. [video online] Available at: <https://youtu.be/KBN9OoTxspw?si=fgYTlm-Da_5dTiYd> [Accessed 24 June 2025].

OpenAI. 2025. ChatGPT(Version 4). [Large language model]. Available at: https://chatgpt.com/c/68050ee0-2cd0-8002-8684-bae79e6d18f9[Accessed: 24 June 2025].
