using System.Media;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game_Testes_WPF.Domain
{
    public class GameOver : EntityBase
    {
        Rectangle rect;
        public GameOver(Canvas canvas) : base(canvas)
        {
            Height = 350;
            Width = 490;
            X = (int)(canvas.Width / 2) - Width / 2;
            Y = (int)(canvas.Height / 2) - Height / 2;
            

            rect = Rect;
            rect.Height = Height;
            rect.Width = Width;
            rect.Fill = Helper.ObterImage(Media.GameOver);
            rect.Margin = Posicionar();

            SystemSounds.Beep.Play();
        }

        public override void Desenhar()
        {
            Canvas.Children.Add(rect);
        }

        public void Hide()
        {
            Canvas.Children.Remove(rect);
        }
    }
}
