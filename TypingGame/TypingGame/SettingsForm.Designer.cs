namespace RPGGame
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.CheckBox chkFullscreen;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.chkFullscreen = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();

            // TableLayoutPanel settings
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            // ComboBox for Resolution settings
            this.cmbResolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // CheckBox for Fullscreen settings
            this.chkFullscreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkFullscreen.Text = "Fullscreen";

            // Button settings
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Add controls to the TableLayoutPanel
            this.tableLayoutPanel.Controls.Add(this.cmbResolution, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.chkFullscreen, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnApply, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.btnCancel, 1, 2);

            // Add TableLayoutPanel to the Form
            this.Controls.Add(this.tableLayoutPanel);

            // Other form settings
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
        }
    }
}

