namespace TCPGameChatProject
{
    partial class LauncherForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Button btnClient;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnServer = new System.Windows.Forms.Button();
            this.btnClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(30, 30);
            this.btnServer.Size = new System.Drawing.Size(200, 60);
            this.btnServer.Text = "伺服器模式 (管理員登入)";
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // btnClient
            // 
            this.btnClient.Location = new System.Drawing.Point(30, 110);
            this.btnClient.Size = new System.Drawing.Size(200, 60);
            this.btnClient.Text = "客戶端模式 (使用者登入)";
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // LauncherForm
            // 
            this.ClientSize = new System.Drawing.Size(260, 200);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.btnClient);
            this.Name = "LauncherForm";
            this.Text = "聊天室系統啟動器";
            this.ResumeLayout(false);
        }
    }
}
