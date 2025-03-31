using System;

namespace RPGGame
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }

        public Player()
        {
            Health = 100;
            Damage = 15;
        }

        // Additional player methods
    }
}
