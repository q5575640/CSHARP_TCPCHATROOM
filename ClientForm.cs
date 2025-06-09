using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        private ConcurrentQueue<(PacketHelper.PacketType, string)> messageQueue = new ConcurrentQueue<(PacketHelper.PacketType, string)>();
        private System.Windows.Forms.Timer queueTimer;

        private RockPaperScissorsForm rpsForm;
        private DiceGameForm diceForm;
        private ChatHistoryForm historyForm;

        private string nickname;
        private string currentGameType = "";
        private string opponentName = "";

        public ClientForm(string nickname)
        {
            InitializeComponent();
            this.nickname = nickname;
            this.Text = $"客戶端 - {nickname}";

            rpsForm = new RockPaperScissorsForm(this, nickname);
            diceForm = new DiceGameForm(this, nickname);
            historyForm = new ChatHistoryForm();

            queueTimer = new System.Windows.Forms.Timer();
            queueTimer.Interval = 50;
            queueTimer.Tick += QueueTimer_Tick;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient();
                client.Connect(txtIP.Text, int.Parse(txtPort.Text));
                stream = client.GetStream();

                SendPacket("[LOGIN]:" + nickname);

                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                queueTimer.Start();

                AppendMessage("✅ 已連線到伺服器");
            }
            catch (Exception ex)
            {
                AppendMessage("❌ 連線失敗: " + ex.Message);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                stream?.Close();
                client?.Close();
                receiveThread?.Abort();
                queueTimer?.Stop();
                AppendMessage("⚠️ 已中斷連線");
            }
            catch { }
        }

        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    var (packetType, message) = PacketHelper.ReceivePacket(stream);
                    if (message == null) break;
                    messageQueue.Enqueue((packetType, message));
                }
            }
            catch
            {
                AppendMessage("⚠️ 伺服器已中斷");
            }
        }

        private void QueueTimer_Tick(object sender, EventArgs e)
        {
            if (messageQueue.TryDequeue(out var packet))
            {
                ProcessPacket(packet.Item1, packet.Item2);
            }
        }

        private void ProcessPacket(PacketHelper.PacketType type, string message)
        {
            if (type == PacketHelper.PacketType.Image)
            {
                string[] parts = message.Split(new[] { ':' }, 2);
                string sender = parts[0];
                string base64Str = parts[1];
                string timestamp = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]";
                string displaySender = (sender == nickname) ? "你" : sender;

                string displayMsg = $"{timestamp} {displaySender} 發出了一張圖片:";
                AppendMessage(displayMsg);
                historyForm.AddMessage(displayMsg);
                ShowImageFromBase64(sender, base64Str);
                return;
            }

            if (message.StartsWith("[INVITE]:"))
            {
                string[] parts = message.Substring(9).Split(':');
                string gameType = parts[0];
                string inviter = parts[1];

                if (inviter == nickname)
                {
                    AppendMessage($"你已向對方發送 {gameType} 對戰邀請");
                    return;
                }

                var result = MessageBox.Show($"{inviter} 邀請你進行 {gameType} 遊戲，是否接受？", "遊戲邀請", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    SendPacket($"[AGREE]:{gameType}:{inviter}:{nickname}");
                }
                return;
            }

            if (message.StartsWith("[INVITE_FAIL]:"))
            {
                string failMsg = message.Substring(13);
                MessageBox.Show(failMsg, "邀請失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (message.StartsWith("[START]:"))
            {
                string[] parts = message.Substring(8).Split(':');
                currentGameType = parts[0];
                opponentName = (nickname == parts[1]) ? parts[2] : parts[1];

                if (currentGameType == "RPS")
                {
                    if (rpsForm.IsDisposed) rpsForm = new RockPaperScissorsForm(this, nickname);
                    rpsForm.ResetButtons();
                    rpsForm.Show();
                }
                else if (currentGameType == "DICE")
                {
                    if (diceForm.IsDisposed) diceForm = new DiceGameForm(this, nickname);
                    diceForm.ResetButton();
                    diceForm.Show();
                }
                return;
            }

            if (message.StartsWith("[CHOICE_DONE]:"))
            {
                string[] parts = message.Substring(14).Split(':');
                string gameType = parts[0];
                if (gameType == "RPS")
                    rpsForm.SetOpponentReady();
                else if (gameType == "DICE")
                    diceForm.SetOpponentReady();
                return;
            }

            if (message.StartsWith("[RESULT]:"))
            {
                string resultMsg = message.Substring(9);
                AppendMessage($"🎮 遊戲結果：{resultMsg}");
                historyForm.AddMessage($"🎮 遊戲結果：{resultMsg}");

                if (currentGameType == "RPS")
                {
                    MessageBox.Show("猜拳對戰結束！");
                    rpsForm.Hide();
                }
                else if (currentGameType == "DICE")
                {
                    MessageBox.Show("比大小對戰結束！");
                    diceForm.Hide();
                }
                return;
            }

            AppendMessage(message);
            historyForm.AddMessage(message);
        }

        public void SendChoice(string choice)
        {
            SendPacket($"[CHOICE]:{currentGameType}:{nickname}:{choice}");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string text = txtInput.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                string fullMsg = $"{nickname}: {text}";
                SendPacket("[TEXT]:" + fullMsg);
                txtInput.Clear();
            }
        }

        private void btnSendImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(ofd.FileName);
                Image resized = ResizeImage(img, 800, 800);

                using (MemoryStream ms = new MemoryStream())
                {
                    var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    var encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 85L);
                    resized.Save(ms, encoder, encoderParams);

                    string imgBase64 = Convert.ToBase64String(ms.ToArray());
                    string payload = $"{nickname}:{imgBase64}";
                    SendPacket(payload, PacketHelper.PacketType.Image);
                }
            }
        }

        private void btnRPS_Click(object sender, EventArgs e)
        {
            string targetUser = PromptTargetUser();
            if (!string.IsNullOrEmpty(targetUser))
            {
                if (targetUser == nickname)
                {
                    MessageBox.Show("❌ 不能邀請自己！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SendPacket($"[INVITE]:RPS:{targetUser}:{nickname}");
            }
        }

        private void btnDice_Click(object sender, EventArgs e)
        {
            string targetUser = PromptTargetUser();
            if (!string.IsNullOrEmpty(targetUser))
            {
                if (targetUser == nickname)
                {
                    MessageBox.Show("❌ 不能邀請自己！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SendPacket($"[INVITE]:DICE:{targetUser}:{nickname}");
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            historyForm.ShowDialog();
        }

        private string PromptTargetUser()
        {
            return Microsoft.VisualBasic.Interaction.InputBox("請輸入對戰對象暱稱：", "選擇對手");
        }

        private void ShowImageFromBase64(string sender, string base64Str)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Str);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);

                    string timestamp = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]";
                    string labelText = (sender == nickname)
                        ? $"{timestamp} 來自你的圖片："
                        : $"{timestamp} 來自 {sender} 的圖片：";

                    // 動態計算目前最底部的 Y 位置
                    int offsetY = 10;
                    foreach (Control c in panelChat.Controls)
                    {
                        offsetY = Math.Max(offsetY, c.Bottom + 10);
                    }

                    // 動態新增文字Label
                    Label lbl = new Label
                    {
                        Text = labelText,
                        AutoSize = true,
                        MaximumSize = new Size(panelChat.Width - 40, 0),
                        Location = new Point(10, offsetY),
                        Font = new Font("微軟正黑體", 10, FontStyle.Bold)
                    };
                    panelChat.Controls.Add(lbl);

                    // 動態新增圖片
                    PictureBox pictureBox = new PictureBox
                    {
                        Image = img,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Width = panelChat.Width - 40,
                        Height = 200,
                        Location = new Point(10, lbl.Bottom + 5)
                    };
                    panelChat.Controls.Add(pictureBox);
                }
            }
            catch
            {
                AppendMessage("⚠️ 圖片解碼失敗");
            }
        }


        private Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var resized = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(resized))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return resized;
        }

        public void SendPacket(string message, PacketHelper.PacketType type = PacketHelper.PacketType.Text)
        {
            try
            {
                PacketHelper.SendPacket(stream, message, type);
            }
            catch
            {
                AppendMessage("⚠️ 傳送封包失敗，可能已斷線");
            }
        }

        private void AppendMessage(string msg)
        {
            if (InvokeRequired)
                Invoke(new Action(() => txtChat.AppendText(msg + Environment.NewLine)));
            else
                txtChat.AppendText(msg + Environment.NewLine);
        }
    }
}
