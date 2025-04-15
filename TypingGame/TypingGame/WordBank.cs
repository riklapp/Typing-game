// WordBank.cs

namespace RPGGame
{
    public static class WordBank
    {
        public static List<string> GetWords(int difficultyLevel)
        {
            return difficultyLevel switch
            {
                0 => new List<string> {
                    "king", "queen", "sword", "castle", "knight", "horse", "shield", "crown",
                    "dragon", "flag", "tavern", "axe", "bowl", "lance", "staff", "dungeon",
                    "page", "joust", "peasant", "tunic"
                },

                1 => new List<string> {
                    "chivalry", "catapult", "sorcerer", "trebuchet", "jester", "goblet",
                    "blacksmith", "courtier", "alchemist", "fiefdom", "heraldry", "quest",
                    "minstrel", "battalion", "garrison", "fortress", "vassal", "apothecary",
                    "scroll", "tapestry", "barracks"
                },

                2 => new List<string> {
                    "feudalism", "crusade", "inquisition", "serfdom", "monastery",
                    "chancellor", "vanguard", "diplomacy", "armament", "reconquista",
                    "constabulary", "retribution", "excommunication", "tournament",
                    "heretic", "scepter", "beseeching", "armory", "enchantment",
                    "cavalry", "diplomat"
                }
            };
        }
    }
}