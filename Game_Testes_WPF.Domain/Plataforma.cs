using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game_Testes_WPF.Domain
{
    public class Plataforma : EntityBase
    {
        Rectangle rect;
        public Plataforma(Canvas canvas) : base(canvas)
        {
            X = -30;
            Y = (int)canvas.Height - 310;
            Height = 150;
            Width = 290; 
            rect = Rect;
            rect.Height = Height;
            rect.Width = Width;
            rect.Fill = Helper.ObterImage(Media.Plataforma);
            rect.Margin = Posicionar();
        }

        public override void Desenhar()
        {
            Canvas.Children.Add(rect);
        }
    }
}
