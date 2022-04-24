using Game_Testes_WPF.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Text.Json;
using System.Configuration;
using System.Windows.Media;
using System.IO;
using Game_Testes_WPF.API;

namespace Game_Testes_WPF.WPForms
{

    public partial class MainWindow : Window
    {

        Stopwatch sw;
        DispatcherTimer gameTimer;
        DispatcherTimer baloesTimer;
        DispatcherTimer especiaisTimer;
        Random random;
        Canvas canvas;
        PanoDeFundo panoDeFundo;
        MusicaDeFundo musicaDeFundo;
        Placar placar;
        PlacarMaximo placarMaximo;
        Plataforma plataforma;
        Arqueiro arqueiro;
        List<Balao> baloes;
        List<Flecha> flechas;
        List<Especial> especiais;
        Municao municao;
        Record record;
        GameOver gameOver;
        HallDaFama hallDaFama;
        int intervaloBaloes;
        int intervaloEspeciais;
        int[] pontosParaPremioMunicao = { 100, 300, 600, 1200, 2400 };
        bool atualizouMunicao;
        int checkPoint;
        int recordMaximo;

        public MainWindow()
        {
            InitializeComponent();
            sw = new Stopwatch();
            random = new Random();
            canvas = new Canvas();
            canvas.Height = this.Height;
            canvas.Width = this.Width;
            panoDeFundo = new PanoDeFundo(canvas);
            plataforma = new Plataforma(canvas);
            baloes = new List<Balao>();
            flechas = new List<Flecha>();
            especiais = new List<Especial>();
            this.AddChild(canvas);
            panoDeFundo.Desenhar();
            plataforma.Desenhar();
            musicaDeFundo = new MusicaDeFundo();

            gameTimer = new DispatcherTimer(DispatcherPriority.Render);
            gameTimer.Interval = TimeSpan.FromMilliseconds(16.6666);
            gameTimer.Tick += GameTimer_Tick;

            baloesTimer = new DispatcherTimer(DispatcherPriority.Render);
            baloesTimer.Interval = TimeSpan.FromMilliseconds(30);
            baloesTimer.Tick += BaloesTimer_Tick;

            especiaisTimer = new DispatcherTimer(DispatcherPriority.Render);
            especiaisTimer.Interval = TimeSpan.FromMilliseconds(100);
            especiaisTimer.Tick += EspeciaisTimer_Tick;

            StartGame();

        }

        private void StartGame()
        {
            hallDaFama?.Hide();
            hallDaFama = null;
            gameOver?.Hide();
            gameOver = null;

            municao = new Municao(canvas);
            arqueiro = new Arqueiro(canvas, municao);

            placar = new Placar(canvas);
            placarMaximo?.Hide();
            placarMaximo = null;
            placarMaximo = new PlacarMaximo(canvas, ObterPlacarMaximo());
            arqueiro.Desenhar();
            placar.Desenhar();
            placarMaximo.Desenhar();
            municao.Desenhar();
            gameTimer.Stop();
            gameTimer.Start();
            baloesTimer.Start();
            especiaisTimer.Start();
            sw.Start();
        }

        private int ObterPlacarMaximo()
        {

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings != null)
                {
                    Record r = JsonSerializer.Deserialize<Record>(appSettings["record"]);
                    recordMaximo = r.Pontos;
                }
            }
            catch { }


            return recordMaximo;
        }

        private void EspeciaisTimer_Tick(object sender, EventArgs e)
        {
            if (intervaloEspeciais <= 0)
            {
                especiais.Add(new Especial(canvas, random));
                especiais.ForEach(p => p.Desenhar());
                intervaloEspeciais = random.Next(15, 55);
            }
            intervaloEspeciais--;
        }

        private void BaloesTimer_Tick(object sender, EventArgs e)
        {
            arqueiro.Atirando();
            if (intervaloBaloes <= 0)
            {
                baloes.Add(new Balao(canvas, random));
                baloes.ForEach(p => p.Desenhar());
                intervaloBaloes = random.Next(10, 50);
            }
            intervaloBaloes--;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                baloes.RemoveAll(x => !x.EstaVivo);
                flechas.RemoveAll(x => !x.EstaVivo);

                flechas.ForEach(p => p.Mover());
                baloes.ForEach(p => p.Mover());

                flechas.ForEach(p => p.EstaColidindoCom(baloes, placar));

                if (placar.Pontos > recordMaximo)
                    placarMaximo.AtualizarPontos(placar.Pontos);

                if (pontosParaPremioMunicao.Contains(placar.Pontos) && !atualizouMunicao)
                {
                    municao.AtualizaMunicao(placar.Pontos / 4);
                    checkPoint = placar.Pontos;
                    atualizouMunicao = true;
                }
                else if ((placar.Pontos != checkPoint) && atualizouMunicao)
                {
                    atualizouMunicao = false;
                }                             

                foreach (Especial especial in especiais)
                {
                    especial.Sintilar();
                    especial.Mover();
                }

                flechas.ForEach(p => p.EstaColidindoCom(especiais));

                arqueiro.Mover();
                arqueiro.TemMunicao(flechas);
                hallDaFama?.Mover();

            }
            catch
            {

                gameOver = new GameOver(canvas);
                flechas.ForEach(p => p.Hide());
                arqueiro.Hide();
                municao.Hide();

                baloesTimer.Stop();
                especiaisTimer.Stop();
                gameOver.Desenhar();
                sw.Stop();
                ObterDadosParaHallDaFama();

            }

        }

        private void SalvarDadosEmAPIHallDaFama(ApiAzure apiAzure)
        {
            try
            {
                int minPontos = apiAzure.GetMin();
                if (placar.Pontos > minPontos)
                {
                    apiAzure.Salvar(record);
                }

            }
            catch { }

        }

        private void ObterDadosParaHallDaFama()
        {
            if (placar.Pontos <= recordMaximo) return;

            var d = new PlayerRecord();
            d.ShowDialog();

            record = new Record();
            record.Nome = d.Nome;
            record.Game = "ballons";
            record.Pontos = placar.Pontos;
            record.Data = DateTime.Today.ToString("dd-MM-yyyy");
            record.TempoDeJogo = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);

            var str = JsonSerializer.Serialize(record);
            SalvarNoConfigFile(str);

            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) return;

            ApiAzure apiAzure = new ApiAzure();
            SalvarDadosEmAPIHallDaFama(apiAzure);
            MostrarHallDaFama(apiAzure);
        }

        private void MostrarHallDaFama(ApiAzure apiAzure)
        {
            try
            {
                hallDaFama = new HallDaFama(canvas, apiAzure.GetAll());
                hallDaFama.Desenhar();
            }
            catch { }           
            
        }

        private void SalvarNoConfigFile(string str)
        {
            string key = "record";
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, str);
            }
            else
            {
                settings[key].Value = str;
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                var novaFlecha = arqueiro.Atirar();

                if (novaFlecha != null)
                    flechas.Add(novaFlecha);
            }

            if (e.Key == Key.S)
            {
                placar.Hide();
                StartGame();
            }
        }


    }
}
