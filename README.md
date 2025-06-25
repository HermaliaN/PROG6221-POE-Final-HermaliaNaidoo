
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
