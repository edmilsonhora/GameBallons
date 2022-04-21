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
    public class Especial : EntityBase
    {
        Rectangle rect;
        byte[][] baloes = { Media.Balao_1, Media.Balao_2, Media.Balao_3 };
        Random _random;
        int velocidadeMovimento = 2;
        public Especial(Canvas canvas, Random random) : base(canvas)
        {
            this._random = random;
            velocidadeMovimento = _random.Next(3, 9);
            EstaVivo = true;
            X = random.Next(400, (int)canvas.Width - 100);
            Y = (int)canvas.Height;
            Height = 100;
            Width = 70;
            rect = Rect;
            rect.Height = Height;
            rect.Width = Width;
            //rect.Fill = Helper.ObterImage(baloes[_random.Next(0, 2)]);
            rect.Margin = Posicionar();
        }

        public override void Desenhar()
        {
            if (!Canvas.Children.Contains(rect))
                Canvas.Children.Add(rect);
        }

        public void Sintilar()
        {
            rect.Fill = Helper.ObterImage(baloes[_random.Next(0, 2)]);
        }

        public void Mover()
        {
            this.Y -= velocidadeMovimento;
            rect.Margin = Posicionar();
            EstaNaTela();
        }

        public void EstaNaTela()
        {
            if (this.Y <= -this.Height)
            {
                Canvas.Children.Remove(rect);
                EstaVivo = false;
            }
        }
    }
}
