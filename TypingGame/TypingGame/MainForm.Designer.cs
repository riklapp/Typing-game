namespace RPGGame
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnExit;

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
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));

            this.btnStartGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartGame.Text = "Start Game";
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);

            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSettings.Text = "Settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            this.tableLayoutPanel.Controls.Add(this.btnStartGame, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.btnSettings, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnExit, 0, 2);

            this.Controls.Add(this.tableLayoutPanel);

            this.Name = "MainForm";
            this.Text = "RPG Game";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
        }
    }
}
