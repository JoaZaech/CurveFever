using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CurveFever.Services;

namespace CurveFever.ViewModels
{

    public class GameViewModel : ViewModelBase
    {
        private readonly GameDataService _gameDataService;
        private DispatcherTimer _gameTimer;
        private Canvas GameCanvas;
        public int x, y;
        public string Test { get; set; } = "Test";
        public GameViewModel(GameDataService gameDataService)
        {
            _gameTimer = new DispatcherTimer();
            x = 100;
            y = 100;
            _gameTimer.Tick += gameTick;
            _gameTimer.Interval = new TimeSpan(0, 0, 1);
            StartGame();                                        
            _gameDataService = gameDataService;
        }
        public void setCanvas(Canvas canvas)
        {
            GameCanvas = canvas;
        }
        public void StartGame() {
            _gameTimer.Start();
        }

        void gameTick(object sender, object e)
        {
            Debug.WriteLine("1");
            GameCanvas.SetLe
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
