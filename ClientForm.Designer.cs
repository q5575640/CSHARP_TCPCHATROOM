namespace TCPGameChatProject
{
    partial class ClientForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSendImage;
        private System.Windows.Forms.Button btnRPS;
        private System.Windows.Forms.Button btnDice;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.Panel panelChat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnConnect = new Button();
            btnDisconnect = new Button();
            txtIP = new TextBox();
            txtPort = new TextBox();
            txtInput = new TextBox();
            btnSend = new Button();
            btnSendImage = new Button();
            btnRPS = new Button();
            btnDice = new Button();
            btnHistory = new Button();
            txtChat = new TextBox();
            panelChat = new Panel();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(204, 10);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(80, 25);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "連線";
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(290, 10);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(80, 25);
            btnDisconnect.TabIndex = 3;
            btnDisconnect.Text = "斷線";
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(12, 12);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(120, 23);
            txtIP.TabIndex = 0;
            txtIP.Text = "127.0.0.1";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(138, 12);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(60, 23);
            txtPort.TabIndex = 1;
            txtPort.Text = "8888";
            // 
            // txtInput
            // 
            txtInput.Location = new Point(12, 460);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(320, 23);
            txtInput.TabIndex = 6;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(340, 458);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(80, 25);
            btnSend.TabIndex = 7;
            btnSend.Text = "傳送";
            btnSend.Click += btnSend_Click;
            // 
            // btnSendImage
            // 
            btnSendImage.Location = new Point(430, 458);
            btnSendImage.Name = "btnSendImage";
            btnSendImage.Size = new Size(80, 25);
            btnSendImage.TabIndex = 8;
            btnSendImage.Text = "傳送圖片";
            btnSendImage.Click += btnSendImage_Click;
            // 
            // btnRPS
            // 
            btnRPS.Location = new Point(530, 50);
            btnRPS.Name = "btnRPS";
            btnRPS.Size = new Size(100, 40);
            btnRPS.TabIndex = 9;
            btnRPS.Text = "猜拳";
            btnRPS.Click += btnRPS_Click;
            // 
            // btnDice
            // 
            btnDice.Location = new Point(530, 100);
            btnDice.Name = "btnDice";
            btnDice.Size = new Size(100, 40);
            btnDice.TabIndex = 10;
            btnDice.Text = "比大小";
            btnDice.Click += btnDice_Click;
            // 
            // btnHistory
            // 
            btnHistory.Location = new Point(530, 150);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(100, 40);
            btnHistory.TabIndex = 11;
            btnHistory.Text = "對話紀錄";
            btnHistory.Click += btnHistory_Click;
            // 
            // txtChat
            // 
            txtChat.Location = new Point(12, 45);
            txtChat.Multiline = true;
            txtChat.Name = "txtChat";
            txtChat.ScrollBars = ScrollBars.Vertical;
            txtChat.Size = new Size(500, 200);
            txtChat.TabIndex = 4;
            // 
            // panelChat
            // 
            panelChat.AutoScroll = true;
            panelChat.Location = new Point(12, 250);
            panelChat.Name = "panelChat";
            panelChat.Size = new Size(500, 200);
            panelChat.TabIndex = 5;
            // 
            // ClientForm
            // 
            ClientSize = new Size(650, 500);
            Controls.Add(txtIP);
            Controls.Add(txtPort);
            Controls.Add(btnConnect);
            Controls.Add(btnDisconnect);
            Controls.Add(txtChat);
            Controls.Add(panelChat);
            Controls.Add(txtInput);
            Controls.Add(btnSend);
            Controls.Add(btnSendImage);
            Controls.Add(btnRPS);
            Controls.Add(btnDice);
            Controls.Add(btnHistory);
            Name = "ClientForm";
            Text = "多人聊天室遊戲系統";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
