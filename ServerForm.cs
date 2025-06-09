using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class ServerForm : Form
    {
        private TcpListener server;
        private Thread listenThread;
        private bool running = false;
        private string adminName;

        private Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        private Dictionary<string, string> clientInGame = new Dictionary<string, string>();

        private class GameSession
        {
            public string Player1;
            public string Player2;
            public string Type;
            public Dictionary<string, string> Choices = new Dictionary<string, string>();
        }
        private List<GameSession> gameSessions = new List<GameSession>();

        public ServerForm(string admin)
        {
            InitializeComponent();
            adminName = admin;
            this.Text = $"伺服器端 (管理員: {adminName})";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            running = true;
            server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            listenThread = new Thread(ListenClients);
            listenThread.Start();
            Log("伺服器已啟動");
        }

        private void ListenClients()
        {
            while (running)
            {
                TcpClient client = server.AcceptTcpClient();
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            string nickname = "";

            try
            {
                while (true)
                {
                    var (packetType, message) = PacketHelper.ReceivePacket(stream);
                    if (message == null) break;

                    if (packetType == PacketHelper.PacketType.Image)
                    {
                        BroadcastRawImage(message);
                        Log("圖片封包已轉發");
                        continue;
                    }

                    if (message.StartsWith("[LOGIN]:"))
                    {
                        nickname = message.Substring(8);
                        lock (clients)
                        {
                            clients[nickname] = client;
                        }
                        Log($"{nickname} 已登入");
                        BroadcastWithTimestamp($"系統: {nickname} 已加入聊天室");
                    }
                    else if (message.StartsWith("[TEXT]:"))
                    {
                        string text = message.Substring(7);
                        Log(text);
                        BroadcastWithTimestamp(text);
                    }
                    else if (message.StartsWith("[INVITE]:"))
                    {
                        string[] parts = message.Substring(9).Split(':');
                        string gameType = parts[0];
                        string target = parts[1];
                        string inviter = parts[2];

                        if (!clients.ContainsKey(target))
                        {
                            SendTo(inviter, "[INVITE_FAIL]:對象不存在或未在線上");
                            continue;
                        }
                        if (inviter == target)
                        {
                            SendTo(inviter, "[INVITE_FAIL]:無法邀請自己進行對戰");
                            continue;
                        }

                        Log($"{inviter} 邀請 {target} 進行 {gameType} 對戰");
                        SendTo(target, $"[INVITE]:{gameType}:{inviter}");
                    }
                    else if (message.StartsWith("[AGREE]:"))
                    {
                        string[] parts = message.Substring(8).Split(':');
                        string gameType = parts[0];
                        string inviter = parts[1];
                        string accepter = parts[2];

                        var session = new GameSession
                        {
                            Player1 = inviter,
                            Player2 = accepter,
                            Type = gameType
                        };
                        gameSessions.Add(session);
                        clientInGame[inviter] = accepter;
                        clientInGame[accepter] = inviter;

                        Log($"遊戲開始：{inviter} vs {accepter} ({gameType})");
                        SendTo(inviter, $"[START]:{gameType}:{inviter}:{accepter}");
                        SendTo(accepter, $"[START]:{gameType}:{inviter}:{accepter}");
                    }
                    else if (message.StartsWith("[CHOICE]:"))
                    {
                        string[] parts = message.Substring(9).Split(':');
                        string gameType = parts[0];
                        string player = parts[1];
                        string choice = parts[2];

                        GameSession session = gameSessions.Find(s => s.Type == gameType && (s.Player1 == player || s.Player2 == player));
                        if (session != null)
                        {
                            session.Choices[player] = choice;
                            string opponent = (player == session.Player1) ? session.Player2 : session.Player1;
                            SendTo(opponent, $"[CHOICE_DONE]:{gameType}:{player}");

                            if (session.Choices.Count == 2)
                            {
                                string result = CalculateResult(session);
                                Log($"遊戲結果: {result}");
                                BroadcastWithTimestamp("遊戲結果:" + (gameType== "RPS" ? "猜拳": "比大小") +$"|{result}");

                                gameSessions.Remove(session);
                                clientInGame.Remove(session.Player1);
                                clientInGame.Remove(session.Player2);
                            }
                        }
                    }
                }
            }
            catch { }
            finally
            {
                if (!string.IsNullOrEmpty(nickname))
                {
                    lock (clients)
                    {
                        clients.Remove(nickname);
                        clientInGame.Remove(nickname);
                    }
                    Log($"{nickname} 離線");
                    BroadcastWithTimestamp($"系統: {nickname} 已離開聊天室");
                }
                client.Close();
            }
        }

        private string CalculateResult(GameSession session)
        {
            string p1 = session.Player1;
            string p2 = session.Player2;
            string c1 = session.Choices[p1];
            string c2 = session.Choices[p2];
            string detail = "";

            if (session.Type == "RPS")
            {
                // 先轉換成中文顯示
                string c1Text = TranslateRPS(c1);
                string c2Text = TranslateRPS(c2);

                detail = $"{p1} 出 {c1Text}, {p2} 出 {c2Text} — ";

                if (c1 == c2)
                    return detail + "平手";

                if ((c1 == "ROCK" && c2 == "SCISSORS") ||
                    (c1 == "PAPER" && c2 == "ROCK") ||
                    (c1 == "SCISSORS" && c2 == "PAPER"))
                    return detail + $"{p1} 勝利";

                return detail + $"{p2} 勝利";
            }
            else if (session.Type == "DICE")
            {
                detail = $"{p1} 擲出 {c1} 點, {p2} 擲出 {c2} 點 — ";
                int n1 = int.Parse(c1);
                int n2 = int.Parse(c2);
                if (n1 == n2) return detail + "平手";
                return detail + ((n1 > n2) ? $"{p1} 勝利" : $"{p2} 勝利");
            }

            return "未知結果";
        }


        private void BroadcastWithTimestamp(string msg)
        {
            string finalMsg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {msg}";
            Broadcast(finalMsg);
        }

        private void Broadcast(string msg)
        {
            lock (clients)
            {
                foreach (var client in clients.Values)
                {
                    try
                    {
                        PacketHelper.SendPacket(client.GetStream(), msg, PacketHelper.PacketType.Text);
                    }
                    catch { }
                }
            }
        }

        private void BroadcastRawImage(string base64)
        {
            lock (clients)
            {
                foreach (var client in clients.Values)
                {
                    try
                    {
                        PacketHelper.SendPacket(client.GetStream(), base64, PacketHelper.PacketType.Image);
                    }
                    catch { }
                }
            }
        }

        private void SendTo(string target, string msg)
        {
            lock (clients)
            {
                if (clients.ContainsKey(target))
                {
                    PacketHelper.SendPacket(clients[target].GetStream(), msg, PacketHelper.PacketType.Text);
                }
            }
        }

        private void Log(string text)
        {
            string timestamp = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ";
            if (InvokeRequired)
                Invoke(new Action(() => lstLog.Items.Add(timestamp + text)));
            else
                lstLog.Items.Add(timestamp + text);
        }

        private void btnExportLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文字檔 (*.txt)|*.txt";
            sfd.FileName = $"ServerLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    foreach (var item in lstLog.Items)
                    {
                        sw.WriteLine(item.ToString());
                    }
                }
                MessageBox.Show("✅ 匯出完成！", "匯出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private string TranslateRPS(string choice)
        {
            return choice switch
            {
                "ROCK" => "石頭",
                "PAPER" => "布",
                "SCISSORS" => "剪刀",
                _ => choice
            };
        }

    }
}
