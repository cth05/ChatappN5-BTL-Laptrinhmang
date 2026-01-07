using AntdUI;
using AntdUI.Chat;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_app
{
    public partial class fMain : AntdUI.Window
    {
        static string dbPath = Path.Combine(Application.StartupPath, "users.db");
        List<string> typeServerForward = new List<string>
        {
            "chat",
            "image",
            "file",
            "call-voice-require",
            "call-voice-accept",
            "call-voice-end",
            "call-voice-reject",
            "aes-handshake"
        };
        private List<User> users;
        Server socket;
        User server=new User 
        { 
            id = -1,
            name = "Server"
        };
        public fMain(Server socket)
        {
            InitializeComponent();
            this.socket = socket;
            lbIP.Text = this.socket.ipAddress;
            lbPort.Text = this.socket.port.ToString();
            socket.MessageReceived += OnMessageReceived;
            socket.StatusChanged += OnStatusReceived;
            socket.PeerChanged += OnPeerReceived;
        }
        private void OnMessageReceived(string id, string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnMessageReceived(id, msg)));
                return;
            }
            JObject json = JObject.Parse(msg);
            if ((string)json["type"] == "login")
            {
                var objAuth = JsonConvert.DeserializeObject<AuthMessage>(msg);
                bool loginSuccess = CheckLogin(objAuth.email, objAuth.password);
                AuthResponse res = new AuthResponse
                {
                    status = loginSuccess ? "logged" : "error",
                    message = loginSuccess?"Đăng nhập thành công":"Đăng nhập thất bại",
                    data = loginSuccess?GetUserPublicByEmail(objAuth.email):null
                };
                if (loginSuccess)
                {
                    users.FirstOrDefault(u => u.email == objAuth.email).sessionid = id;
                    users.FirstOrDefault(u => u.email == objAuth.email).socket = socket.clientConnected[id];
                    users.FirstOrDefault(u => u.email == objAuth.email).publicKeyRSA = objAuth.pubkeyrsa;
                    socket.SendMessage(id, JsonConvert.SerializeObject(res, Formatting.None));
                    Thread.Sleep(500);
                    ReloadDataSource();
                    foreach (User u in users)
                        if (u.IsOnline())
                            SendListOnlineUser(u);
                    return;
                }
                socket.SendMessage(id, JsonConvert.SerializeObject(res, Formatting.None));
                return;
            }
            else if ((string)json["type"] == "signup")
            {
                var objAuth = JsonConvert.DeserializeObject<AuthMessage>(msg);
                AuthMessage check = GetUserPublicByEmail(objAuth.email.ToLower());
                if(check != null)
                {
                    AuthResponse res = new AuthResponse
                    {
                        status = "error",
                        message = "Email đã tồn tại",
                        data = null
                    };
                    socket.SendMessage(id, JsonConvert.SerializeObject(res, Formatting.None));
                }
                else
                {
                    InsertUser(objAuth.name, objAuth.email.ToLower(), objAuth.password, objAuth.avatarencoded);
                    AuthResponse res = new AuthResponse
                    {
                        status = "signed",
                        message = "Đăng ký thành công",
                        data = null
                    };
                    socket.SendMessage(id, JsonConvert.SerializeObject(res, Formatting.None));
                    check = GetUserPublicByEmail(objAuth.email.ToLower());
                    users.Add(new User
                    {
                        id = check.id,
                        name= check.name,
                        email= check.email,
                        password= objAuth.password,
                        avatarencoded= check.avatarencoded,
                        btns = new AntdUI.CellLink[]
                        {
                            new AntdUI.CellButton("delete", "Delete", AntdUI.TTypeMini.Error)
                        }
                    });
                    LoadTable();
                }
                return;
            }
            else if (typeServerForward.Contains((string)json["type"]))
            {
                var obj = JsonConvert.DeserializeObject<ChatMessage>(msg);
                PublicUser to = obj.to;
                User forwardTo = users.FirstOrDefault(u => u.id == to.id);
                if (string.IsNullOrEmpty(obj.from.ipAddress))
                    obj.from.ipAddress=users.FirstOrDefault(u => u.sessionid == id).ipAddress;
                if(string.IsNullOrEmpty(obj.to.ipAddress))
                    obj.to.ipAddress= forwardTo.ipAddress;
                socket.SendMessage(forwardTo.sessionid, JsonConvert.SerializeObject(obj, Formatting.None));
            }
        }
        private void SendListOnlineUser(User skipUser)
        {
            List<PublicUser> onlineUser = GetOnlineUser(skipUser);
            ChatMessage msg = new ChatMessage
            {
                from = ConvertPublicUser(server),
                to = ConvertPublicUser(skipUser),
                type = "online",
                message = JsonConvert.SerializeObject(onlineUser, Formatting.None)
            };
            socket.SendMessage(skipUser.sessionid, JsonConvert.SerializeObject(msg, Formatting.None));
        }
        private void OnStatusReceived(string msg)
        {
            AntdUI.Message.info(this, msg, Font);
        }
        private void OnPeerReceived(string type, string id)
        {
            if (type == "delete")
            {
                try
                {
                    users.FirstOrDefault(x => x.sessionid == id).socket = null;
                    users.FirstOrDefault(x => x.sessionid == id).sessionid = null;
                    ReloadDataSource();
                    foreach (User u in users)
                        if (u.IsOnline())
                            SendListOnlineUser(u);
                }
                catch { }
            }
        }
        private void ReloadDataSource()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ReloadDataSource()));
                return;
            }
            lbTotalUser.Text = "Tổng số thành viên: " + users?.Count.ToString();
            int onlineCount = users?.Count(u => u.IsOnline()) ?? 0;
            lbConnected.Text = onlineCount.ToString();
            table.DataSource = users;
        }
        public static AuthMessage GetUserPublicByEmail(string email)
        {
            string dbPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "users.db"
            );

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                string sql = @"
            SELECT id, email, name, avatarencoded
            FROM users
            WHERE email = @email
            LIMIT 1
        ";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            return new AuthMessage
                            {
                                id = rd.GetInt32(0),
                                email = rd.GetString(1),
                                name = rd.GetString(2),
                                avatarencoded = rd.IsDBNull(3)
                                    ? null
                                    : rd.GetString(3)
                            };
                        }
                    }
                }
            }

            return null;
        }
        bool CheckLogin(string email, string passwordRaw)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.db");

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                string sql = @"SELECT COUNT(1)
                       FROM users
                       WHERE email = @email
                       AND password = @password";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", passwordRaw);

                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private void InitTable()
        {
            InitColumns();
            ReloadDataSource();
        }
        private void InitColumns()
        {
            table.Columns = new AntdUI.ColumnCollection
            {
                // Checkbox
                new AntdUI.ColumnCheck("check").SetFixed(),

                // STT
                new AntdUI.Column("id", "ID").SetWidth("80"),
                new AntdUI.Column("email", "Email").SetWidth("220"),
                new AntdUI.Column("name", "Name").SetWidth("150"),
                new AntdUI.Column("password", "Password").SetWidth("120"),
                new AntdUI.Column("status", "Status", AntdUI.ColumnAlign.Center)
                {
                    Width = "120"
                },
                // Action
                new AntdUI.Column("btns", "Action")
                    .SetFixed()
                    .SetWidth("120")
            };
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            table.EmptyText = "Không có dữ liệu";
            InitTable();
            users = GetUsers();
            LoadTable();
            
        }
        bool DeleteUser(int id)
        {
            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("DELETE FROM users WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch { return false; }
            }
        }
        private void LoadTable()
        {
            table.DataSource = null;
            ReloadDataSource();
        }
        private List<User> GetUsers()
        {
            var list = new List<User>();

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                string sql = "SELECT id, name, email, password, avatarencoded FROM users ORDER BY id DESC";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new User
                        {
                            id = rd.GetInt32(0),
                            name = rd.GetString(1),
                            email = rd.GetString(2),
                            password = rd.GetString(3),
                            avatarencoded = rd.IsDBNull(4) ? null : rd.GetString(4),
                            btns = new AntdUI.CellLink[]
                            {
                        new AntdUI.CellButton("delete", "Delete", AntdUI.TTypeMini.Error)
                            }
                        });
                    }
                }
            }
            return list;
        }
        public static bool InsertUser(string name, string email, string password, string avatar)
        {
            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                try
                {
                    conn.Open();

                    string sql = @"INSERT INTO users(name,email,password,avatarencoded)
                       VALUES(@name,@email,@password,@avatar)";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@avatar", avatar);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch { return false; }
            }
        }
        private void table_CellButtonClick(object sender, TableButtonEventArgs e)
        {
            if (e.Record is User emp && e.Btn.Id == "delete")
            {
                AntdUI.Modal.open(new Modal.Config(this, $"Xác nhận xóa User: {emp.email}", "", AntdUI.TType.Warn)
                {
                    CancelText = "No",
                    OkText = "Yes",
                    OnOk = config =>
                    {
                        DeleteUser(emp.id);
                        users.Remove(emp);
                        LoadTable();
                        return true;
                    },
                });
            }
        }
        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        public List<PublicUser> GetOnlineUser(User skipUser)
        {
            List<PublicUser> onlineUser=new List<PublicUser>();
            foreach (User u in users)
            {
                if(u.id != skipUser.id && u.socket != null && u.socket.Connected)
                {
                    onlineUser.Add(new PublicUser
                    {
                        id = u.id,
                        name = u.name,
                        email =u.email,
                        avatarencoded = u.avatarencoded,
                        ipAddress=u.ipAddress,
                        pubkeyrsa=u.publicKeyRSA
                    });
                }
            }
            return onlineUser;
        }
        public PublicUser ConvertPublicUser(User u)
        {
            return new PublicUser
            {
                id = u.id,
                name = u.name,
                email = u.email,
                avatarencoded = u.avatarencoded,
                ipAddress = u.ipAddress,
                pubkeyrsa=u.publicKeyRSA
            };
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            foreach (User u in users)
            {
                if (u.check)
                {
                    DeleteUser(u.id);
                    users.Remove(u);
                    btnDeleteSelected_Click(sender, e);
                    return;
                }
            }
            LoadTable();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using (fRegister f = new fRegister())
            {
                var result = f.ShowDialog(this);
                if(result == DialogResult.OK && f.created != null)
                {
                    users.Add(new User
                    {
                        id = f.created.id,
                        name = f.created.name,
                        email = f.created.email,
                        password = f.created.password,
                        avatarencoded = f.created.avatarencoded,
                        btns = new AntdUI.CellLink[]
                        {
                            new AntdUI.CellButton("delete", "Delete", AntdUI.TTypeMini.Error)
                        }
                    });
                    LoadTable();
                }
            }    
        }
    }
}
