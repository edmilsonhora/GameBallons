using Game_Testes_WPF.API;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game_Testes_WPF.Domain
{

    public class HallDaFama : EntityBase
    {
        TextBlock texto;
        public HallDaFama(Canvas canvas, List<Record> records) : base(canvas)
        {
            EstaVivo = true;
            X = (int)canvas.Width;
            Y = 30;
            Height = 280;
            Width = 390;

            texto = new TextBlock();
            texto.Padding = new System.Windows.Thickness(13);
            texto.LineHeight = 25;
            texto.Width = Width;
            texto.Height = Height;
            texto.FontFamily = new FontFamily("Arial");
            texto.FontSize = 12;
            texto.Text = ObterLista(records);
            texto.Background = Brushes.White;

            texto.Margin = Posicionar();
        }

        private string ObterLista(List<Record> records)
        {
            StringBuilder sb = new StringBuilder();                    

            int i = 1;
            foreach (var item in records.OrderByDescending(x => x.Pontos))
            {
                var nome = item.Nome.Length < 20 ? (item.Nome + new string(' ', 20 - item.Nome.Length)) : item.Nome;
                sb.AppendLine($"{i}.º - {nome}\t{item.Pontos}\t{item.Data}\t{item.TempoDeJogo}");
                i++;
            }
            return sb.ToString();
        }

        public override void Desenhar()
        {
            Canvas.Children.Add(texto);
        }

        public void Hide()
        {
            Canvas.Children.Remove(texto);
        }

        public void Mover()
        {

            if (X >= 590)
            {
                X -= 5;
                texto.Margin = Posicionar();
            }
        }
    }
}
