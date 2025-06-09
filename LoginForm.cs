using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class LoginForm : Form
    {
        private Dictionary<string, (string password, string nickname)> users = new Dictionary<string, (string, string)>()
        {
            { "user1", ("1234", "小明") },
            { "user2", ("1234", "小美") },
            { "user3", ("1234", "阿章") },
            { "N10170011", ("1234", "曾羽彰") },
            {"0000",("0000","使用者0000") },
            {"0001",("0001","使用者0001") }
        };

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string account = txtUserID.Text.Trim();
            string pwd = txtPassword.Text.Trim();

            if (users.ContainsKey(account) && users[account].password == pwd)
            {
                string nickname = users[account].nickname;

                this.Hide();
                ClientForm client = new ClientForm(nickname);
                client.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("帳號或密碼錯誤");
            }
        }
    }
}
