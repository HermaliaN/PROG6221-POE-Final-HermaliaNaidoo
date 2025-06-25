using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{
    //Declares the logs dictionary
    public class ActivityLog
    {
        private static readonly List<string> logs = new List<string>();

        public static void Log(string message)
        {
            string entry = $"{DateTime.Now:HH:mm} - {message}";
            logs.Add(entry);
        }

        //Gets the 10 most recent logs
        public static List<string> GetRecentLogs(int count = 10)
        {
            int start = Math.Max(0, logs.Count - count);
            return logs.GetRange(start, logs.Count - start);
        }
    }
}
