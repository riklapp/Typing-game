using System;
using System.Drawing;
using System.Windows.Forms;

namespace RPGGame
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cmbResolution.Items.Add("800x600");
            cmbResolution.Items.Add("1024x768");
            cmbResolution.Items.Add("1280x720");
            cmbResolution.Items.Add("1920x1080");
            cmbResolution.SelectedIndex = 3;

            chkFullscreen.Checked = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string selectedResolution = cmbResolution.SelectedItem.ToString();
            string[] dimensions = selectedResolution.Split('x');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);
e
            bool isFullscreen = chkFullscreen.Checked;

            MainForm mainForm = (MainForm)this.Owner;
            mainForm.ChangeResolution(width, height, isFullscreen);

            MessageBox.Show($"Resolution changed to {width}x{height} ({(isFullscreen ? "Fullscreen" : "Windowed")}).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
