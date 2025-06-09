namespace TCPGameChatProject
{
    partial class DiceGameForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnRoll;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnRoll = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRoll
            // 
            this.btnRoll.Location = new System.Drawing.Point(80, 60);
            this.btnRoll.Size = new System.Drawing.Size(120, 50);
            this.btnRoll.Text = "擲骰子";
            this.btnRoll.Click += new System.EventHandler(this.btnRoll_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(30, 20);
            this.lblStatus.Size = new System.Drawing.Size(250, 25);
            this.lblStatus.Text = "請按下擲骰子";
            this.lblStatus.AutoSize = true;
            // 
            // DiceGameForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.btnRoll);
            this.Controls.Add(this.lblStatus);
            this.Name = "DiceGameForm";
            this.Text = "比大小遊戲";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
