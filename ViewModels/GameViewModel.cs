using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Drawing;
using CurveFever.Services;
using CurveFever.Models;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Reactive.Linq;

namespace CurveFever.ViewModels
{

    public class GameViewModel : ViewModelBase
    {
        private readonly GameDataService _gameDataService;
        public static int ItemCounter { get; set; } = 0;
        private readonly int _gamePadding = 20;
        private Game _game;
        private DispatcherTimer _gameTimer;
        private Canvas GameCanvas;
        public string Test { get; set; } = "Test";
        public GameViewModel(GameDataService gameDataService, Canvas canvas)
        {
            _gameTimer = new DispatcherTimer();
            GameCanvas = canvas;
            _gameTimer.Tick += gameTick;
            _gameTimer.Interval = new TimeSpan(0, 0, 3);
            _game = new Game(gameDataService, new System.Drawing.Point((int)canvas.Width - _gamePadding, (int)canvas.Height - _gamePadding));
            _gameDataService = gameDataService;
            StartGame();
        }
        public void StartGame() {
            _gameTimer.Start();
            NewItem();
        }

        public void NewItem()
        {
            _game.AddItem();
            var item = _game.Items.Last();
            Canvas.SetLeft(item.Rect, item.Position.X);
            Canvas.SetTop(item.Rect, item.Position.Y);
            Debug.WriteLine(item.Rect.ToString());
            GameCanvas.Children.Add(item.Rect);
        }

        void gameTick(object sender, object e)
        {
            foreach(Item item in _game.Items)
            {
                if (item.isExpired())
                {
                    GameCanvas.Children.Remove(item.Rect);
                    _game.Items.Remove(item);
                    break;
                }
            }
            if (ItemCounter % 2 == 0)
            {
                NewItem();
            }
            ItemCounter++;
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
