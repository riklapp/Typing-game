// WordBank.cs
using System.Collections.Generic;

namespace RPGGame
{
    public static class WordBank
    {
        public static List<string> GetWords(int difficultyLevel)
        {
            return difficultyLevel switch
            {
                0 => new List<string> { "sword", "shield", "arrow", "potion", "helmet", "boots", "gloves", "cloak" },
                1 => new List<string> { "battle", "charge", "defend", "victory", "combat", "strategy", "warrior", "soldier" },
                2 => new List<string> { "apocalypse", "annihilation", "cataclysm", "executioner", "obliterate", "vengeance", "armageddon", "destruction" },
                _ => new List<string> { "practice", "training", "exercise" }
            };
        }
    }
}