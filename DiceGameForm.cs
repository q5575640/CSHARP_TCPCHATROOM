using System;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class DiceGameForm : Form
    {
        private ClientForm clientForm;
        private string nickname;
        private Random rand = new Random();
        private bool myChoiceMade = false;
        private bool opponentReady = false;

        public DiceGameForm(ClientForm clientForm, string nickname)
        {
            InitializeComponent();
            this.clientForm = clientForm;
            this.nickname = nickname;
            this.Text = $"比大小遊戲 - {nickname}";
        }

        public void ResetButton()
        {
            myChoiceMade = false;
            opponentReady = false;
            btnRoll.Enabled = true;
            lblStatus.Text = "請擲骰子";
        }

        public void SetOpponentReady()
        {
            opponentReady = true;
            UpdateStatus();
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            int point = rand.Next(1, 7);
            lblStatus.Text = $"你擲出了 {point} 點";
            clientForm.SendChoice(point.ToString());
            myChoiceMade = true;
            btnRoll.Enabled = false;
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (myChoiceMade)
            {
                lblStatus.Text += opponentReady ? "，等待比對結果..." : "，等待對手擲骰";
                if(myChoiceMade == opponentReady)
                {
                    lblStatus.Text += "結果出爐";
                    this.Close();
                }
            }
        }
    }
}
