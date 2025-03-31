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
            // Add common screen resolutions to the ComboBox
            cmbResolution.Items.Add("800x600");
            cmbResolution.Items.Add("1024x768");
            cmbResolution.Items.Add("1280x720");
            cmbResolution.Items.Add("1920x1080");
            cmbResolution.SelectedIndex = 3; // Default to 1920x1080

            // Set default fullscreen mode
            chkFullscreen.Checked = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Parse the selected resolution
            string selectedResolution = cmbResolution.SelectedItem.ToString();
            string[] dimensions = selectedResolution.Split('x');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);

            // Get the fullscreen mode
            bool isFullscreen = chkFullscreen.Checked;

            // Apply the resolution and fullscreen mode to the main form
            MainForm mainForm = (MainForm)this.Owner;
            mainForm.ChangeResolution(width, height, isFullscreen);

            MessageBox.Show($"Resolution changed to {width}x{height} ({(isFullscreen ? "Fullscreen" : "Windowed")}).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the settings form
        }
    }
}
