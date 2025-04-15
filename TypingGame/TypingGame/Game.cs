// Game.cs
using System;
using System.Windows.Forms;

namespace RPGGame
{
    public class Game
    {
        public void Start()
        {
            ShowGameForm();
        }

        private void ShowGameForm()
        {
            GameForm gameForm = new GameForm();
            gameForm.ShowDialog();
        }
    }
}