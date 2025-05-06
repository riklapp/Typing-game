using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
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
        private StoryManager storyManager;

        private Button btnAttack;
        private Label lblWordToType;
        private TextBox txtInput;
        private Label lblEnemyInfo;
        private Label lblPlayerInfo;
        private ProgressBar pbEnemyHealth;
        private ProgressBar pbPlayerHealth;
        private Button btnMenu;
        private OutlinedLabel lblSkipCharges;
        private OutlinedLabel lblTimeCharges;
        private OutlinedLabel lblDamageCharges;

        private ProgressBar pbTimeLeft;
        private Label lblTimeLeft;
        private System.Windows.Forms.Timer countdownTimer;
        private int remainingTime;

        private List<MapLevel> mapLevels = new List<MapLevel>();
        private int currentMapLevel = 0;
        private bool showMap = false;

        private readonly Point titlePosition = new Point(0, 0);
        private readonly Point playButtonPosition = new Point(840, 540);
        private readonly Point exitButtonPosition = new Point(840, 750);
        private readonly Point difficultyLabelPosition = new Point(840, 290);
        private readonly Point easyRadioPosition = new Point(840, 350);
        private readonly Point mediumRadioPosition = new Point(840, 410);
        private readonly Point hardRadioPosition = new Point(840, 470);

        public GameForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.player = new Player();
            this.storyManager = new StoryManager();
            this.Resize += GameForm_Resize;

            AudioManager.Initialize();
            AudioManager.PlayMenuMusic();

            InitializeMainMenu();
        }

        private Bitmap AdjustImageBrightness(Image image, float brightness)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                float[][] matrix = {
        new float[] {brightness, 0, 0, 0, 0},
        new float[] {0, brightness, 0, 0, 0},
        new float[] {0, 0, brightness, 0, 0},
        new float[] {0, 0, 0, 1, 0},
        new float[] {0, 0, 0, 0, 1}
    };

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(new ColorMatrix(matrix));

                g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, image.Width, image.Height,
                    GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }

        public class OutlinedLabel : Label
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                using (Font font = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style))
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x != 0 || y != 0)
                            {
                                e.Graphics.DrawString(this.Text, font, Brushes.Black,
                                    new PointF(x * 1.5f, y * 1.5f));
                            }
                        }
                    }
                    e.Graphics.DrawString(this.Text, font, new SolidBrush(this.ForeColor), PointF.Empty);
                }
            }
        }

        public class OutlinedRadioButton : RadioButton
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                this.OnPaintBackground(e);
                using (Font font = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style))
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x != 0 || y != 0)
                            {
                                e.Graphics.DrawString(this.Text, font, Brushes.Black,
                                    new PointF(20 + x * 1.5f, (this.Height - e.Graphics.MeasureString(this.Text, font).Height) / 2 + y * 1.5f));
                            }
                        }
                    }
                    e.Graphics.DrawString(this.Text, font, Brushes.White,
                        new PointF(20, (this.Height - e.Graphics.MeasureString(this.Text, font).Height) / 2));
                }

                Rectangle radioCircle = new Rectangle(0, (this.Height - 16) / 2, 16, 16);
                e.Graphics.FillEllipse(Brushes.Black, radioCircle);
                e.Graphics.DrawEllipse(new Pen(Color.White, 1), radioCircle);
                if (this.Checked)
                {
                    e.Graphics.FillEllipse(Brushes.White,
                        new Rectangle(4, (this.Height - 8) / 2, 8, 8));
                }
            }
        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            if (showMap)
            {
                InitializeMap();
            }
            else if (this.Controls.Count > 0 && this.Controls[0] is Button)
            {
                return;
            }
            else
            {
                InitializeMainMenu();
            }
        }

        private void InitializeMainMenu()
        {
            AudioManager.PlayMenuMusic();

            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }

            ClearAllControls();



            PrivateFontCollection privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\alagard-12px-unicode.ttf");

            FontFamily customFontFamily = privateFontCollection.Families[0];
            Font customFont = new Font(customFontFamily, 24, FontStyle.Regular);

            this.BackgroundImage = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\bgmain.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            PictureBox pbTitle = new PictureBox
            {
                Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Title.png"),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = titlePosition,
                BackColor = Color.Transparent
            };
            this.Controls.Add(pbTitle);

            OutlinedLabel lblSelectDifficulty = new OutlinedLabel
            {
                Text = "SELECT DIFFICULTY:",
                Font = customFont,
                ForeColor = Color.Gold,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            lblSelectDifficulty.Location = difficultyLabelPosition;
            this.Controls.Add(lblSelectDifficulty);

            int radioButtonWidth = 200;
            int radioButtonHeight = 40;
            int radioY = centerY - 50;

            selectedDifficulty = DifficultyLevel.Easy;

            OutlinedRadioButton rbEasy = new OutlinedRadioButton
            {
                Text = "EASY",
                Font = customFont,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Size = new Size(radioButtonWidth, radioButtonHeight),
                Checked = true,
                Tag = DifficultyLevel.Easy
            };
            rbEasy.Location = easyRadioPosition;
            rbEasy.CheckedChanged += (s, e) => {
                if (rbEasy.Checked)
                {
                    selectedDifficulty = DifficultyLevel.Easy;
                }
            };
            this.Controls.Add(rbEasy);

            OutlinedRadioButton rbMedium = new OutlinedRadioButton
            {
                Text = "MEDIUM",
                Font = customFont,
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Size = new Size(radioButtonWidth, radioButtonHeight),
                Tag = DifficultyLevel.Medium,
            };
            rbMedium.Location = mediumRadioPosition;
            rbMedium.CheckedChanged += (s, e) => {
                if (rbMedium.Checked)
                {
                    selectedDifficulty = DifficultyLevel.Medium;
                }
            };
            this.Controls.Add(rbMedium);

            OutlinedRadioButton rbHard = new OutlinedRadioButton
            {
                Text = "HARD",
                Font = customFont,
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Size = new Size(radioButtonWidth, radioButtonHeight),
                Tag = DifficultyLevel.Hard,
            };
            rbHard.Location = hardRadioPosition;
            rbHard.CheckedChanged += (s, e) => {
                if (rbHard.Checked)
                {
                    selectedDifficulty = DifficultyLevel.Hard;
                }
            };
            this.Controls.Add(rbHard);

            Button btnStartGame = new Button
            {
                Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnPlay.png"),
                Size = new Size(391, 199),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Location = playButtonPosition
            };
            btnStartGame.FlatAppearance.BorderSize = 0;
            btnStartGame.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnStartGame.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnStartGame.MouseEnter += (s, e) => btnStartGame.Image = AdjustImageBrightness(btnStartGame.Image, 1.2f);
            btnStartGame.MouseLeave += (s, e) => btnStartGame.Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnPlay.png");
            btnStartGame.Click += (s, e) => {
                this.Controls.Clear();
                InitializeMap();
            };
            this.Controls.Add(btnStartGame);

            Button btnExit = new Button
            {
                Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnExit.png"),
                Size = new Size(391, 199),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Location = exitButtonPosition
            };
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnExit.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnExit.MouseEnter += (s, e) => btnExit.Image = AdjustImageBrightness(btnExit.Image, 1.2f);
            btnExit.MouseLeave += (s, e) => btnExit.Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnExit.png");
            btnExit.Click += btnExit_Click;
            this.Controls.Add(btnExit);

            lblSelectDifficulty.BringToFront();
            rbEasy.BringToFront();
            rbMedium.BringToFront();
            rbHard.BringToFront();
            btnStartGame.BringToFront();
            btnExit.BringToFront();
        }

        private void InitializeMap()
        {
            showMap = true;
            ClearAllControls();

            this.BackgroundImage = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Map.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            int startX = this.ClientSize.Width / 2 - 880;
            int startY = this.ClientSize.Height / 3;
            int spacing = 200;

            if (mapLevels.Count == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    var difficulty = i < 3 ? DifficultyLevel.Easy :
                                    i < 6 ? DifficultyLevel.Medium : DifficultyLevel.Hard;

                    var level = new MapLevel(
                        levelNumber: i + 1,
                        position: new Point(startX + (i * spacing), startY),
                        difficulty: difficulty,
                        brightnessAdjuster: AdjustImageBrightness,
                        isCurrent: i == 0,
                        isCompleted: false);

                    level.OnLevelSelected += (selectedLevel) => {
                        showMap = false;
                        StartLevel(selectedLevel);
                    };

                    mapLevels.Add(level);
                }
            }

            foreach (var level in mapLevels)
            {
                if (this.Controls.Contains(level.LevelButton))
                {
                    this.Controls.Remove(level.LevelButton);
                }

                level.LevelButton = new Button
                {
                    Image = level.CreateLevelButtonImage(level.LevelNumber, level.IsCompleted, level.IsCurrent),
                    Size = new Size(162, 163),
                    Location = new Point(startX + ((level.LevelNumber - 1) * spacing), startY),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent,
                    Tag = level,
                    Enabled = level.IsCurrent || level.IsCompleted
                };

                level.LevelButton.FlatAppearance.BorderSize = 0;
                level.LevelButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
                level.LevelButton.FlatAppearance.MouseDownBackColor = Color.Transparent;

                if (level.IsCurrent || level.IsCompleted)
                {
                    level.LevelButton.MouseEnter += (s, e) => {
                        level.LevelButton.Image = AdjustImageBrightness(
                            Image.FromFile($@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btn{level.LevelNumber}.png"),
                            level.IsCompleted ? 1.4f : 1.2f);
                    };

                    level.LevelButton.MouseLeave += (s, e) => {
                        level.LevelButton.Image = level.CreateLevelButtonImage(level.LevelNumber, level.IsCompleted, level.IsCurrent);
                    };
                }

                level.LevelButton.Click += (s, e) => {
                    if (level.IsCurrent || level.IsCompleted)
                    {
                        showMap = false;
                        StartLevel(level);
                    }
                };

                this.Controls.Add(level.LevelButton);
                level.LevelButton.BringToFront();
            }

            Button btnMenu = new Button
            {
                Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Menu.png"),
                Size = new Size(391, 199),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Location = new Point(20, this.ClientSize.Height - 220)
            };

            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnMenu.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnMenu.MouseEnter += (s, e) => btnMenu.Image = AdjustImageBrightness(btnMenu.Image, 1.2f);
            btnMenu.MouseLeave += (s, e) => btnMenu.Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Menu.png");
            btnMenu.Click += (s, e) => {
                showMap = false;
                InitializeMainMenu();
            };

            this.Controls.Add(btnMenu);
            btnMenu.BringToFront();

            this.Invalidate();
        }

        public class DoubleBufferedPanel : Panel
        {
            public DoubleBufferedPanel()
            {
                this.DoubleBuffered = true;
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                             ControlStyles.AllPaintingInWmPaint |
                             ControlStyles.UserPaint, true);
                this.UpdateStyles();
            }
        }

        private void StartLevel(MapLevel level)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }

            showMap = false;
            currentMapLevel = level.LevelNumber - 1;
            selectedDifficulty = level.Difficulty;

            ClearAllControls();
            storyManager.ShowLevelStory(this, level.LevelNumber, () =>
            {
                SetupBattle();
            });
        }

        private Dictionary<DifficultyLevel, TimeSettings> difficultyTimeSettings = new Dictionary<DifficultyLevel, TimeSettings>()
        {
            {
                DifficultyLevel.Easy,
                new TimeSettings(
                    baseTime: 6000,
                    decayPerLevel: 50,
                    minTime: 3000
                )
            },
            {
                DifficultyLevel.Medium,
                new TimeSettings(
                    baseTime: 4500,
                    decayPerLevel: 75,
                    minTime: 2000
                )
            },
            {
                DifficultyLevel.Hard,
                new TimeSettings(
                    baseTime: 3500,
                    decayPerLevel: 100,
                    minTime: 1500
                )
            }
        };

        private class TimeSettings
        {
            public int BaseTime { get; }
            public int DecayPerLevel { get; }
            public int MinTime { get; }

            public TimeSettings(int baseTime, int decayPerLevel, int minTime)
            {
                BaseTime = baseTime;
                DecayPerLevel = decayPerLevel;
                MinTime = minTime;
            }
        }

        private int CalculateTimeLimit()
        {
            var settings = difficultyTimeSettings[selectedDifficulty];
            int calculatedTime = settings.BaseTime - (currentMapLevel * settings.DecayPerLevel);
            return Math.Max(settings.MinTime, calculatedTime);
        }

        private void SetupBattle()
        {
            AudioManager.PlayBattleMusic();

            PrivateFontCollection privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\alagard-12px-unicode.ttf");

            FontFamily customFontFamily = privateFontCollection.Families[0];
            Font customFont = new Font(customFontFamily, 24, FontStyle.Regular);

            this.BackgroundImage = Image.FromFile($@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\arena{currentMapLevel + 1}.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Dictionary<DifficultyLevel, int> baseTimeLimits = new Dictionary<DifficultyLevel, int>
            {
                { DifficultyLevel.Easy, 5000 },
                { DifficultyLevel.Medium, 4000 },
                { DifficultyLevel.Hard, 3500 }
            };

            timeLimit = baseTimeLimits[selectedDifficulty] - (currentMapLevel * 50);

            currentEnemy = new Enemy(selectedDifficulty, currentMapLevel + 1);
            player = new Player();
            words = WordBank.GetWords((int)selectedDifficulty);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = timeLimit;
            timer.Tick += Timer_Tick;

            countdownTimer = new System.Windows.Forms.Timer { Interval = 100 };
            countdownTimer.Tick += CountdownTimer_Tick;


            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            pbTimeLeft = new ProgressBar
            {
                Maximum = timeLimit,
                Value = timeLimit,
                Size = new Size(400, 20),
                ForeColor = Color.Cyan,
                BackColor = Color.Black,
                Style = ProgressBarStyle.Continuous,
                Visible = false
            };
            pbTimeLeft.Location = new Point(centerX - (pbTimeLeft.Width / 2), centerY + 150);
            this.Controls.Add(pbTimeLeft);

            lblTimeLeft = new OutlinedLabel
            {
                Text = $"{timeLimit / 1000f:0.0}s",
                Font = customFont,
                ForeColor = Color.White,
                AutoSize = true,
                BackColor = Color.Transparent,
                Visible = false
            };
            lblTimeLeft.Location = new Point(centerX - (lblTimeLeft.Width / 2) + 20, centerY + 280);
            this.Controls.Add(lblTimeLeft);

            countdownTimer = new System.Windows.Forms.Timer { Interval = 100 };
            countdownTimer.Tick += CountdownTimer_Tick;

            lblEnemyInfo = new OutlinedLabel
            {
                Text = $"{currentEnemy.Name.ToUpper()}",
                Font = customFont,
                ForeColor = Color.OrangeRed,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            lblEnemyInfo.Location = new Point(centerX - (lblEnemyInfo.Width / 2) + 400, centerY - 500);
            this.Controls.Add(lblEnemyInfo);

            pbEnemyHealth = new ProgressBar
            {
                Maximum = currentEnemy.MaxHealth,
                Value = currentEnemy.Health,
                Size = new Size(400, 30),
                ForeColor = Color.Red,
                BackColor = Color.Black
            };
            pbEnemyHealth.Location = new Point(centerX - (pbEnemyHealth.Width / 2) + 450, centerY - 450);
            this.Controls.Add(pbEnemyHealth);

            lblWordToType = new OutlinedLabel
            {
                Font = customFont,
                ForeColor = Color.White,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                BackColor = Color.Transparent
            };
            lblWordToType.Location = new Point(centerX - (lblWordToType.Width / 2), centerY + 50);
            this.Controls.Add(lblWordToType);

            txtInput = new TextBox
            {
                Font = customFont,
                Size = new Size(400, 50),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            txtInput.Location = new Point(centerX - (txtInput.Width / 2), centerY + 100);
            txtInput.TextChanged += TxtInput_TextChanged;
            this.Controls.Add(txtInput);

            btnAttack = new Button
            {
                Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Attack.png"),
                Size = new Size(664, 198),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent
            };
            btnAttack.FlatAppearance.BorderSize = 0;
            btnAttack.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnAttack.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnAttack.MouseEnter += (s, e) => btnAttack.Image = AdjustImageBrightness(btnAttack.Image, 1.2f);
            btnAttack.MouseLeave += (s, e) => btnAttack.Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Attack.png");
            btnAttack.Location = new Point(centerX - (btnAttack.Width / 2), this.ClientSize.Height - 220);
            btnAttack.Click += BtnAttack_Click;
            this.Controls.Add(btnAttack);

            lblPlayerInfo = new OutlinedLabel
            {
                Text = $"KNIGHT: {player.Health} HP",
                Font = customFont,
                ForeColor = Color.White,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            lblPlayerInfo.Location = new Point(centerX - (lblPlayerInfo.Width / 2) - 60, centerY + 200);
            this.Controls.Add(lblPlayerInfo);

            pbPlayerHealth = new ProgressBar
            {
                Maximum = player.MaxHealth,
                Value = player.Health,
                Size = new Size(400, 20),
                ForeColor = Color.LightGreen,
                BackColor = Color.Black
            };
            pbPlayerHealth.Location = new Point(centerX - (pbPlayerHealth.Width / 2), centerY + 250);
            this.Controls.Add(pbPlayerHealth);

            btnMenu = new Button
            {
                Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Menu.png"),
                Size = new Size(391, 199),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Location = new Point(10, this.ClientSize.Height - 220)
            };
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnMenu.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnMenu.MouseEnter += (s, e) => btnMenu.Image = AdjustImageBrightness(btnMenu.Image, 1.2f);
            btnMenu.MouseLeave += (s, e) => btnMenu.Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Menu.png");
            btnMenu.Click += (s, e) => InitializeMainMenu();
            this.Controls.Add(btnMenu);

            var btnSkipWord = new Button
            {
                Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnSkip.png"),
                Size = new Size(162, 163),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Tag = "Skip"
            };
            btnSkipWord.FlatAppearance.BorderSize = 0;
            btnSkipWord.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSkipWord.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSkipWord.MouseEnter += (s, e) => btnSkipWord.Image = AdjustImageBrightness(btnSkipWord.Image, 1.2f);
            btnSkipWord.MouseLeave += (s, e) => btnSkipWord.Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnSkip.png");
            btnSkipWord.Location = new Point(50, 100);
            btnSkipWord.Click += AbilityButton_Click;
            this.Controls.Add(btnSkipWord);

            var btnExtraTime = new Button
            {
                Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnShield.png"),
                Size = new Size(162, 163),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Tag = "Shield"
            };
            btnExtraTime.FlatAppearance.BorderSize = 0;
            btnExtraTime.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnExtraTime.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnExtraTime.MouseEnter += (s, e) => btnExtraTime.Image = AdjustImageBrightness(btnExtraTime.Image, 1.2f);
            btnExtraTime.MouseLeave += (s, e) => btnExtraTime.Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnShield.png");
            btnExtraTime.Location = new Point(50, 280);
            btnExtraTime.Click += AbilityButton_Click;
            this.Controls.Add(btnExtraTime);

            var btnDoubleDamage = new Button
            {
                Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnPower.png"),
                Size = new Size(162, 163),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Tag = "Power"
            };
            btnDoubleDamage.FlatAppearance.BorderSize = 0;
            btnDoubleDamage.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnDoubleDamage.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnDoubleDamage.MouseEnter += (s, e) => btnDoubleDamage.Image = AdjustImageBrightness(btnDoubleDamage.Image, 1.2f);
            btnDoubleDamage.MouseLeave += (s, e) => btnDoubleDamage.Image = Image.FromFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btnPower.png");
            btnDoubleDamage.Location = new Point(50, 460);
            btnDoubleDamage.Click += AbilityButton_Click;
            this.Controls.Add(btnDoubleDamage);

            lblSkipCharges = new OutlinedLabel
            {
                Text = $"{player.SkipWordCharges}",
                Font = customFont,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(220, 160)
            };
            this.Controls.Add(lblSkipCharges);

            lblTimeCharges = new OutlinedLabel
            {
                Text = $"{player.ExtraTimeCharges}",
                Font = customFont,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(220, 340)
            };
            this.Controls.Add(lblTimeCharges);

            lblDamageCharges = new OutlinedLabel
            {
                Text = $"{player.DoubleDamageCharges}",
                Font = customFont,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(220, 520)
            };
            this.Controls.Add(lblDamageCharges);

            btnAttack.Enabled = true;
        }

        private void BtnAttack_Click(object sender, EventArgs e)
        {
            if (!btnAttack.Enabled) return;

            currentWord = GetRandomWord();
            lblWordToType.Text = currentWord;
            lblWordToType.Location = new Point(this.ClientSize.Width / 2 - (lblWordToType.Width / 2), lblWordToType.Location.Y);

            txtInput.Enabled = true;
            txtInput.Text = "";
            txtInput.Focus();
            lblWordToType.Visible = true;

            pbTimeLeft.Visible = true;
            lblTimeLeft.Visible = true;

            remainingTime = timeLimit;
            pbTimeLeft.Maximum = timeLimit;
            pbTimeLeft.Value = remainingTime;
            lblTimeLeft.Text = $"{remainingTime / 1000f:0.0}s";

            timer.Start();
            countdownTimer.Start();
            btnAttack.Enabled = false;
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            remainingTime -= countdownTimer.Interval;

            int displayValue = Math.Min(remainingTime, pbTimeLeft.Maximum);
            pbTimeLeft.Value = Math.Max(0, displayValue);

            lblTimeLeft.Text = $"{remainingTime / 1000f:0.0}s";

            if (remainingTime < timeLimit * 0.25)
            {
                pbTimeLeft.ForeColor = Color.OrangeRed;

                if (remainingTime < timeLimit * 0.1)
                {
                    pbTimeLeft.ForeColor = remainingTime % 200 < 100 ? Color.Red : Color.OrangeRed;
                }
            }
            else
            {
                pbTimeLeft.ForeColor = Color.Cyan;
            }

            if (remainingTime <= 0)
            {
                timer.Stop();
                countdownTimer.Stop();
            }
        }

        private void AbilityButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var ability = button.Tag.ToString();

            switch (ability)
            {
                case "Skip":
                    if (player.SkipWordCharges > 0)
                    {
                        player.SkipWordCharges--;
                        SkipCurrentWord();
                    }
                    break;

                case "Shield":
                    if (player.ExtraTimeCharges > 0)
                    {
                        player.ExtraTimeCharges--;
                        AddExtraTime();
                    }
                    break;

                case "Power":
                    if (player.DoubleDamageCharges > 0)
                    {
                        player.DoubleDamageCharges--;
                        ActivateDoubleDamage();
                    }
                    break;
            }

            RefreshAbilityButtons();
        }

        private void SkipCurrentWord()
        {
            timer.Stop();
            countdownTimer.Stop();

            currentWord = GetRandomWord();
            lblWordToType.Text = currentWord;
            txtInput.Text = "";
            txtInput.Focus();

            remainingTime = CalculateTimeLimit();
            pbTimeLeft.Value = Math.Min(remainingTime, pbTimeLeft.Maximum);
            lblTimeLeft.Text = $"{remainingTime / 1000f:0.0}s";

            timer.Start();
            countdownTimer.Start();
        }

        private void AddExtraTime()
        {
            PrivateFontCollection privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\alagard-12px-unicode.ttf");

            FontFamily customFontFamily = privateFontCollection.Families[0];
            Font customFont = new Font(customFontFamily, 24, FontStyle.Regular);

            int extraTimeAmount = 0;

            switch (selectedDifficulty)
            {
                case DifficultyLevel.Easy:
                    extraTimeAmount = 2000;
                    break;
                case DifficultyLevel.Medium:
                    extraTimeAmount = 1500;
                    break;
                case DifficultyLevel.Hard:
                    extraTimeAmount = 1000;
                    break;
            }

            this.timer.Stop();
            countdownTimer.Stop();

            remainingTime += extraTimeAmount;

            if (remainingTime > pbTimeLeft.Maximum)
            {
                pbTimeLeft.Maximum = remainingTime;
            }

            pbTimeLeft.Value = Math.Min(remainingTime, pbTimeLeft.Maximum);
            lblTimeLeft.Text = $"{remainingTime / 1000f:0.0}s";

            this.timer.Start();
            countdownTimer.Start();

            var extraTimeLabel = new OutlinedLabel
            {
                Text = $"+{extraTimeAmount / 1000f:0.0} SECONDS",
                Font = customFont,
                ForeColor = Color.Cyan,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            extraTimeLabel.Location = new Point(
                this.ClientSize.Width / 2 - extraTimeLabel.Width / 2 - 70,
                lblWordToType.Location.Y - 50);
            this.Controls.Add(extraTimeLabel);

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
            PrivateFontCollection privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\alagard-12px-unicode.ttf");

            FontFamily customFontFamily = privateFontCollection.Families[0];
            Font customFont = new Font(customFontFamily, 24, FontStyle.Regular);

            int originalDamage = player.Damage;
            player.Damage *= 2;

            var damageLabel = new OutlinedLabel
            {
                Text = "2X DAMAGE!",
                Font = customFont,
                ForeColor = Color.Red,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            damageLabel.Location = new Point(
                this.ClientSize.Width / 2 - damageLabel.Width / 2 - 70,
                lblWordToType.Location.Y - 50);
            this.Controls.Add(damageLabel);

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
            var controlsToRemove = new List<Control>();

            foreach (Control control in this.Controls)
            {
                if (control is IDisposable disposable)
                {
                    controlsToRemove.Add(control);
                }
            }

            foreach (var control in controlsToRemove)
            {
                this.Controls.Remove(control);
                control.Dispose();
            }
        }

        private void RefreshAbilityButtons()
        {
            lblSkipCharges.Text = $"{player.SkipWordCharges}";
            lblTimeCharges.Text = $"{player.ExtraTimeCharges}";
            lblDamageCharges.Text = $"{player.DoubleDamageCharges}";

            foreach (Control control in this.Controls)
            {
                if (control is Button button && button.Tag != null)
                {
                    bool enableButton = false;

                    switch (button.Tag.ToString())
                    {
                        case "Skip":
                            enableButton = player.SkipWordCharges > 0;
                            break;
                        case "Shield":
                            enableButton = player.ExtraTimeCharges > 0;
                            break;
                        case "Power":
                            enableButton = player.DoubleDamageCharges > 0;
                            break;
                    }

                    button.Enabled = enableButton;
                    button.Image = enableButton ?
                        Image.FromFile($@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btn{button.Tag}.png") :
                        AdjustImageBrightness(Image.FromFile($@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btn{button.Tag}.png"), 0.5f);
                }
            }
        }

        private void TxtInput_TextChanged(object sender, EventArgs e)
        {
            if (txtInput.Text.ToLower() == currentWord.ToLower())
            {
                timer.Stop();
                countdownTimer.Stop();

                int damageDealt = player.Damage + random.Next(-2, 3);
                currentEnemy.Health -= Math.Max(1, damageDealt);
                pbEnemyHealth.Value = Math.Max(0, currentEnemy.Health);

                ShowBattleFeedback($"-{damageDealt}", Color.Red, new Point(lblEnemyInfo.Location.X, lblEnemyInfo.Location.Y + 100));

                if (currentEnemy.Health <= 0)
                {
                    mapLevels[currentMapLevel].IsCompleted = true;

                    player.SkipWordCharges += 1;
                    player.ExtraTimeCharges += 1;
                    if (currentMapLevel % 3 == 0)
                    {
                        player.DoubleDamageCharges += 1;
                    }

                    if (currentMapLevel < mapLevels.Count - 1)
                    {
                        mapLevels[currentMapLevel + 1].IsCurrent = true;
                    }

                    ClearAllControls();
                    storyManager.ShowPostBattleStory(this, currentMapLevel + 1, () =>
                    {
                        showMap = true;
                        InitializeMap();
                    });
                }
                else
                {
                    ResetRound();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            countdownTimer.Stop();
            timer.Interval = timeLimit;

            int enemyDamage = currentEnemy.Damage + random.Next(-2, 3);
            player.Health -= Math.Max(1, enemyDamage);
            pbPlayerHealth.Value = Math.Max(0, player.Health);
            lblPlayerInfo.Text = $"KNIGHT: {player.Health} HP";

            ShowBattleFeedback($"-{enemyDamage}", Color.OrangeRed, new Point(lblPlayerInfo.Location.X + 60, lblPlayerInfo.Location.Y - 200));

            pbPlayerHealth.Refresh();
            lblPlayerInfo.Refresh();

            if (player.Health <= 0)
            {
                ClearAllControls();
                storyManager.ShowDeathScreen(this, currentMapLevel + 1, () =>
                {
                    player = new Player();
                    showMap = true;
                    InitializeMap();
                });
            }
            else
            {
                ResetRound();
            }
        }

        private void ShowBattleFeedback(string text, Color color, Point location)
        {
            PrivateFontCollection privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\alagard-12px-unicode.ttf");

            FontFamily customFontFamily = privateFontCollection.Families[0];
            Font customFont = new Font(customFontFamily, 24, FontStyle.Regular);

            var feedbackLabel = new OutlinedLabel
            {
                Text = text,
                Font = customFont,
                ForeColor = color,
                AutoSize = true,
                BackColor = Color.Transparent,
                Location = location
            };
            this.Controls.Add(feedbackLabel);
            feedbackLabel.BringToFront();

            var feedbackTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            feedbackTimer.Tick += (s, e) => {
                this.Controls.Remove(feedbackLabel);
                feedbackTimer.Stop();
                feedbackTimer.Dispose();
            };
            feedbackTimer.Start();
        }

        private void ResetRound()
        {
            txtInput.Enabled = false;
            txtInput.Text = "";
            lblWordToType.Visible = false;
            btnAttack.Enabled = true;
            timer.Interval = timeLimit;

            pbTimeLeft.Visible = false;
            lblTimeLeft.Visible = false;

            countdownTimer.Stop();
            remainingTime = timeLimit;
            pbTimeLeft.Maximum = timeLimit;
            pbTimeLeft.Value = remainingTime;
            lblTimeLeft.Text = $"{timeLimit / 1000f:0.0}s";
            pbTimeLeft.ForeColor = Color.Cyan;
        }

        private string GetRandomWord()
        {
            return words[random.Next(words.Count)];
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            AudioManager.Dispose();
            Application.Exit();
        }
    }
}