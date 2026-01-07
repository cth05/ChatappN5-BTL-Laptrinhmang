using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_app
{
    public class ChatMessage
    {
        public string type { get; set; }
        public User from { get; set; }
        public User to { get; set; }
        public string message { get; set; }
        public string note { get; set; }
        public DateTime timestamp { get; set; } = DateTime.UtcNow;
    }
    public class AuthMessage
    {
        public string type { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string avatarencoded { get; set; }
        public string pubkeyrsa { get; set; }
    }
    public class User
    {
        public string name { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string avatarencoded { get; set; }
        public string ipaddress { get; set; }
        public int udpport { get; set; }
        public string pubkeyrsa { get; set; }
    }
}
