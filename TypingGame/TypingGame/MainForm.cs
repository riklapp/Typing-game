using System;
using System.Drawing;
using System.Windows.Forms;

namespace RPGGame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ChangeResolution(1920, 1080, true);
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            game.Start();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Owner = this;
            settingsForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void ChangeResolution(int width, int height, bool isFullscreen)
        {
            if (isFullscreen)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.Size = new Size(width, height);
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.Size = new Size(width, height);
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }
    }
}
