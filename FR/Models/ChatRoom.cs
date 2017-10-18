using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR
{
    public class ChatRoom
    {
        public int ID { get; private set;  }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Type { get; private set; }
        public string Password { get; private set; }

        public ChatRoom(int id, string name, string description, int type, string password)
        {
            ID = id;
            Name = name;
            Description = description;
            Type = type;
            Password = password;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
