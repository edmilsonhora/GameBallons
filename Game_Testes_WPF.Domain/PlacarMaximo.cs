using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game_Testes_WPF.Domain
{
    public class PlacarMaximo : EntityBase
    {
        TextBlock texto;
        public PlacarMaximo(Canvas canvas, int pontos) : base(canvas)
        {
            Pontos = pontos;
            EstaVivo = true;
            X = (int)canvas.Width - 210;
            Y = 0;

            texto = new TextBlock();
            texto.FontFamily = new FontFamily("Arial");
            texto.FontSize = 25;
            texto.Foreground = new SolidColorBrush(Colors.Blue);
            texto.Text = $"Hige Score: {Pontos}";
            texto.Margin = new Thickness(this.X, this.Y, 0, 0);
            
        }

        public int Pontos { get; set; }

        public override void Desenhar()
        {
            Canvas.Children.Add(texto);
        }

        public void AtualizarPontos(int pontos)
        {
            Pontos = pontos;
            texto.Text = $"Hige Score: {Pontos}";
        }

        public void Hide()
        {
            Canvas.Children.Remove(texto);
        }
    }
}
