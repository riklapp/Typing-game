using RPGGame;

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
        int enemyIndex = (levelNumber - 1) % 3;

        switch (difficulty)
        {
            case DifficultyLevel.Easy:
                switch (enemyIndex)
                {
                    case 0:
                        Name = "Wild Goblin";
                        Health = 50 + (levelNumber * 5);
                        Damage = 8 + (levelNumber * 1);
                        DisplayColor = Color.LimeGreen;
                        ImagePath = @"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Enemies\goblin.png";
                        break;
                    case 1:
                        Name = "Forest Troll";
                        Health = 60 + (levelNumber * 5);
                        Damage = 15 + (levelNumber * 1);
                        DisplayColor = Color.White;
                        ImagePath = @"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Enemies\troll.png";
                        break;
                    case 2:
                        Name = "Guffaw";
                        Health = 40 + (levelNumber * 5);
                        Damage = 6 + (levelNumber * 1);
                        DisplayColor = Color.Brown;
                        ImagePath = @"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Enemies\guffaw.png";
                        break;
                }
                break;

            case DifficultyLevel.Medium:
                switch (enemyIndex)
                {
                    case 0:
                        Name = "Drunk Paladin";
                        Health = 100 + (levelNumber * 10);
                        Damage = 15 + (levelNumber * 2);
                        DisplayColor = Color.Orange;
                        ImagePath = @"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Enemies\paladin.png";
                        break;
                    case 1:
                        Name = "Gladiator Champion";
                        Health = 80 + (levelNumber * 10);
                        Damage = 30 + (levelNumber * 2);
                        DisplayColor = Color.Purple;
                        ImagePath = @"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Enemies\gladiator.png";
                        break;
                    case 2:
                        Name = "Dark Knight";
                        Health = 120 + (levelNumber * 10);
                        Damage = 25 + (levelNumber * 2);
                        DisplayColor = Color.Green;
                        ImagePath = @"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Enemies\knight.png";
                        break;
                }
                break;

            case DifficultyLevel.Hard:
                switch (enemyIndex)
                {
                    case 0:
                        Name = "Necromancer";
                        Health = 200 + (levelNumber * 20);
                        Damage = 25 + (levelNumber * 3);
                        DisplayColor = Color.Red;
                        ImagePath = @"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Enemies\necromancer.png";
                        break;
                    case 1:
                        Name = "Poisonous Spider";
                        Health = 180 + (levelNumber * 20);
                        Damage = 45 + (levelNumber * 3);
                        DisplayColor = Color.DarkRed;
                        ImagePath = @"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Enemies\spider.png";
                        break;
                    case 2:
                        Name = "Dragon Lord";
                        Health = 250 + (levelNumber * 20);
                        Damage = 50 + (levelNumber * 3);
                        DisplayColor = Color.DarkBlue;
                        ImagePath = @"C:\Users\AdminPC\Desktop\TypingGame\TypingGame\Resources\Enemies\dragon.png";
                        break;
                }
                break;
        }

        MaxHealth = Health;
    }
}