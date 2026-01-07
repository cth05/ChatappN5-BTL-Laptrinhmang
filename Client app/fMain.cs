using AntdUI;
using AntdUI.Chat;
using Client_app.Properties;
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
using static System.Net.Mime.MediaTypeNames;

namespace Client_app
{
    public partial class fMain : AntdUI.Window
    {
        RSACryptoServiceProvider rsa;
        private Client socket;
        private string publicKeyRSABase64;
        private bool IsLoggedIn = false;
        Dictionary<User, ChatList> userChatList = new Dictionary<User, ChatList>();
        Dictionary<int, AesSession> listIdUserAes = new Dictionary<int, AesSession>();
        User target, myInfo;
        public fMain(Client socket)
        {
            InitializeComponent();
            this.socket = socket;
            socket.StatusChanged += OnStatusReceived;
            socket.MessageReceived += OnMessageReceived;
            pbAvatarTarget.Paint += PicAvatar_Paint;
            
        }

        private void OnMessageReceived(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnMessageReceived(msg)));
                return;
            }
            JObject json = JObject.Parse(msg);
            if ((string)json["type"] == "online")
            {
                List<User> serverList = JArray.Parse((string)json["message"]).ToObject<List<User>>();
                var serverIds = new HashSet<int>(serverList.Select(x => x.id));
                var offlineUsers = userChatList.Keys.Where(id => !serverIds.Contains(id.id)).ToList();
                foreach (User u in offlineUsers)
                {
                    if (u == target)
                    {
                        pbAvatarTarget.Image = null;
                        lbTarget.Text = "";
                        pnlToolChat.Enabled = pnlToolChat.Visible = false;
                        pnlChatInfo.Enabled = pnlChatInfo.Visible = false;
                    }
                    var chatList = userChatList[u];
                    chatList.Visible = false;
                    chatList = null;
                    userChatList.Remove(u);
                    var itemRemove = msgList.Items.FirstOrDefault(x => x.ID == u.id.ToString());
                    msgList.Items.Remove(itemRemove);
                    listIdUserAes.Remove(u.id);
                }
                foreach (User dto in serverList)
                {
                    if (dto == null)
                        continue;
                    if (!ContainUser(dto))
                    {
                        ChatList chatlist = new ChatList();
                        chatlist.Dock = DockStyle.Fill;
                        chatlist.Enabled = false;
                        chatlist.Visible = false;
                        List<(int, int, string, string, long)> history = LoadHistory(myInfo.id, dto.id);
                        if (history.Count > 0)
                        {
                            foreach(var item in history)
                            {
                                bool isMe = item.Item1 == myInfo.id;
                                if (item.Item3 == "chat")
                                {
                                    chatlist.AddToBottom(new TextChatItem(item.Item4, Base64ToImage(isMe ? myInfo.avatarencoded : dto.avatarencoded), isMe ? myInfo.name : dto.name) { Me = isMe });
                                }
                                else if (item.Item3 == "file")
                                {
                                    chatlist.AddToBottom(new TextChatItem($"📎 {item.Item4}", Base64ToImage(isMe ? myInfo.avatarencoded : dto.avatarencoded), isMe ? myInfo.name : dto.name) { Me = isMe });
                                }
                                else if (item.Item3 == "image")
                                {
                                    chatlist.AddToBottom(new TextChatItem("data:image/png;base64," + item.Item4, Base64ToImage(isMe ? myInfo.avatarencoded : dto.avatarencoded), isMe ? myInfo.name : dto.name) { Me = isMe });
                                }
                            }    
                        }
                        pnlChat.Controls.Add(chatlist);
                        userChatList.Add(dto, chatlist);
                        this.msgList.Items.Add(new MsgItem { ID = dto.id.ToString(), Name = dto.name, Text = "Connected", Tag = dto.id, Count = 0, Icon = Base64ToImage(dto.avatarencoded) });
                        if(myInfo.id < dto.id)
                        {
                            var aes = AesSession.Create();
                            string targetPublicKeyBase64 = dto.pubkeyrsa;
                            byte[] encryptedKey = RsaHelper.EncryptWithPublicKey(aes.Key, targetPublicKeyBase64);
                            byte[] encryptedIV = RsaHelper.EncryptWithPublicKey(aes.IV, targetPublicKeyBase64);
                            ChatMessage chatObj = new ChatMessage
                            {
                                type = "aes-handshake",
                                from = myInfo,
                                to = dto,
                                message = Convert.ToBase64String(encryptedKey),
                                note = Convert.ToBase64String(encryptedIV)
                            };
                            socket.SendMessage(JsonConvert.SerializeObject(chatObj, Formatting.None));
                            listIdUserAes.Add(dto.id, aes);
                        }    
                    }
                }
            }
            else if ((string)json["type"] == "chat")
            {
                var obj = JsonConvert.DeserializeObject<ChatMessage>(msg);
                User from = obj.from;
                var existingUser = userChatList.Keys.FirstOrDefault(u => u.id == obj.from.id);
                ChatList chatlist = userChatList[existingUser];
                string plaintext= DecryptMessage(obj.message, listIdUserAes[from.id]);
                InsertMessage(from.id, myInfo.id, "chat", plaintext);
                chatlist.AddToBottom(new TextChatItem(plaintext, Base64ToImage(from.avatarencoded), from.name));
                NotifyMessage(from.name, $"{from.name}: {plaintext}");
                var item = msgList.Items.FirstOrDefault(i => i.ID == from.id.ToString());
                if (item != null)
                {
                    item.Text = plaintext;
                    item.Time = DateTime.Now.ToString("HH:mm");
                    if (target == null || target.id != from.id)
                        item.Count += 1;
                }
            }
            else if ((string)json["type"] == "file")
            {
                var obj = JsonConvert.DeserializeObject<ChatMessage>(msg);
                string fileName = obj.note;
                User from = obj.from;
                var existingUser = userChatList.Keys.FirstOrDefault(u => u.id == obj.from.id);
                ChatList chatlist = userChatList[existingUser];
                var item = msgList.Items.FirstOrDefault(i => i.ID == from.id.ToString());
                if (item != null)
                {
                    item.Text = obj.note;
                    item.Time = DateTime.Now.ToString("HH:mm");
                    if (target == null || target.id != from.id)
                        item.Count += 1;
                }
                chatlist.AddToBottom(new TextChatItem($"📎 {Path.GetFileName(fileName)} (đã gửi)", Base64ToImage(from.avatarencoded), from.name) { Me = false });
                NotifyMessage(from.name, $"Đã gửi tệp tin: {obj.note}");
                AntdUI.Modal.open(new Modal.Config(this, "Xác nhận", $"{from.name} muốn gửi cho bạn tệp tin: {obj.note}", AntdUI.TType.Info)
                {
                    CancelText = "No",
                    OkText = "Yes",
                    OnOk = config =>
                    {
                        string saveDir = Path.Combine(System.Windows.Forms.Application.StartupPath, "Downloads");
                        if(!Directory.Exists(saveDir))
                            Directory.CreateDirectory(saveDir);
                        string plaintext= DecryptMessage(obj.message, listIdUserAes[from.id]);
                        byte[] bytes = Convert.FromBase64String(plaintext);
                        string savePath = Path.Combine(saveDir, fileName);
                        File.WriteAllBytes(savePath, bytes);
                        InsertMessage(from.id, myInfo.id, "file", savePath);
                        AntdUI.Message.info(this, $"Đã lưu: {savePath}", Font);
                        return true;
                    },
                });
            }
            else if ((string)json["type"] == "image")
            {
                var obj = JsonConvert.DeserializeObject<ChatMessage>(msg);
                string fileName = obj.note;
                byte[] bytes = Convert.FromBase64String(obj.message);
                User from = obj.from;
                var existingUser = userChatList.Keys.FirstOrDefault(u => u.id == obj.from.id);
                ChatList chatlist = userChatList[existingUser];
                var item = msgList.Items.FirstOrDefault(i => i.ID == from.id.ToString());
                if (item != null)
                {
                    item.Text = obj.note;
                    item.Time = DateTime.Now.ToString("HH:mm");
                    if (target == null || target.id != from.id)
                        item.Count += 1;
                }
                NotifyMessage(from.name, $"Đã gửi hình ảnh");
                string plaintext= DecryptMessage(obj.message, listIdUserAes[from.id]);
                InsertMessage(from.id, myInfo.id, "image", plaintext);
                chatlist.AddToBottom(new TextChatItem("data:image/png;base64," + plaintext, Base64ToImage(from.avatarencoded), from.name) { Me = false });

            }
            else if ((string)json["type"] == "call-voice-require")
            {
                var obj = JsonConvert.DeserializeObject<ChatMessage>(msg);
                User from = obj.from;
                from.udpport=int.Parse(DecryptMessage(obj.message, listIdUserAes[from.id]));
                using (fNotiCall f = new fNotiCall(socket, from, myInfo, listIdUserAes[from.id]))
                {
                    var existingUser = userChatList.Keys.FirstOrDefault(u => u.id == obj.from.id);
                    ChatList chatlist = userChatList[existingUser];
                    f.ShowDialog(this);
                    InsertMessage(from.id, myInfo.id, "chat", f.IsReply ? $"Cuộc gọi đến" : "Đã từ chối cuộc gọi");
                    chatlist.AddToBottom(new TextChatItem(f.IsReply ? $"Cuộc gọi đến":"Đã từ chối cuộc gọi", Base64ToImage(from.avatarencoded), from.name));
                }
            }
            else if ((string)json["type"] == "aes-handshake")
            {
                var obj = JsonConvert.DeserializeObject<ChatMessage>(msg);
                User from = obj.from;
                byte[] encKey = Convert.FromBase64String(obj.message);
                byte[] encIV = Convert.FromBase64String(obj.note);
                byte[] aesKey = rsa.Decrypt(encKey, false);
                byte[] aesIV = rsa.Decrypt(encIV, false);
                var aes = new AesSession
                {
                    Key = aesKey,
                    IV = aesIV
                };
                listIdUserAes.Add(from.id, aes);
            }
        }
        List<(int fromId, int toId, string type, string message, long timestamp)> LoadHistory(int userA, int userB)
        {
            var list = new List<(int, int, string, string, long)>();
            using(var conn = new SQLiteConnection("Data Source=chat.db"))
            {
                conn.Open();

                var cmd = new SQLiteCommand(@"
                    SELECT fromId, toId, type, message, timestamp
                    FROM messages
                    WHERE 
                        (fromId = @a AND toId = @b)
                        OR
                        (fromId = @b AND toId = @a)
                    ORDER BY timestamp ASC
                ", conn);

                cmd.Parameters.AddWithValue("@a", userA);
                cmd.Parameters.AddWithValue("@b", userB);

                using(var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add((
                            Convert.ToInt32(rd["fromId"]),
                            Convert.ToInt32(rd["toId"]),
                            rd["type"].ToString(),
                            rd["message"].ToString(),
                            Convert.ToInt64(rd["timestamp"])
                        ));
                    }
                }
            }
            return list;
        }
        private void NotifyMessage(string from, string message)
        {
            AntdUI.Notification.info(this, $"Bạn có tin nhắn từ: {from}", message, TAlignFrom.BR, Font);
        }
        private bool ContainUser(User user)
        {
            foreach (User u in userChatList.Keys)
                if (u.id == user.id)
                    return true;
            return false;
        }
        private void OnStatusReceived(string msg)
        {
            AntdUI.Message.info(this, msg, Font);
            if (msg == "Server Disconnected")
            {
                AntdUI.Modal.open(new Modal.Config(this, "Server Disconnected", "", AntdUI.TType.Info)
                {
                    OkText = "Exit",
                    CancelText = "Close",
                    OnOk = config =>
                    {
                        Environment.Exit(0);
                        return true;
                    },
                });
            }
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            if (!File.Exists("chat.db"))
                InitDb.CreateTable();
            if (!Directory.Exists("Key"))
                Directory.CreateDirectory("Key");
            if(!File.Exists(Path.Combine("Key", "private.key")) || !File.Exists(Path.Combine("Key", "public.key")))
            {
                rsa = new RSACryptoServiceProvider(2048);
                string privateKey = rsa.ToXmlString(true);
                string publicKey = rsa.ToXmlString(false);
                File.WriteAllText(Path.Combine("Key", "private.key"), privateKey);
                File.WriteAllText(Path.Combine("Key", "public.key"), publicKey);
            }
            else
            {
                rsa = new RSACryptoServiceProvider();
                string privateKey = File.ReadAllText(Path.Combine("Key", "private.key"));
                rsa.FromXmlString(privateKey);
            }
            publicKeyRSABase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(File.ReadAllText(Path.Combine("Key", "public.key"))));
        }
        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        public static string EncryptMessage(string plainText, AesSession aes)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherBytes = AesCrypto.Encrypt(plainBytes, aes.Key, aes.IV);
            return Convert.ToBase64String(cipherBytes);
        }
        public static string DecryptMessage(string encryptedBase64, AesSession aes)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedBase64);
            byte[] plainBytes = AesCrypto.Decrypt(cipherBytes, aes.Key, aes.IV);
            return Encoding.UTF8.GetString(plainBytes);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string text = txtInput.Text;
            if (string.IsNullOrEmpty(text))
                return;
            ChatMessage msgObj = new ChatMessage
            {
                type = "chat",
                from = myInfo,
                to = target,
                message = EncryptMessage(text, listIdUserAes[target.id]),
            };
            string rawMsgSend = JsonConvert.SerializeObject(msgObj, Formatting.None);
            try
            {
                socket.SendMessage(rawMsgSend);
                ChatList chatlist;
                if (userChatList.TryGetValue(target, out chatlist) && target != null)
                {
                    chatlist.AddToBottom(new TextChatItem(text, Base64ToImage(myInfo.avatarencoded), myInfo.name) { Me = true });
                    InsertMessage(myInfo.id, target.id, "chat", text);
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this, $"❌ Không gửi được tin nhắn! Lỗi: {ex.Message}", Font);
            }
            finally
            {
                txtInput.Clear();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (fLogin f = new fLogin(socket, publicKeyRSABase64))
            {
                var result = f.ShowDialog(this);
                if (result == DialogResult.OK && f.IsLoggedIn)
                {
                    IsLoggedIn = true;
                    myInfo = new User
                    {
                        email = f.CurrentEmail,
                        name = f.Name,
                        id = f.Id,
                        avatarencoded = f.AvatarEncoded
                    };
                    AntdUI.Notification.success(
                        this,
                        "Thông báo",
                        f.Message,
                        AntdUI.TAlignFrom.Top
                    );
                    btnLogin.Enabled = false;
                    btnLogin.Visible = false;
                    btnSignup.Enabled = false;
                    btnSignup.Visible = false;
                    AntdUI.Panel pnInfo= CreateUserInfoPanel(myInfo.name, myInfo.id, myInfo.avatarencoded);
                    pnInfoUser.Controls.Add(pnInfo);
                }
            }
        }
        AntdUI.Panel CreateUserInfoPanel(string name,int userId,string avatarBase64)
        {
            AntdUI.Panel panel = new AntdUI.Panel
            {
                Size = new Size(227, 51),
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };

            PictureBox picAvatar = new PictureBox
            {
                Size = new Size(40, 40),
                Location = new Point(6, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Base64ToImage(avatarBase64)
            };

            picAvatar.Paint += (s, e) =>
            {
                var gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
                picAvatar.Region = new Region(gp);
            };

            AntdUI.Label lblName = new AntdUI.Label
            {
                Text = name,
                Location = new Point(55, 8),
                AutoSize = false,
                Size = new Size(160, 18),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.Black
            };

            AntdUI.Label lblInfo = new AntdUI.Label
            {
                Text = $"{myInfo.email.ToLower()} - ID: {userId}",
                Location = new Point(55, 24),
                AutoSize = false,
                Size = new Size(160, 18),
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray
            };
            panel.Controls.Add(picAvatar);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblInfo);

            return panel;
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            using (fRegister f = new fRegister(socket))
            {
                var result = f.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    AntdUI.Notification.success(
                        this,
                        "Thông báo",
                        f.Message,
                        AntdUI.TAlignFrom.Top
                    );
                }
            }
        }
        private void PicAvatar_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb == null) return;

            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, pb.Width - 1, pb.Height - 1);
            pb.Region = new Region(path);
        }
        private void msgList_ItemClick(object sender, MsgItemClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                e.Item.Select = true;
                target = userChatList.Keys.FirstOrDefault(x => x.id.ToString() == e.Item.ID);
                lbTarget.Text = $"{target.id}: {target.name}";
                ChatList chatlist;
                if (!userChatList.TryGetValue(target, out chatlist))
                {
                    AntdUI.Message.error(this, $"Không tìm thấy user {e.Item.ID}", Font);
                    return;
                }
                foreach (var item in userChatList)
                {
                    if (item.Key.id != target.id)
                    {
                        item.Value.Visible = false;
                        item.Value.Enabled = false;
                        continue;
                    }
                    item.Value.Visible = item.Value.Enabled = true;
                }
                pbAvatarTarget.Visible = true;
                pbAvatarTarget.SizeMode = PictureBoxSizeMode.Zoom;
                pbAvatarTarget.Image = Base64ToImage(target.avatarencoded);
                e.Item.Count = 0;
                pnlToolChat.Enabled = pnlToolChat.Visible = true;
                pnlChatInfo.Enabled = pnlChatInfo.Visible = true;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    AntdUI.Modal.open(new Modal.Config(this, $"Xác nhận gửi tệp tin [{ofd.FileName}]", "", AntdUI.TType.Info)
                    {
                        CancelText = "No",
                        OkText = "Yes",
                        OnOk = config =>
                        {
                            SendFile(target, ofd.FileName);
                            return true;
                        },
                    });
                }
            }
        }
        private void SendFile(User target, string filePath)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(filePath);
                string base64 = Convert.ToBase64String(bytes);

                ChatMessage msgObj = new ChatMessage
                {
                    type = "file",
                    from = myInfo,
                    to = target,
                    note = Path.GetFileName(filePath),
                    message = EncryptMessage(base64, listIdUserAes[target.id])
                };

                string raw = JsonConvert.SerializeObject(msgObj, Formatting.None);
                socket.SendMessage(raw);
                if (userChatList.TryGetValue(target, out ChatList chatlist))
                {
                    chatlist.AddToBottom(new TextChatItem($"📎 {Path.GetFileName(filePath)} (đã gửi)", Base64ToImage(myInfo.avatarencoded), myInfo.name) { Me = true });
                    AntdUI.Message.error(this, $"Gửi tệp tin thành công", Font);
                    InsertMessage(myInfo.id, target.id, "file", filePath);
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this, $"Không gửi được file: {ex.Message}", Font);
            }
        }
        void InsertMessage(int fromId,int toId,string type,string encryptedMessage)
        {
            using (var conn = new SQLiteConnection("Data Source=chat.db"))
            {
                conn.Open();

                long ts = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                var cmd = new SQLiteCommand(
                    "INSERT INTO messages (fromId, toId, type, message, timestamp) " +
                    "VALUES (@f,@t,@type,@msg,@ts)",
                    conn
                );

                cmd.Parameters.AddWithValue("@f", fromId);
                cmd.Parameters.AddWithValue("@t", toId);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@msg", encryptedMessage);
                cmd.Parameters.AddWithValue("@ts", ts);

                cmd.ExecuteNonQuery();
            }
        }
        private void btnSendImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ảnh|*.png;*.jpg;*.jpeg;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SendImage(target, ofd.FileName);
                }
            }
        }
        private void SendImage(User target, string filePath)
        {
            try
            {
                byte[] bytes = ImageHelper.CompressImage(filePath);
                string base64 = Convert.ToBase64String(bytes);

                ChatMessage msgObj = new ChatMessage
                {
                    type = "image",
                    from = myInfo,
                    to = target,
                    note = Path.GetFileName(filePath),
                    message = EncryptMessage(base64, listIdUserAes[target.id])
                };

                string raw = JsonConvert.SerializeObject(msgObj, Formatting.None);
                socket.SendMessage(raw);
                if (userChatList.TryGetValue(target, out ChatList chatlist))
                {
                    chatlist.AddToBottom(new TextChatItem("data:image/png;base64," + base64, Base64ToImage(myInfo.avatarencoded), myInfo.name) { Me = true });
                    AntdUI.Message.success(this, $"Gửi hình ảnh thành công", Font);
                    InsertMessage(myInfo.id, target.id, "image", base64);
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this, $"Không gửi được file: {ex.Message}", Font);
            }
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (target == null)
                return;
            using (fCallVoice f = new fCallVoice(socket, myInfo, target, listIdUserAes[target.id]))
            {
                f.ShowDialog(this);
                var existingUser = userChatList.Keys.FirstOrDefault(u => u.id == target.id);
                ChatList chatlist = userChatList[existingUser];
                InsertMessage(myInfo.id, target.id, "chat", f.IsSuccessed ? $"Đã gọi đến" : "Đã bị từ chối cuộc gọi");
                chatlist.AddToBottom(new TextChatItem(f.IsSuccessed ? $"Đã gọi đến" : "Đã bị từ chối cuộc gọi", Base64ToImage(target.avatarencoded), target.name) { Me=true});
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        public static System.Drawing.Image Base64ToImage(string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return Properties.Resources.default_avatar; // avatar mặc định

            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                using (var ms = new MemoryStream(bytes))
                {
                    return System.Drawing.Image.FromStream(ms);
                }
            }
            catch
            {
                return Properties.Resources.default_avatar;
            }
        }
    }
}
