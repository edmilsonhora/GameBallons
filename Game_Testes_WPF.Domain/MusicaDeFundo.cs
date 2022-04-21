using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game_Testes_WPF.Domain
{
   public class MusicaDeFundo
    {
        MediaElement mediaPlayer;
        public MusicaDeFundo()
        {
            mediaPlayer = new MediaElement();
            mediaPlayer.Source = new Uri(Directory.GetCurrentDirectory() + "/Resources/MusicaGame.mp3");
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            Tocar();
        }

        private void Tocar()
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.LoadedBehavior = MediaState.Manual;
            mediaPlayer.Volume = 2;
            mediaPlayer.Play();
            mediaPlayer.UnloadedBehavior = MediaState.Manual;
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            Tocar();
        }
    }
}
