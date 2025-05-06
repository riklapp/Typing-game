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