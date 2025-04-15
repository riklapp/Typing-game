// Ability.cs
namespace RPGGame
{
    public class Ability
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Ability(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}