using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class ChatHistoryForm : Form
    {
        public ChatHistoryForm()
        {
            InitializeComponent();
        }

        public void AddMessage(string message)
        {
            if (InvokeRequired)
                Invoke(new Action(() => listHistory.Items.Add(message)));
            else
                listHistory.Items.Add(message);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listHistory.Items.Clear();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文字檔 (*.txt)|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(sfd.FileName, listHistory.Items.Cast<string>());
                MessageBox.Show("紀錄已成功匯出");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文字檔 (*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(ofd.FileName);
                listHistory.Items.Clear();
                listHistory.Items.AddRange(lines);
                MessageBox.Show("紀錄已成功匯入");
            }
        }
    }
}
