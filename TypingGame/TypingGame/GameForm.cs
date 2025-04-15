using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RPGGame
{
    public partial class GameForm : Form
    {
        private DifficultyLevel selectedDifficulty;
        private List<string> words;
        private Random random = new Random();
        private string currentWord;
        private System.Windows.Forms.Timer timer;
        private int timeLimit;

        private Enemy currentEnemy;
        private Player player;

        // UI Controls
        private Button btnAttack;
        private Label lblWordToType;
        private TextBox txtInput;
        private Label lblEnemyInfo;
        private Label lblPlayerInfo;
        private ProgressBar pbEnemyHealth;
        private ProgressBar pbPlayerHealth;
        private Button btnMenu;

        // New fields for map levels
        private List<MapLevel> mapLevels = new List<MapLevel>();
        private int currentMapLevel = 0;
        private bool showMap = false;

        public GameForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.player = new Player();
            this.Resize += GameForm_Resize; // Add resize handler
            InitializeMainMenu();
        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            // Reinitialize current screen when window is resized
            if (showMap)
            {
                InitializeMap();
            }
            else if (this.Controls.Count > 0 && this.Controls[0] is Button) // Check if in battle
            {
                SetupBattle();
            }
            else
            {
                InitializeMainMenu();
            }
        }

        private void InitializeMainMenu()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }

            ClearAllControls(); // Use our safe clear method

            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            // Title Label
            Label lblTitle = new Label
            {
                Text = "KEYBOARD KNIGHT",
                Font = new Font("Arial", 36, FontStyle.Bold),
                ForeColor = Color.Gold,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblTitle.Location = new Point(centerX - (lblTitle.Width / 2), centerY - 200);
            this.Controls.Add(lblTitle);

            // Difficulty Selection
            Label lblSelectDifficulty = new Label
            {
                Text = "SELECT DIFFICULTY:",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };
            lblSelectDifficulty.Location = new Point(centerX - (lblSelectDifficulty.Width / 2), centerY - 100);
            this.Controls.Add(lblSelectDifficulty);

            // Difficulty Radio Buttons
            int radioButtonWidth = 200;
            int radioButtonHeight = 40;

            RadioButton rbEasy = new RadioButton
            {
                Text = "EASY",
                Font = new Font("Arial", 14),
                ForeColor = Color.LimeGreen,
                Size = new Size(radioButtonWidth, radioButtonHeight),
                Checked = true,
                Tag = DifficultyLevel.Easy
            };
            rbEasy.Location = new Point(centerX - (radioButtonWidth / 2), centerY - 50);
            rbEasy.CheckedChanged += (s, e) => { if (rbEasy.Checked) selectedDifficulty = DifficultyLevel.Easy; };
            this.Controls.Add(rbEasy);

            RadioButton rbMedium = new RadioButton
            {
                Text = "MEDIUM",
                Font = new Font("Arial", 14),
                ForeColor = Color.Orange,
                Size = new Size(radioButtonWidth, radioButtonHeight),
                Tag = DifficultyLevel.Medium
            };
            rbMedium.Location = new Point(centerX - (radioButtonWidth / 2), centerY);
            rbMedium.CheckedChanged += (s, e) => { if (rbMedium.Checked) selectedDifficulty = DifficultyLevel.Medium; };
            this.Controls.Add(rbMedium);

            RadioButton rbHard = new RadioButton
            {
                Text = "HARD",
                Font = new Font("Arial", 14),
                ForeColor = Color.Red,
                Size = new Size(radioButtonWidth, radioButtonHeight),
                Tag = DifficultyLevel.Hard
            };
            rbHard.Location = new Point(centerX - (radioButtonWidth / 2), centerY + 50);
            rbHard.CheckedChanged += (s, e) => { if (rbHard.Checked) selectedDifficulty = DifficultyLevel.Hard; };
            this.Controls.Add(rbHard);

            selectedDifficulty = DifficultyLevel.Easy;

            // Start Game Button
            Button btnStartGame = new Button
            {
                Text = "START GAME",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.DarkGreen,
                Size = new Size(300, 80),
                FlatStyle = FlatStyle.Flat
            };
            btnStartGame.FlatAppearance.BorderSize = 0;
            btnStartGame.Location = new Point(centerX - (btnStartGame.Width / 2), centerY + 120);
            btnStartGame.Click += (s, e) => {
                this.Controls.Clear();
                InitializeMap();
            };
            this.Controls.Add(btnStartGame);

            // Exit Button
            Button btnExit = new Button
            {
                Text = "EXIT",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.DarkRed,
                Size = new Size(300, 80),
                FlatStyle = FlatStyle.Flat
            };
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.Location = new Point(centerX - (btnExit.Width / 2), centerY + 220);
            btnExit.Click += btnExit_Click;
            this.Controls.Add(btnExit);

            // Set background
            this.BackColor = Color.FromArgb(30, 30, 40);
        }

        private void InitializeMap()
        {
            showMap = true;
            mapLevels.Clear();
            currentMapLevel = 0;

            ClearAllControls(); // Clear existing controls first

            int startX = this.ClientSize.Width / 2 - 300; // Centered horizontally
            int startY = this.ClientSize.Height / 3; // Start 1/3 down the screen
            int spacing = 120;

            for (int i = 0; i < 10; i++)
            {
                var difficulty = i < 3 ? DifficultyLevel.Easy :
                                i < 7 ? DifficultyLevel.Medium : DifficultyLevel.Hard;

                // Use the constructor with all parameters
                mapLevels.Add(new MapLevel(
                    levelNumber: i + 1,
                    position: new Point(startX + (i * spacing), startY),
                    difficulty: difficulty,
                    isCurrent: i == 0,  // First level is current
                    isCompleted: false));
            }

            this.Invalidate();

            // Add "Back to Menu" button
            var btnMenu = new Button
            {
                Text = "MAIN MENU",
                Font = new Font("Arial", 14),
                ForeColor = Color.White,
                BackColor = Color.DarkSlateBlue,
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat
            };
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.Location = new Point(20, this.ClientSize.Height - 80); // Fixed position
            btnMenu.Click += (s, e) => {
                var result = MessageBox.Show("Return to main menu? Current battle progress will be lost.",
                                           "Confirm Exit",
                                           MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Clear map state
                    showMap = false;
                    mapLevels.Clear();

                    if (timer != null)
                    {
                        timer.Stop();
                        timer.Dispose();
                        timer = null;
                    }

                    InitializeMainMenu();
                }
            };
            this.Controls.Add(btnMenu);

            this.Invalidate(); // Force redraw
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (showMap)
            {
                // Draw title
                var title = "CHOOSE YOUR PATH";
                var font = new Font("Arial", 24, FontStyle.Bold);
                var size = e.Graphics.MeasureString(title, font);
                e.Graphics.DrawString(title, font, Brushes.Gold,
                    (this.ClientSize.Width - size.Width) / 2,
                    50);

                // Draw connections between levels
                for (int i = 0; i < mapLevels.Count - 1; i++)
                {
                    var start = new Point(
                        mapLevels[i].Bounds.Right,
                        mapLevels[i].Bounds.Top + (mapLevels[i].Bounds.Height / 2));

                    var end = new Point(
                        mapLevels[i + 1].Bounds.Left,
                        mapLevels[i + 1].Bounds.Top + (mapLevels[i + 1].Bounds.Height / 2));

                    e.Graphics.DrawLine(IsLevelUnlocked(i + 1) ? Pens.White : Pens.Gray, start, end);
                }

                // Draw levels
                foreach (var level in mapLevels)
                {
                    level.Draw(e.Graphics);
                }

                // Draw player info
                e.Graphics.DrawString($"Score: {player.Score}\nLevels Completed: {mapLevels.Count(l => l.IsCompleted)}/{mapLevels.Count}",
                    new Font("Arial", 16),
                    Brushes.White,
                    this.ClientSize.Width - 250,
                    50);
            }
        }

        private bool IsLevelUnlocked(int levelNumber)
        {
            if (levelNumber == 1) return true;
            return mapLevels[levelNumber - 2].IsCompleted;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (showMap)
            {
                foreach (var level in mapLevels)
                {
                    if (level.Bounds.Contains(e.Location))
                    {
                        if (level.IsCurrent ||
                           (level.LevelNumber == currentMapLevel + 1 && mapLevels[currentMapLevel].IsCompleted))
                        {
                            showMap = false; // Hide the map immediately
                            this.Invalidate(); // Force redraw to remove map
                            StartLevel(level);
                            return;
                        }
                    }
                }
            }
        }

        private void StartLevel(MapLevel level)
        {
            // Clean up existing timer if any
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }

            showMap = false;
            currentMapLevel = level.LevelNumber - 1;
            selectedDifficulty = level.Difficulty;

            ClearAllControls();

            SetupBattle();
        }

        private void SetupBattle()
        {
            this.BackColor = Color.FromArgb(30, 30, 40);

            timeLimit = selectedDifficulty switch
            {
                DifficultyLevel.Easy => 5000,
                DifficultyLevel.Medium => 3500,
                DifficultyLevel.Hard => 2000,
                _ => 3000
            };

            // Create enemy with current level number
            currentEnemy = new Enemy(selectedDifficulty, currentMapLevel + 1);
            player = new Player();
            words = WordBank.GetWords((int)selectedDifficulty);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = timeLimit;
            timer.Tick += Timer_Tick;

            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            lblEnemyInfo = new Label
            {
                Text = $"{currentEnemy.Name.ToUpper()}",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.OrangeRed,
                AutoSize = true
            };
            lblEnemyInfo.Location = new Point(centerX - (lblEnemyInfo.Width / 2), centerY - 200);
            this.Controls.Add(lblEnemyInfo);

            pbEnemyHealth = new ProgressBar
            {
                Maximum = currentEnemy.MaxHealth,
                Value = currentEnemy.Health,
                Size = new Size(400, 30),
                ForeColor = Color.Red
            };
            pbEnemyHealth.Location = new Point(centerX - (pbEnemyHealth.Width / 2), centerY - 150);
            this.Controls.Add(pbEnemyHealth);

            lblWordToType = new Label
            {
                Font = new Font("Arial", 28, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            lblWordToType.Location = new Point(centerX - (lblWordToType.Width / 2), centerY - 50);
            this.Controls.Add(lblWordToType);

            txtInput = new TextBox
            {
                Font = new Font("Arial", 20),
                Size = new Size(400, 50),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center
            };
            txtInput.Location = new Point(centerX - (txtInput.Width / 2), centerY);
            txtInput.TextChanged += TxtInput_TextChanged;
            this.Controls.Add(txtInput);

            btnAttack = new Button
            {
                Text = "ATTACK!",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.DarkRed,
                Size = new Size(300, 80),
                FlatStyle = FlatStyle.Flat
            };
            btnAttack.FlatAppearance.BorderSize = 0;
            btnAttack.Location = new Point(centerX - (btnAttack.Width / 2), centerY + 80);
            btnAttack.Click += BtnAttack_Click;
            this.Controls.Add(btnAttack);

            lblPlayerInfo = new Label
            {
                Text = $"KNIGHT: {player.Health} HP",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.LightBlue,
                AutoSize = true
            };
            lblPlayerInfo.Location = new Point(centerX - (lblPlayerInfo.Width / 2), centerY + 180);
            this.Controls.Add(lblPlayerInfo);

            pbPlayerHealth = new ProgressBar
            {
                Maximum = player.MaxHealth,
                Value = player.Health,
                Size = new Size(400, 20),
                ForeColor = Color.LightGreen
            };
            pbPlayerHealth.Location = new Point(centerX - (pbPlayerHealth.Width / 2), centerY + 220);
            this.Controls.Add(pbPlayerHealth);

            btnMenu = new Button
            {
                Text = "MAIN MENU",
                Font = new Font("Arial", 14),
                ForeColor = Color.White,
                BackColor = Color.DarkSlateBlue,
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat
            };
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.Location = new Point(centerX - (btnMenu.Width / 2), this.ClientSize.Height - 100);
            btnMenu.Click += (s, e) => InitializeMainMenu();
            this.Controls.Add(btnMenu);

            var btnSkipWord = new Button
            {
                Text = $"SKIP WORD ({player.SkipWordCharges})",
                Font = new Font("Arial", 12),
                ForeColor = Color.White,
                BackColor = Color.Purple,
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                Tag = "skip"
            };
            btnSkipWord.FlatAppearance.BorderSize = 0;
            btnSkipWord.Location = new Point(50, 100);
            btnSkipWord.Click += AbilityButton_Click;
            this.Controls.Add(btnSkipWord);

            var btnExtraTime = new Button
            {
                Text = $"EXTRA TIME ({player.ExtraTimeCharges})",
                Font = new Font("Arial", 12),
                ForeColor = Color.White,
                BackColor = Color.Blue,
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                Tag = "time"
            };
            btnExtraTime.FlatAppearance.BorderSize = 0;
            btnExtraTime.Location = new Point(50, 150);
            btnExtraTime.Click += AbilityButton_Click;
            this.Controls.Add(btnExtraTime);

            var btnDoubleDamage = new Button
            {
                Text = $"2X DAMAGE ({player.DoubleDamageCharges})",
                Font = new Font("Arial", 12),
                ForeColor = Color.White,
                BackColor = Color.DarkRed,
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                Tag = "damage"
            };
            btnDoubleDamage.FlatAppearance.BorderSize = 0;
            btnDoubleDamage.Location = new Point(50, 200);
            btnDoubleDamage.Click += AbilityButton_Click;
            this.Controls.Add(btnDoubleDamage);

            // Start first attack immediately
            BtnAttack_Click(null, EventArgs.Empty);
            RefreshAbilityButtons();
        }

        private void BtnAttack_Click(object sender, EventArgs e)
        {
            currentWord = GetRandomWord();
            lblWordToType.Text = currentWord;
            lblWordToType.Location = new Point(this.ClientSize.Width / 2 - (lblWordToType.Width / 2), lblWordToType.Location.Y);

            txtInput.Enabled = true;
            txtInput.Text = "";
            txtInput.Focus();
            lblWordToType.Visible = true;

            timer.Start();
            btnAttack.Enabled = false;
        }

        private void AbilityButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var ability = button.Tag.ToString();

            switch (ability)
            {
                case "skip":
                    if (player.SkipWordCharges > 0)
                    {
                        player.SkipWordCharges--;
                        SkipCurrentWord();
                    }
                    break;

                case "time":
                    if (player.ExtraTimeCharges > 0)
                    {
                        player.ExtraTimeCharges--;
                        AddExtraTime();
                    }
                    break;

                case "damage":
                    if (player.DoubleDamageCharges > 0)
                    {
                        player.DoubleDamageCharges--;
                        ActivateDoubleDamage();
                    }
                    break;
            }

            RefreshAbilityButtons(); // Update all ability buttons after any ability is used
        }

        private void SkipCurrentWord()
        {
            timer.Stop();
            currentWord = GetRandomWord();
            lblWordToType.Text = currentWord;
            txtInput.Text = "";
            txtInput.Focus();
            timer.Start();
        }

        private void AddExtraTime()
        {
            this.timer.Stop(); // Use 'this.timer' to refer to the field
            timeLimit += 3000;
            this.timer.Interval = timeLimit;
            this.timer.Start();

            // Visual feedback
            var extraTimeLabel = new Label
            {
                Text = "+3 SECONDS",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.Cyan,
                AutoSize = true
            };
            extraTimeLabel.Location = new Point(
                this.ClientSize.Width / 2 - extraTimeLabel.Width / 2,
                lblWordToType.Location.Y - 50);
            this.Controls.Add(extraTimeLabel);

            // Use System.Windows.Forms.Timer explicitly
            var feedbackTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            feedbackTimer.Tick += (s, e) => {
                this.Controls.Remove(extraTimeLabel);
                feedbackTimer.Stop();
                feedbackTimer.Dispose();
            };
            feedbackTimer.Start();
        }

        private void ActivateDoubleDamage()
        {
            int originalDamage = player.Damage;
            player.Damage *= 2;

            // Visual feedback
            var damageLabel = new Label
            {
                Text = "2X DAMAGE!",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = true
            };
            damageLabel.Location = new Point(
                this.ClientSize.Width / 2 - damageLabel.Width / 2,
                lblEnemyInfo.Location.Y + 40);
            this.Controls.Add(damageLabel);

            // Use System.Windows.Forms.Timer explicitly
            var damageTimer = new System.Windows.Forms.Timer { Interval = 5000 };
            damageTimer.Tick += (s, e) => {
                player.Damage = originalDamage;
                this.Controls.Remove(damageLabel);
                damageTimer.Stop();
                damageTimer.Dispose();
            };
            damageTimer.Start();
        }

        private void ClearAllControls()
        {
            // Suspend layout to prevent flickering
            this.SuspendLayout();

            // Create a list to avoid modification during iteration
            var controlsToRemove = this.Controls.Cast<Control>().ToList();

            foreach (var control in controlsToRemove)
            {
                control.Dispose();
            }

            this.Controls.Clear();
            this.ResumeLayout();

            // Force immediate cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void RefreshAbilityButtons()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button && button.Tag != null)
                {
                    switch (button.Tag.ToString())
                    {
                        case "skip":
                            button.Text = $"SKIP WORD ({player.SkipWordCharges})";
                            button.Enabled = player.SkipWordCharges > 0;
                            break;
                        case "time":
                            button.Text = $"EXTRA TIME ({player.ExtraTimeCharges})";
                            button.Enabled = player.ExtraTimeCharges > 0;
                            break;
                        case "damage":
                            button.Text = $"2X DAMAGE ({player.DoubleDamageCharges})";
                            button.Enabled = player.DoubleDamageCharges > 0;
                            break;
                    }
                }
            }
        }

        private void TxtInput_TextChanged(object sender, EventArgs e)
        {
            if (txtInput.Text.ToLower() == currentWord.ToLower())
            {
                timer.Stop();

                currentEnemy.Health -= player.Damage;
                pbEnemyHealth.Value = Math.Max(0, currentEnemy.Health);

                if (currentEnemy.Health <= 0)
                {
                    // Grant ability charges when defeating enemies
                    player.SkipWordCharges += 1;
                    player.ExtraTimeCharges += 1;
                    if (currentMapLevel % 3 == 0) // Every 3 levels
                    {
                        player.DoubleDamageCharges += 1;
                    }

                    RefreshAbilityButtons();

                    player.EnemiesDefeated++;
                    player.Score += (int)selectedDifficulty * 100;

                    // Mark current level as completed
                    mapLevels[currentMapLevel].IsCompleted = true;

                    // Unlock next level if available
                    if (currentMapLevel < mapLevels.Count - 1)
                    {
                        mapLevels[currentMapLevel + 1].IsCurrent = true;
                    }

                    if (timer != null && timer.Enabled)
                    {
                        timer.Stop();
                    }

                    // Return to map
                    showMap = true;
                    this.Controls.Clear();
                    this.Invalidate(); // Force redraw to show map

                    MessageBox.Show($"You defeated the {currentEnemy.Name}!\n" +
                                  $"Level {currentMapLevel + 1} completed!\n" +
                                  $"Score: {player.Score}",
                                  "Victory!",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    MessageBox.Show($"You defeated the {currentEnemy.Name}!\n" +
                  $"Level {currentMapLevel + 1} completed!\n" +
                  $"Score: {player.Score}",
                  "Victory!",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"You hit the {currentEnemy.Name} for {player.Damage} damage!",
                                  "Attack!",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    ResetRound();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // First stop the timer to prevent re-entrancy
            timer.Stop();

            // Check if we're still in battle (might have exited to menu)
            if (!showMap && this.Controls.Contains(lblEnemyInfo))
            {
                player.Health -= currentEnemy.Damage;
                pbPlayerHealth.Value = Math.Max(0, player.Health);
                lblPlayerInfo.Text = $"KNIGHT: {player.Health} HP";

                pbPlayerHealth.Refresh();
                lblPlayerInfo.Refresh();

                if (player.Health <= 0)
                {
                    MessageBox.Show($"The {currentEnemy.Name} defeated you!\nFinal Score: {player.Score}",
                                    "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    InitializeMainMenu();
                }
                else
                {
                    MessageBox.Show($"The {currentEnemy.Name} hit you for {currentEnemy.Damage} damage!",
                                    "Counterattack!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ResetRound();
                }
            }
        }

        private void ResetRound()
        {
            txtInput.Enabled = false;
            txtInput.Text = "";
            lblWordToType.Visible = false;
            btnAttack.Enabled = true;
        }

        private string GetRandomWord()
        {
            return words[random.Next(words.Count)];
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }
    }
}