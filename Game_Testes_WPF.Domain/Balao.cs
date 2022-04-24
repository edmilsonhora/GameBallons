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
    public class Balao : EntityBase
    {
        Rectangle rect;
        byte[][] baloes = { Media.Balao_1, Media.Balao_2, Media.Balao_3 };
        Random _radom;
        int velocidadeMovimento = 2;
        public Balao(Canvas canvas, Random random) : base(canvas)
        {
            this._radom = random;
            velocidadeMovimento = _radom.Next(3, 9);
            EstaVisival = true;
            EstaVivo = true;
            X = random.Next(400, (int)canvas.Width - 100);
            Y = (int)canvas.Height;
            Height = 100;
            Width = 70;
            rect = Rect;
            rect.Fill = Helper.ObterImage(baloes[random.Next(0, 3)]);
            rect.Margin = Posicionar();

            MediaPlayer = new MediaPlayer();
            MediaPlayer.Open(new Uri(Directory.GetCurrentDirectory() + "/Resources/Explosao.mp3"));
        }

        public bool EstaVisival { get; set; }

        public override void Desenhar()
        {
            if (!Canvas.Children.Contains(rect))
                Canvas.Children.Add(rect);
        }

        public void EstaNaTela()
        {
            if (this.Y <= -this.Height)
            {
                Canvas.Children.Remove(rect);
                EstaVivo = false;
            }
        }

        public void RemoveDoCanvas()
        {
            Canvas.Children.Remove(rect);
        }

        public void Esconder()
        {
            EstaVisival = false;
            rect.Visibility = Visibility.Hidden;
            RemoveDoCanvas();

        }

        public void TocarSom()
        {
            MediaPlayer.Play();


        }

        public void Mover()
        {
            this.Y -= velocidadeMovimento;
            rect.Margin = Posicionar();
            EstaNaTela();
        }


    }
}
