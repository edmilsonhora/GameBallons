using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game_Testes_WPF.Domain
{
   public abstract class EntityBase 
    {
        public EntityBase(Canvas canvas)
        {
            Canvas = canvas;
        }

        public Canvas Canvas { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Rectangle Rect { get { return GetRectangle(); } }
        public Rect Area { get { return GetArea(); } }
        public bool EstaVivo { get; set; }
        public MediaPlayer MediaPlayer { get; set; }
        public abstract void Desenhar();

        protected Thickness Posicionar()
        {
            return new Thickness(this.X, this.Y, this.Width + this.X, this.Y + this.Height);
        }

        protected Rect GetArea()
        {
            return new Rect(new Point(this.X, this.Y), new Size(this.Width, this.Height));
        }

        protected Rectangle GetRectangle()
        {
            return new Rectangle
            {
                Height = this.Height,
                Width = this.Width,
            };
        }

    }
}
