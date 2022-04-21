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
    public class Municao : EntityBase
    {
        Rectangle rect;
        TextBlock texto;
        public Municao(Canvas canvas) : base(canvas)
        {
            Total = 50;
            X = 8;
            Y = (int) canvas.Height - 100;
            Height = 60;
            Width = 70;
            rect = Rect;
            rect.Height = Height;
            rect.Width = Width;
            rect.Fill = Helper.ObterImage(Media.Flechas);
            rect.Margin = Posicionar();

            texto = new TextBlock();
            texto.FontFamily = new FontFamily("Arial");
            texto.FontSize = 20;
            texto.Foreground = new SolidColorBrush(Colors.Black);
            texto.Margin = Posicionar();
            texto.Text = $"         {Total}";
        }

        public int Total { get; set; }

        public override void Desenhar()
        {
            Canvas.Children.Add(rect);
            Canvas.Children.Add(texto);
        }

        public void Hide()
        {
            Canvas.Children.Remove(texto);
            Canvas.Children.Remove(rect);
        }

        public void AtualizaMunicao()
        {
            Total--;
            texto.Text = $"        {Total}";
        }

        public void AtualizaMunicao(int qtd)
        {
            Total += qtd;
        }
    }
}
