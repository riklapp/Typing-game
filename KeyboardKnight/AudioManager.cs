using System.Media;

namespace RPGGame
{
    public static class AudioManager
    {
        private static SoundPlayer menuMusicPlayer;
        private static SoundPlayer battleMusicPlayer;
        private static bool isInitialized = false;

        public static void Initialize()
        {
            if (!isInitialized)
            {
                menuMusicPlayer = new SoundPlayer(@"C:\Users\AdminPC\Desktop\Codes\KeyboardKnight\KeyboardKnight\Resources\Minstrels-Song.wav");
                battleMusicPlayer = new SoundPlayer(@"C:\Users\AdminPC\Desktop\Codes\KeyboardKnight\KeyboardKnight\Resources\Medieval-2-Total-War-—-Battle-in-Medieval.wav");
                isInitialized = true;
            }
        }

        public static void PlayMenuMusic()
        {
            StopAllMusic();
            menuMusicPlayer.PlayLooping();
        }

        public static void PlayBattleMusic()
        {
            StopAllMusic();
            battleMusicPlayer.PlayLooping();
        }

        public static void StopAllMusic()
        {
            menuMusicPlayer.Stop();
            battleMusicPlayer.Stop();
        }

        public static void Dispose()
        {
            StopAllMusic();
            menuMusicPlayer.Dispose();
            battleMusicPlayer.Dispose();
            isInitialized = false;
        }
    }
}