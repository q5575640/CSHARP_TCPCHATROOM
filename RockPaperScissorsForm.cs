using System;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class RockPaperScissorsForm : Form
    {
        private ClientForm clientForm;
        private string nickname;
        private bool myChoiceMade = false;
        private bool opponentReady = false;

        public RockPaperScissorsForm(ClientForm clientForm, string nickname)
        {
            InitializeComponent();
            this.clientForm = clientForm;
            this.nickname = nickname;
            this.Text = $"猜拳遊戲 - {nickname}";
        }

        public void ResetButtons()
        {
            myChoiceMade = false;
            opponentReady = false;
            btnRock.Enabled = btnPaper.Enabled = btnScissors.Enabled = true;
            lblStatus.Text = "請選擇你的出拳";
        }

        public void SetOpponentReady()
        {
            opponentReady = true;
            UpdateStatus();
        }

        private void SendChoice(string choice)
        {
            clientForm.SendChoice(choice);
            myChoiceMade = true;
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (myChoiceMade)
            {
                lblStatus.Text = opponentReady ? "等待比對結果..." : "已選擇，等待對手出拳";
                if (myChoiceMade == opponentReady)
                {
                    lblStatus.Text += "結果出爐";
                    this.Close();
                }
            }
        }

        private void btnRock_Click(object sender, EventArgs e)
        {
            SendChoice("ROCK");
            btnRock.Enabled = btnPaper.Enabled = btnScissors.Enabled = false;
        }

        private void btnPaper_Click(object sender, EventArgs e)
        {
            SendChoice("PAPER");
            btnRock.Enabled = btnPaper.Enabled = btnScissors.Enabled = false;
        }

        private void btnScissors_Click(object sender, EventArgs e)
        {
            SendChoice("SCISSORS");
            btnRock.Enabled = btnPaper.Enabled = btnScissors.Enabled = false;
        }
    }
}
