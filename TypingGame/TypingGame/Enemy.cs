// Enemy.cs
using System.Drawing;

namespace RPGGame
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }
        public Color DisplayColor { get; set; }
        public string ImagePath { get; set; }

        public Enemy(DifficultyLevel difficulty, int levelNumber)
        {
            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    switch (levelNumber % 3) // 3 easy level enemies
                    {
                        case 0:
                            Name = "Goblin Grunt";
                            Health = 50 + (levelNumber * 5);
                            Damage = 8 + (levelNumber * 1);
                            DisplayColor = Color.LimeGreen;
                            ImagePath = "goblin.png";
                            break;
                        case 1:
                            Name = "Skeleton Warrior";
                            Health = 60 + (levelNumber * 5);
                            Damage = 7 + (levelNumber * 1);
                            DisplayColor = Color.White;
                            ImagePath = "skeleton.png";
                            break;
                        case 2:
                            Name = "Giant Rat";
                            Health = 40 + (levelNumber * 5);
                            Damage = 10 + (levelNumber * 1);
                            DisplayColor = Color.Brown;
                            ImagePath = "rat.png";
                            break;
                    }
                    break;

                case DifficultyLevel.Medium:
                    switch (levelNumber % 3) // 3 medium level enemies
                    {
                        case 0:
                            Name = "Orc Warrior";
                            Health = 100 + (levelNumber * 8);
                            Damage = 15 + (levelNumber * 2);
                            DisplayColor = Color.Orange;
                            ImagePath = "orc.png";
                            break;
                        case 1:
                            Name = "Dark Elf";
                            Health = 80 + (levelNumber * 8);
                            Damage = 18 + (levelNumber * 2);
                            DisplayColor = Color.Purple;
                            ImagePath = "elf.png";
                            break;
                        case 2:
                            Name = "Troll";
                            Health = 120 + (levelNumber * 8);
                            Damage = 12 + (levelNumber * 2);
                            DisplayColor = Color.Green;
                            ImagePath = "troll.png";
                            break;
                    }
                    break;

                case DifficultyLevel.Hard:
                    switch (levelNumber % 3) // 3 hard level enemies
                    {
                        case 0:
                            Name = "Dragon Lord";
                            Health = 200 + (levelNumber * 10);
                            Damage = 25 + (levelNumber * 3);
                            DisplayColor = Color.Red;
                            ImagePath = "dragon.png";
                            break;
                        case 1:
                            Name = "Demon";
                            Health = 180 + (levelNumber * 10);
                            Damage = 30 + (levelNumber * 3);
                            DisplayColor = Color.DarkRed;
                            ImagePath = "demon.png";
                            break;
                        case 2:
                            Name = "Lich King";
                            Health = 150 + (levelNumber * 10);
                            Damage = 35 + (levelNumber * 3);
                            DisplayColor = Color.DarkBlue;
                            ImagePath = "lich.png";
                            break;
                    }
                    break;

                default:
                    Name = "Training Dummy";
                    Health = 50;
                    Damage = 5;
                    DisplayColor = Color.Gray;
                    ImagePath = "dummy.png";
                    break;
            }

            MaxHealth = Health; // Set MaxHealth equal to starting Health
        }
    }
}