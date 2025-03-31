﻿using System;

namespace RPGGame
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }

        public Enemy()
        {
            Name = "Goblin"; // Example enemy
            Health = 100;
            Damage = 10;
        }

        // Additional enemy methods
    }
}
