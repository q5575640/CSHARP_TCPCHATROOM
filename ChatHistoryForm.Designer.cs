namespace TCPGameChatProject
{
    partial class ChatHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listHistory;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listHistory = new System.Windows.Forms.ListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listHistory
            // 
            this.listHistory.FormattingEnabled = true;
            this.listHistory.ItemHeight = 12;
            this.listHistory.Location = new System.Drawing.Point(12, 12);
            this.listHistory.Size = new System.Drawing.Size(360, 280);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 305);
            this.btnClear.Size = new System.Drawing.Size(360, 30);
            this.btnClear.Text = "清除紀錄";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 345);
            this.btnExport.Size = new System.Drawing.Size(175, 30);
            this.btnExport.Text = "匯出紀錄";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(197, 345);
            this.btnImport.Size = new System.Drawing.Size(175, 30);
            this.btnImport.Text = "匯入紀錄";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // ChatHistoryForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 390);
            this.Controls.Add(this.listHistory);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Name = "ChatHistoryForm";
            this.Text = "聊天紀錄";
            this.ResumeLayout(false);
        }
    }
}
