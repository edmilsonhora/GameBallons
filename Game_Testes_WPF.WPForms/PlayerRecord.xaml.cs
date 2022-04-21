using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game_Testes_WPF.WPForms
{
    /// <summary>
    /// Lógica interna para PlayerRecord.xaml
    /// </summary>
    public partial class PlayerRecord : Window
    {

        public string Nome { get; set; }
        public PlayerRecord()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtPlayerName.Text))
            {
                MessageBox.Show("Digite o nome do Jogador", "Nome Jogador", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            this.Nome = txtPlayerName.Text;
            this.Close();
        }
    }
}
