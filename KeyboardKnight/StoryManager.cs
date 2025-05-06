using System.Drawing.Text;
using System.Text;

namespace RPGGame
{
    public class StoryManager : IDisposable
    {
        private static readonly Dictionary<int, string> LevelStories = new Dictionary<int, string>
        {
            {1, "As dawn breaks, you awaken in a tranquil glade, surrounded by ancient trees.\n\n" +
                "Your memory is shrouded in mist, yet a peculiar device catches your eye.\n" +
                "With a few curious buttons, you grasp it tightly, unaware of its true power.\n" +
                "In a moment of folly, you press a button, and a pesky mosquito falls lifeless to the ground.\n" +
                "Stepping forth from the woodland, you behold a meadow bathed in sunlight.\n" +
                "A distant tower rises above the treetops, yet your gaze is drawn to a creature of green, lurking in the underbrush.\n" +
                "Before you can react, it appears before you, intent on pilfering your belongings.\n\n" +
                "Your only weapon is this enchanted device.\n" +
                "Alas, time has not favored you to master its secrets..."},

            {2, "Fortune smiles upon you, for with this strange contraption, you vanquish the goblin.\n\n" +
                "With resolve, you set your sights on the tower you glimpsed earlier...\n\n" +
                "As you traverse the forest that separates you from your goal, a fearsome growl reverberates through the trees.\n" +
                "A colossal beast stands in your path, leaving no room for retreat.\n\n" +
                "It is time to test this device, which, as you discern from its markings, is known as a keyboard..."},

            {3, "Miraculously surviving the clash with the fearsome troll, you emerge from the forest\n" +
                "to behold the humble town of Eldermere, as indicated by a weathered sign.\n\n" +
                "A young lad, startled by your presence, flees but soon returns with a man named Guffaw.\n" +
                "He challenges you to a duel, for your armor projects an aura of intimidation.\n" +
                "As you walk toward the arena he speaks of, tales of his prowess fill the air.\n\n" +
                "Yet, his bravado fails to sway your confidence..."},

            {4, "After delivering a grievous blow to Guffaw,\n\n" +
                "you seek solace in the tavern adjacent to the arena, hoping to forge alliances.\n" +
                "You engage in conversation with a man clad in paladin's garb, the tavern's proprietor.\n" +
                "Upon hearing of your victory over Guffaw, he proposes a contest of strength,\n\n" +
                "and it is difficult to deny the challenge of a spirited paladin..."},

            {5, "The battle against the paladin proves formidable,\n\n" +
                "yet you emerge triumphant, even as he claims to have held back his full might.\n" +
                "Impressed by your skill with the keyboard, he invites you to the Keylisseum,\n" +
                "a grand arena shaped like a colossal circle.\n" +
                "He informs you that your name is already inscribed for battle; there is no turning back.\n" +
                "You watch as gladiators fall one after another.\n\n" +
                "The bell tolls, signaling that your moment has come..."},

            {6, "Having witnessed your extraordinary prowess, the paladin confides,\n\n" +
                "that Eldermere faces relentless assaults from the Dark Knight's sinister army.\n" +
                "He implores you to confront the Dark Knight and halt his reign of terror.\n\n" +
                "You know, without doubt, that you must put an end to this villain's machinations."},

            {7, "Just before you vanquish the Dark Knight,\n\n" +
                "he reveals that he serves a dreadful lord of dragons.\n" +
                "This ominous truth foretells that the plight of Eldermere is far from over.\n" +
                "Now, you set your sights on the Dragon Lord himself,\n\n" +
                "but as you traverse the ruins of a castle, a ghastly Necromancer emerges..."},

            {8, "The Necromancer proves a formidable adversary, as fearsome as the Dark Knight.\n" +
                "Yet, against all odds, you prevail..."
            },

            {9, "Atop Dragon Lord's Peak...\n\n" +
                "Your ultimate trial awaits at the summit of the mountain.\n" +
                "Prepare to face the winged terror that has ruled these lands for centuries."
            }
        };

