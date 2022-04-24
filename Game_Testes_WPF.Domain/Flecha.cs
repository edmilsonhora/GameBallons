using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game_Testes_WPF.Domain
{
    public class Flecha : EntityBase
    {
        Rectangle rect;
        bool fimDeJogo;
        public Flecha(Canvas canvas) : base(canvas)
        {
            EstaVivo = true;
            X = 69;
            Y = (int)canvas.Height - 400;
            Height = 15;
            Width = 46;
            rect = Rect;            
            rect.Fill = Helper.ObterImage(Media.Flecha);
            rect.Margin = Posicionar();
        }

        public override void Desenhar()
        {
            if (!Canvas.Children.Contains(rect))
                Canvas.Children.Add(rect);
        }

        public void EstaNaTela()
        {
            if (this.X >= Canvas.Width + this.Width)
            {
                Canvas.Children.Remove(rect);
                EstaVivo = false;
            }
        }

        public void Hide()
        {
            Canvas.Children.Remove(rect);
            EstaVivo = false;
        }

        public void Mover()
        {
            this.X += 10;
            rect.Margin = Posicionar();
            EstaNaTela();
        }

        public void EstaColidindoCom(List<Balao> baloes, Placar placar)
        {
            foreach (var balao in baloes)
            {
                if (balao.Area.Contains(this.Area) && balao.EstaVisival)
                {
                    balao.TocarSom();
                    this.EstaVivo = false;                     
                    balao.Esconder();
                    Canvas.Children.Remove(this.rect);
                    placar.MarcarPontos(10);
                }
            }
        }

        public void EstaColidindoCom(List<Especial> especiais)
        {
            foreach (var especial in especiais)
            {
                if (fimDeJogo) break;
                if (especial.Area.Contains(this.Area))
                {
                    fimDeJogo = true;
                    throw new ApplicationException("Acertou balão especial");
                }
                   
            }
        }

        internal void TocarSom()
        {
            MediaPlayer = new MediaPlayer();
            MediaPlayer.Open(new Uri(Directory.GetCurrentDirectory() + "/Resources/Som_Flecha.mp3"));
            MediaPlayer.Play();
            
        }
    }
}
