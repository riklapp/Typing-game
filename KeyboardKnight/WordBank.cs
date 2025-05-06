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
                "page", "joust", "peasant", "tunic", "armor", "squire", "spear", "fable",
                "golem", "witch", "basil", "forge", "herbs", "prince", "witch", "beast",
                "cider", "druid", "plume", "frost", "flame", "bloom", "forge", "glade",
                "quest", "sword", "sling", "stone", "troll", "goblin", "merlin", "cabin"
            },

                1 => new List<string> {
                "chivalry", "catapult", "sorcerer", "trebuchet", "jester", "goblet",
                "blacksmith", "courtier", "alchemist", "fiefdom", "heraldry", "quest",
                "minstrel", "battalion", "garrison", "fortress", "vassal", "apothecary",
                "scroll", "tapestry", "barracks", "knapsack", "tournament", "diplomat",
                "outlawed", "conjurer", "garrison", "invasion", "fortified", "ambushed",
                "mystical", "adventurer", "faction", "expedition", "goblins", "guardian",
                "heraldry", "vanguard", "serfdom", "bastion", "hallowed", "fletching"
            },

                2 => new List<string> {
                "feudalism", "crusade", "inquisition", "serfdom", "monastery",
                "chancellor", "vanguard", "diplomacy", "armament", "reconquista",
                "constabulary", "retribution", "excommunication", "tournament",
                "heretic", "scepter", "beseeching", "armory", "enchantment",
                "cavalry", "diplomat", "insurrection", "armistice", "conspiracy",
                "nefarious", "conqueror", "cataclysm", "overthrow", "desecration",
                "enlightenment", "transgression", "revolution", "dominion", "expedition",
                "retribution", "fortification", "proclamation", "insurrection", "sacrilege",
                "inquisition", "subjugation", "intercession", "reconciliation", "persecution",
                "excommunication", "diplomatic", "intervention", "incantation", "resurrection"
            }
            };
        }
    }
}