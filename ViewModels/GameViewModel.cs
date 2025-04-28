using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Drawing;
using CurveFever.Services;
using CurveFever.Models;

namespace CurveFever.ViewModels
{

    public class GameViewModel : ViewModelBase
    {
        private readonly GameDataService _gameDataService;
        private Game _game;
        private DispatcherTimer _gameTimer;
        private Canvas GameCanvas;
        public string Test { get; set; } = "Test";
        public GameViewModel(GameDataService gameDataService, Canvas canvas)
        {
            _gameTimer = new DispatcherTimer();
            GameCanvas = canvas;
            _gameTimer.Tick += gameTick;
            _gameTimer.Interval = new TimeSpan(0, 0, 1);
            _game = new Game(gameDataService.Rounds, new System.Drawing.Point((int)canvas.Width, (int)canvas.Height));
            _gameDataService = gameDataService;
            StartGame();                                        
        }
        public void StartGame() {
            _gameTimer.Start();
        }

        void gameTick(object sender, object e)
        {
            Debug.WriteLine("1");
        }

        public void keyPressed(string key)
        {
            MessageBox.Show(key);
        }

        ~GameViewModel()
        {
            _gameTimer.Tick -= gameTick;
        }
    }
}
