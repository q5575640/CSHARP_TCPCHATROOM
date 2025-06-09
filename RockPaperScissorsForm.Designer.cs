namespace TCPGameChatProject
{
    partial class RockPaperScissorsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnRock;
        private System.Windows.Forms.Button btnPaper;
        private System.Windows.Forms.Button btnScissors;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnRock = new System.Windows.Forms.Button();
            this.btnPaper = new System.Windows.Forms.Button();
            this.btnScissors = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRock
            // 
            this.btnRock.Location = new System.Drawing.Point(30, 60);
            this.btnRock.Size = new System.Drawing.Size(80, 40);
            this.btnRock.Text = "石頭";
            this.btnRock.Click += new System.EventHandler(this.btnRock_Click);
            // 
            // btnPaper
            // 
            this.btnPaper.Location = new System.Drawing.Point(130, 60);
            this.btnPaper.Size = new System.Drawing.Size(80, 40);
            this.btnPaper.Text = "布";
            this.btnPaper.Click += new System.EventHandler(this.btnPaper_Click);
            // 
            // btnScissors
            // 
            this.btnScissors.Location = new System.Drawing.Point(230, 60);
            this.btnScissors.Size = new System.Drawing.Size(80, 40);
            this.btnScissors.Text = "剪刀";
            this.btnScissors.Click += new System.EventHandler(this.btnScissors_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(30, 20);
            this.lblStatus.Size = new System.Drawing.Size(300, 25);
            this.lblStatus.Text = "請選擇出拳";
            this.lblStatus.AutoSize = true;
            // 
            // RockPaperScissorsForm
            // 
            this.ClientSize = new System.Drawing.Size(350, 130);
            this.Controls.Add(this.btnRock);
            this.Controls.Add(this.btnPaper);
            this.Controls.Add(this.btnScissors);
            this.Controls.Add(this.lblStatus);
            this.Name = "RockPaperScissorsForm";
            this.Text = "猜拳遊戲";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
