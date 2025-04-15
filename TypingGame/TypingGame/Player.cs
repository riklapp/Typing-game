// Player.cs
public class Player
{
    public string Name { get; set; } = "Hero";
    public int Health { get; set; } = 100;
    public int MaxHealth { get; set; } = 100;
    public int Damage { get; set; } = 10;
    public int Score { get; set; }
    public int EnemiesDefeated { get; set; }
    public int Level { get; set; } = 1;
    public int CurrentLevel { get; set; } = 1;
    public int MaxLevelReached { get; set; } = 1;

    public int SkipWordCharges { get; set; } = 3;
    public int ExtraTimeCharges { get; set; } = 3;
    public int DoubleDamageCharges { get; set; } = 1;

    public void CompleteLevel()
    {
        CurrentLevel++;
        if (CurrentLevel > MaxLevelReached)
        {
            MaxLevelReached = CurrentLevel;
        }
    }
}