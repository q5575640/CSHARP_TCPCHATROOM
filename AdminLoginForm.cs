using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class AdminLoginForm : Form
    {
        private Dictionary<string, (string password, string nickname)> admins = new Dictionary<string, (string, string)>()
        {
            { "admin1", ("1234", "管理員1") },
            { "admin2", ("1234", "管理員2") },
            { "admin3", ("1234", "管理員2") },
            { "N10170011", ("1234", "曾羽彰") },
            {"0000",("0000","管理員0000") },
            {"0001",("0001","管理員0001") },
            {"admin",("1234","管理員") }
        };

        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string account = txtUserID.Text.Trim();
            string pwd = txtPassword.Text.Trim();

            if (admins.ContainsKey(account) && admins[account].password == pwd)
            {
                this.Hide();
                ServerForm server = new ServerForm(admins[account].nickname); // 帶入管理員名稱
                server.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("帳號或密碼錯誤");
            }
        }
    }
}