        private static readonly Dictionary<int, string> LevelPostStories = new Dictionary<int, string>
        {
            {1, "The goblin collapses to the earth, its crude weapon clattering to the ground.\n\n" +
                "As you catch your breath, a faint glow beckons from the distant tower.\n" +
                "The path ahead appears clear, yet a whisper of foreboding suggests this is merely the beginning..."},

            {2, "The troll bellows a final, earth-shaking roar before crumbling like ancient stone.\n\n" +
                "Silence envelops the forest as you step over its massive form.\n" +
                "Ahead, the tower looms larger, its timeworn stones murmuring tales of battles yet to unfold."},

            {3, "Guffaw kneels in the dirt, his bravado shattered alongside his armor.\n\n" +
                "The townsfolk gaze in awe as you sheathe your weapon.\n" +
                "'Perhaps there is more to you than meets the eye,' whispers a hooded figure from the crowd..."},

            {4, "The paladin raises his tankard in salute, despite the wounds he bears.\n\n" +
                "'You fight with honor, stranger,' he proclaims between hearty gulps of ale.\n" +
                "'Come, allow me to show you where true warriors are tested...'"},

            {5, "The crowd erupts in cheers as your opponent concedes.\n\n" +
                "The paladin from the tavern nods approvingly from the stands.\n" +
                "'You have earned your place here,' he declares, 'but the greatest challenge still lies ahead...'"},

            {6, "The Dark Knight's armor shatters beneath your final blow.\n\n" +
                "As life fades from his eyes, he utters a final warning:\n" +
                "'He... is coming... the Dragon Lord...'"},

            {7, "The Necromancer's staff clatters to the ground, dark energy dissipating into the air.\n\n" +
                "From the ruins, distant wingbeats grow louder.\n" +
                "The final battle draws near..."},

            {8, "The Necromancer's final curse echoes through the ruins as he falls.\n\n" +
                "The ground trembles beneath your feet—not from magic, but from something far more ancient approaching..."},

            {9, "As the Dragon Lord's colossal form collapses, the very mountain seems to exhale in relief.\n\n" +
                "You stand victorious, yet a strange emptiness lingers.\n" +
                "Is this truly the end? Or merely the closing of one chapter...\n\n" +
                "Perhaps the real adventure begins now."}
        };

        private static readonly Dictionary<int, string> DeathMessage = new Dictionary<int, string>
        {
            [1] = "YOU DIED\n\n" +
          "The goblin's crude weapon finds its mark...\n\n" +
          "But your story isn't over yet. Try again!",
            [2] = "YOU DIED\n\n" +
          "The troll's massive club crushes your defenses...\n\n" +
          "The forest echoes with your failure. Will you rise again?",
            [3] = "YOU DIED\n\n" +
          "Guffaw's laughter fades as darkness takes you...\n\n" +
          "The townsfolk will remember this defeat.",
            [4] = "YOU DIED\n\n" +
          "The paladin's righteous strike proves too much...\n\n" +
          "The tavern's ale will grow stale without you.",
            [5] = "YOU DIED\n\n" +
          "The arena crowd roars as you fall...\n\n" +
          "Another nameless warrior forgotten in the dust.",
            [6] = "YOU DIED\n\n" +
          "The Dark Knight's blade drinks deep...\n\n" +
          "Eldermere's hope dies with you.",
            [7] = "YOU DIED\n\n" +
          "The Necromancer's dark magic consumes you...\n\n" +
          "Your bones will join his army.",
            [8] = "YOU DIED\n\n" +
          "The Dragon Lord's breath turns you to ash...\n\n" +
          "The mountain claims another victim.",
            [9] = "YOU DIED\n\n" +
          "The winged terror's claws rend your flesh...\n\n" +
          "Centuries of tyranny continue unchallenged."
        };

        private readonly PrivateFontCollection _privateFontCollection;
        private readonly Font _customFont;
        private bool _disposed;

        public StoryManager()
        {
            _privateFontCollection = new PrivateFontCollection();
            _privateFontCollection.AddFontFile(@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\alagard-12px-unicode.ttf");
            _customFont = new Font(_privateFontCollection.Families[0], 24, FontStyle.Regular);
        }

        public void ShowDeathScreen(Form parentForm, int levelNumber, Action onComplete)
        {
            ShowStory(parentForm, DeathMessage, levelNumber, onComplete);
        }

        public void ShowLevelStory(Form parentForm, int levelNumber, Action onComplete)
        {
            ShowStory(parentForm, LevelStories, levelNumber, onComplete);
        }

        public void ShowPostBattleStory(Form parentForm, int levelNumber, Action onComplete)
        {
            ShowStory(parentForm, LevelPostStories, levelNumber, onComplete);
        }

        private void ShowStory(Form parentForm, IReadOnlyDictionary<int, string> storyCollection,
                       int levelNumber, Action onComplete)
        {
            if (!storyCollection.TryGetValue(levelNumber, out var fullStory))
            {
                onComplete?.Invoke();
                return;
            }

            var storyPanel = new StoryPanel(_customFont, fullStory, onComplete);
            storyPanel.Tag = fullStory;
            parentForm.Controls.Add(storyPanel);
            storyPanel.BringToFront();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _customFont?.Dispose();
                _privateFontCollection?.Dispose();
            }

            _disposed = true;
        }
    }

