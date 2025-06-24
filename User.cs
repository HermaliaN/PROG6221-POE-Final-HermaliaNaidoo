using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBotWPF_Final
{
    public class User
    {
        //stores the user details
        public string Name { get; private set; }

        public User(string name)
        {
            Name = name;
        }

    }
}
