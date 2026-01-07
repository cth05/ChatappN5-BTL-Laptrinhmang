using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_app
{
    public class User : AntdUI.NotifyProperty
    {
        public int id { get; set; }

        bool _check;
        public bool check
        {
            get => _check;
            set { _check = value; OnPropertyChanged(); }
        }

        string _email;
        public string email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        string _name;
        public string name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public string publicKeyRSA { get; set; }
        string _password;
        public string password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        string _avatarEncoded;
        public string avatarencoded
        {
            get => _avatarEncoded;
            set { _avatarEncoded = value; OnPropertyChanged(); }
        }
        public TcpClient _socket;
        public TcpClient socket
        {
            get => _socket;
            set
            {
                _socket = value;
                if (value != null)
                {
                    IPEndPoint remoteEndPoint = _socket.Client.RemoteEndPoint as IPEndPoint;
                    this.ipAddress = remoteEndPoint.Address.ToString();

                }
                OnPropertyChanged(nameof(status));
            }
        }
        public AntdUI.CellBadge status
        {
            get
            {
                if (_socket != null && _socket.Connected)
                    return new AntdUI.CellBadge(AntdUI.TState.Success, "Online");

                return new AntdUI.CellBadge(AntdUI.TState.Error, "Offline");
            }
        }
        AntdUI.CellLink[] _btns;
        public AntdUI.CellLink[] btns
        {
            get => _btns;
            set { _btns = value; OnPropertyChanged(); }
        }
        public string sessionid { get; set; }
        public string ipAddress { get; set; }
        public bool IsOnline()
        {
            return (socket != null && sessionid != null && _socket.Connected);
        }
    }
    public class PublicUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string avatarencoded { get; set; }
        public string ipAddress { get; set; }
        public string pubkeyrsa { get; set; }
    }
    public class AuthMessage
    {
        public string type { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string avatarencoded { get; set; }
        public int id { get; set; }
        public string pubkeyrsa { get; set; }
    }
    public class AuthResponse 
    { 
        public string status { get; set; }
        public string message { get; set; }
        public AuthMessage data { get; set; }
    }
    public class ChatMessage
    {
        public string type { get; set; }
        public PublicUser from { get; set; }
        public PublicUser to { get; set; }
        public string message { get; set; }
        public string note { get; set; }
        public DateTime timestamp { get; set; } = DateTime.UtcNow;
    }
}
