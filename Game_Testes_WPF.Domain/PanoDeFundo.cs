using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game_Testes_WPF.Domain
{
    public class PanoDeFundo : EntityBase
    {
        Rectangle rect;
        public PanoDeFundo(Canvas canvas) : base(canvas)
        {
            X = 0;
            Y = 0;
            Height = (int) canvas.Height;
            Width = (int)canvas.Width;
            rect = Rect;           
            rect.Fill = Helper.ObterImage(Media.PanoDeFundo);
            rect.Margin = Posicionar();
        }

       

        public override void Desenhar()
        {
            Canvas.Children.Add(rect);
        }


    }
}
