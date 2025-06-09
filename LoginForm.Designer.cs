namespace TCPGameChatProject
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUserID
            this.lblUserID.Location = new System.Drawing.Point(30, 30);
            this.lblUserID.Size = new System.Drawing.Size(100, 20);
            this.lblUserID.Text = "使用者帳號：";
            // 
            // txtUserID
            this.txtUserID.Location = new System.Drawing.Point(130, 30);
            this.txtUserID.Size = new System.Drawing.Size(150, 22);
            // 
            // lblPassword
            this.lblPassword.Location = new System.Drawing.Point(30, 70);
            this.lblPassword.Size = new System.Drawing.Size(100, 20);
            this.lblPassword.Text = "密碼：";
            // 
            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(130, 70);
            this.txtPassword.Size = new System.Drawing.Size(150, 22);
            this.txtPassword.PasswordChar = '*';
            // 
            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(100, 110);
            this.btnLogin.Size = new System.Drawing.Size(120, 30);
            this.btnLogin.Text = "登入";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // LoginForm
            this.ClientSize = new System.Drawing.Size(320, 170);
            this.Controls.Add(this.lblUserID);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Name = "LoginForm";
            this.Text = "使用者登入";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
