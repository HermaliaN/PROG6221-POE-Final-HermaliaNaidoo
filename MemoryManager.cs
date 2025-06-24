using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{
    public class MemoryManager
    {
        //stores a users history in key value pairs
        private Dictionary<string, string> history = new Dictionary<string, string>();

        //add to memory
        public void Remember(string key, string value)
        {
            history[key] = value;
        }

        //retrieve from memory
        public string Recall(string key)
        {
            return history.ContainsKey(key) ? history[key] : null;
        }

        //checks if memory exists for a key
        public bool HasMemory(string key)
        {
            return history.ContainsKey(key);
        }
    }
}
