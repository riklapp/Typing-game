using System.Collections.Generic;

namespace RPGGame
{
    public static class WordBank
    {
        public static List<string> GetWords(int difficultyLevel)
        {
            return new List<string> { "sword", "shield", "dragon" };
        }
    }
}
