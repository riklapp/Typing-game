// MapLevel.cs
using System.Drawing;

namespace RPGGame
{
    public class MapLevel
    {
        public int LevelNumber { get; set; }
        public Point Position { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCurrent { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public Rectangle Bounds => new Rectangle(Position, new Size(60, 60));

        // Updated constructor with all 5 parameters
        public MapLevel(int levelNumber, Point position, DifficultyLevel difficulty,
                       bool isCurrent = false, bool isCompleted = false)
        {
            LevelNumber = levelNumber;
            Position = position;
            Difficulty = difficulty;
            IsCurrent = isCurrent;
            IsCompleted = isCompleted;
        }

        public void Draw(Graphics g)
        {
            var brush = IsCurrent ? Brushes.Gold :
                       IsCompleted ? Brushes.LimeGreen : Brushes.White;

            g.FillEllipse(brush, Bounds);
            g.DrawEllipse(new Pen(Color.Black, 2), Bounds);

            var font = new Font("Arial", 12, FontStyle.Bold);
            var text = LevelNumber.ToString();
            var size = g.MeasureString(text, font);

            g.DrawString(text, font, Brushes.Black,
                Position.X + (Bounds.Width - size.Width) / 2,
                Position.Y + (Bounds.Height - size.Height) / 2);
        }
    }
}