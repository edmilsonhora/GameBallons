using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game_Testes_WPF.Domain
{
    public class Arqueiro : EntityBase
    {
        Rectangle rect;
        int chao;
        int i;
        bool podeAtirar = true;
        byte[][] lista = { Media.Arc_0, Media.Arc_1, Media.Arc_2, Media.Arc_3 };
        Municao municao;
        bool fimDeJogo;
        public Arqueiro(Canvas canvas, Municao municao) : base(canvas)
        {
            X = 60;
            Y = 0;
            Height = 150;
            Width = 90;
            chao = (int)canvas.Height - 445;
            this.municao = municao;
            rect = Rect;
            rect.Height = Height;
            rect.Width = Width;
            rect.Fill = Helper.ObterImage(lista[i]);
            rect.Margin = Posicionar();
        }

        public bool EstaAtirando { get; set; }

        public override void Desenhar()
        {
            Canvas.Children.Add(rect);
        }

        public void Mover()
        {
            this.Y += 8;
            if (this.Y >= chao)
                this.Y = chao;

            rect.Margin = Posicionar();
        }

        public void Atirando()
        {
            if (!EstaAtirando) return;            
            rect.Fill = Helper.ObterImage(lista[i]);
            i++;
            if (i > 3)
            {
                EstaAtirando = false;
                i = 0;
            }                
        }

        public void Hide()
        {
            podeAtirar = false;           
            Canvas.Children.Remove(rect);
            Y = -100;
        }

        public void TemMunicao(List<Flecha> flechas)
        {
            if (fimDeJogo) return;
            if (municao.Total == 0 && flechas.Count == 0)
            {
                fimDeJogo = true;
                throw new ApplicationException("Acabou a Munição");
            }
                          

        }        

        public Flecha Atirar()
        {
            if (!podeAtirar) return null;
            EstaAtirando = true;
            municao.AtualizaMunicao();
            var flecha = new Flecha(Canvas);
            flecha.Desenhar();
            flecha.TocarSom();
            return flecha;
        }
    }
}
