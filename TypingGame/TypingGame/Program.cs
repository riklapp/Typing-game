// Program.cs
using System;
using System.Windows.Forms;

namespace RPGGame
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameForm gameForm = new GameForm();
            Application.Run(gameForm);
        }
    }
}