    public sealed class StoryPanel : DoubleBufferedPanel
    {
        private readonly PictureBox _textDisplay;
        private readonly Label _promptLabel;
        private readonly System.Windows.Forms.Timer _typewriterTimer;
        private readonly StringBuilder _currentText = new StringBuilder();
        private readonly Action _onComplete;
        private int _currentCharIndex;
        private readonly Font _font;

        public StoryPanel(Font font, string fullStory, Action onComplete)
        {
            _font = font;
            _onComplete = onComplete;

            BackColor = Color.Black;
            Dock = DockStyle.Fill;

            _textDisplay = new PictureBox
            {
                BackColor = Color.Black,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Anchor = AnchorStyles.None
            };
            Controls.Add(_textDisplay);

            _promptLabel = new Label
            {
                Text = "Click anywhere to skip...",
                Font = _font,
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.BottomCenter,
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.Black
            };
            Controls.Add(_promptLabel);

            _typewriterTimer = new System.Windows.Forms.Timer { Interval = 30 };
            _typewriterTimer.Tick += OnTypewriterTick;
            _typewriterTimer.Start();

            Click += OnClick;
            _textDisplay.Click += OnClick;
            _promptLabel.Click += OnClick;
            Resize += OnResize;

            RenderText(fullStory);
        }

        private void OnTypewriterTick(object sender, EventArgs e)
        {
            string fullStory = Tag as string;
            if (fullStory == null || _currentCharIndex >= fullStory.Length)
            {
                _typewriterTimer.Stop();
                _promptLabel.Text = "Click anywhere to continue...";
                return;
            }

            _currentText.Append(fullStory[_currentCharIndex]);
            _currentCharIndex++;
            RenderText(_currentText.ToString());
        }

        private void OnClick(object sender, EventArgs e)
        {
            string fullStory = Tag as string;
            if (fullStory == null)
            {
                return;
            }

            if (_currentCharIndex < fullStory.Length)
            {
                _typewriterTimer.Stop();
                _currentText.Clear();
                _currentText.Append(fullStory);
                _currentCharIndex = fullStory.Length;
                RenderText(fullStory);
                _promptLabel.Text = "Click anywhere to continue...";
            }
            else
            {
                CleanUp();
                _onComplete?.Invoke();
            }
        }


        private void OnResize(object sender, EventArgs e)
        {
            if (_currentText.Length > 0)
                RenderText(_currentText.ToString());
        }

        private void RenderText(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            var proposedSize = new Size(ClientSize.Width - 100, int.MaxValue);
            var textSize = TextRenderer.MeasureText(text, _font, proposedSize,
                TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);

            using (var bmp = new Bitmap(
                Math.Max(1, textSize.Width + 20),
                Math.Max(1, textSize.Height + 20)))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Black);
                    TextRenderer.DrawText(g, text, _font,
                        new Point(10, 10), Color.White, Color.Black,
                        TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);
                }

                if (_textDisplay.Image != null)
                    _textDisplay.Image.Dispose();

                _textDisplay.Image = (Bitmap)bmp.Clone();
                _textDisplay.Location = new Point(
                    (ClientSize.Width - _textDisplay.Width) / 2,
                    (ClientSize.Height - _textDisplay.Height - _promptLabel.Height) / 2);
            }
        }

        private void CleanUp()
        {
            _typewriterTimer.Stop();
            _typewriterTimer.Dispose();

            if (_textDisplay.Image != null)
            {
                _textDisplay.Image.Dispose();
                _textDisplay.Image = null;
            }

            Parent?.Controls.Remove(this);
            Dispose();
        }
    }

    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.UserPaint, true);
            UpdateStyles();
        }
    }
}