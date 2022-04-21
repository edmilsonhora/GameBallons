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
    public class Placar : EntityBase
    {
        TextBlock texto;

        public Placar(Canvas canvas) : base(canvas)
        {
            EstaVivo = true;
            X = 0;
            Y = 0;
           
            texto = new TextBlock();
            texto.FontFamily = new FontFamily("Arial");
            texto.FontSize = 25;
            texto.Foreground = new SolidColorBrush(Colors.Yellow);
            texto.Margin = new Thickness(this.X, this.Y, 0, 0);
            texto.Text = "Score: 0";
        }

        public int Pontos { get; set; }

        public override void Desenhar()
        {
            if (!Canvas.Children.Contains(texto))
                Canvas.Children.Add(texto);
        }

        public void MarcarPontos(int pontos)
        {
            Pontos += pontos;
            texto.Text = $"Score: {Pontos}";
        }

        public void Hide()
        {
            Canvas.Children.Remove(texto);
        }
    }
}
