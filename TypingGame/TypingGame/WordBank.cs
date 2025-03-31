using System.Collections.Generic;

namespace RPGGame
{
    public static class WordBank
    {
        public static List<string> GetWords(int difficultyLevel)
        {
            // Return a list of words based on difficulty
            return new List<string> { "sword", "shield", "dragon" }; // Example words
        }
    }
}
