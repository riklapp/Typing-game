using RPGGame;
using System.Drawing.Imaging;

public class MapLevel
{
    public int LevelNumber { get; set; }
    public Point Position { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsCurrent { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public Rectangle Bounds => new Rectangle(Position, new Size(100, 100));
    public Button LevelButton { get; set; }

    public delegate void LevelSelectedHandler(MapLevel selectedLevel);
    public event LevelSelectedHandler OnLevelSelected;

    private Func<Image, float, Bitmap> adjustBrightnessFunc;

    public MapLevel(int levelNumber, Point position, DifficultyLevel difficulty,
           Func<Image, float, Bitmap> brightnessAdjuster,
           bool isCurrent = false, bool isCompleted = false)
    {
        LevelNumber = levelNumber;
        Position = position;
        Difficulty = difficulty;
        IsCurrent = isCurrent;
        IsCompleted = isCompleted;
        this.adjustBrightnessFunc = brightnessAdjuster;

        CreateLevelButton();
    }

    private void CreateLevelButton()
    {
        LevelButton = new Button
        {
            Image = CreateLevelButtonImage(LevelNumber, IsCompleted, IsCurrent),
            Size = new Size(162, 163),
            Location = Position,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.Transparent,
            Tag = this,
            Enabled = IsCurrent || IsCompleted
        };

        LevelButton.FlatAppearance.BorderSize = 0;
        LevelButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        LevelButton.FlatAppearance.MouseDownBackColor = Color.Transparent;

        if (IsCurrent || IsCompleted)
        {
            LevelButton.MouseEnter += (s, e) => {
                LevelButton.Image = adjustBrightnessFunc(
                    Image.FromFile($@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btn{LevelNumber}.png"),
                    IsCompleted ? 1.4f : 1.2f);
            };

            LevelButton.MouseLeave += (s, e) => {
                LevelButton.Image = CreateLevelButtonImage(LevelNumber, IsCompleted, IsCurrent);
            };
        }

        LevelButton.Click += (s, e) => {
            if (IsCurrent || IsCompleted)
            {
                OnLevelSelected?.Invoke(this);
            }
        };
    }

    public Image CreateLevelButtonImage(int levelNumber, bool isCompleted, bool isCurrent)
    {
        var image = new Bitmap(162, 163);
        using (Graphics g = Graphics.FromImage(image))
        {
            var baseImage = Image.FromFile($@"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\btn{levelNumber}.png");

            if (!isCurrent && !isCompleted)
            {
                baseImage = SetImageOpacity(baseImage, 0.65f);
            }

            g.DrawImage(baseImage, new Rectangle(0, 0, 162, 163));
        }

        return isCompleted ? adjustBrightnessFunc(image, 2f) : image;
    }

    private Bitmap SetImageOpacity(Image image, float opacity)
    {
        Bitmap bmp = new Bitmap(image.Width, image.Height);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = opacity;
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height),
                        0, 0, image.Width, image.Height,
                        GraphicsUnit.Pixel, attributes);
        }
        return bmp;
    }
}