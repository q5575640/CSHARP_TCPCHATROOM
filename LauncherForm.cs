using System;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class LauncherForm : Form
    {
        public LauncherForm()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminLoginForm adminLogin = new AdminLoginForm();
            adminLogin.ShowDialog();
            this.Show();
            this.Close();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm clientLogin = new LoginForm();
            clientLogin.ShowDialog();
            this.Show();
            this.Close();
        }
    }
}
