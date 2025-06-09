namespace TCPGameChatProject
{
    partial class ServerForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Button btnExportLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnStart = new Button();
            lstLog = new ListBox();
            btnExportLog = new Button();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(26, 20);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(88, 30);
            btnStart.TabIndex = 0;
            btnStart.Text = "啟動伺服器";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(26, 70);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(630, 394);
            lstLog.TabIndex = 1;
            // 
            // btnExportLog
            // 
            btnExportLog.Location = new Point(569, 20);
            btnExportLog.Name = "btnExportLog";
            btnExportLog.Size = new Size(88, 30);
            btnExportLog.TabIndex = 2;
            btnExportLog.Text = "匯出紀錄";
            btnExportLog.UseVisualStyleBackColor = true;
            btnExportLog.Click += btnExportLog_Click;
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 500);
            Controls.Add(btnExportLog);
            Controls.Add(lstLog);
            Controls.Add(btnStart);
            Name = "ServerForm";
            Text = "伺服器端";
            ResumeLayout(false);
        }
    }
}
