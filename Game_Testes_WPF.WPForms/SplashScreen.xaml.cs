using Game_Testes_WPF.API;
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
using System.Windows.Threading;

namespace Game_Testes_WPF.WPForms
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        Task task;
        DispatcherTimer timer;
        public SplashScreen()
        {
            InitializeComponent();
            txtCopyYear.Text = $"ESH © - 2022 / {DateTime.Now.Year}";
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (task.IsCompleted)
            {
                timer.Stop();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide();
            }
        }

        private void Iniciar()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                ApiAzure apiAzure = new ApiAzure();
                var min = apiAzure.GetMin();
            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {                

            task = Task.Run(() => Iniciar());
            timer.Start();
                    

        }
    }
}
