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
            // Set default resolution to 1920x1080 in fullscreen
            ChangeResolution(1920, 1080, true);
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            // Start the game
            Game game = new Game();
            game.Start();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Open the settings form
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Owner = this; // Set the MainForm as the owner of the SettingsForm
            settingsForm.ShowDialog(); // Show the settings form as a modal dialog
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Exit the application
            Application.Exit();
        }

        // Method to change resolution and fullscreen mode
        public void ChangeResolution(int width, int height, bool isFullscreen)
        {
            if (isFullscreen)
            {
                // Set the form to fullscreen
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.Size = new Size(width, height);
            }
            else
            {
                // Set the form to windowed mode
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.Size = new Size(width, height);
                this.StartPosition = FormStartPosition.CenterScreen; // Center the form
            }
        }
    }
}
