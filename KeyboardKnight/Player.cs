namespace RPGGame
{
    public class Player
    {
        public int Health { get; set; } = 100;
        public int MaxHealth { get; } = 100;
        public int Damage { get; set; } = 10;
        public int SkipWordCharges { get; set; } = 3;
        public int ExtraTimeCharges { get; set; } = 2;
        public int DoubleDamageCharges { get; set; } = 1;
    }
}