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
        private readonly GameInputService _gameInputService;

        public static int ItemCounter { get; set; } = 0;
        private readonly int _gamePadding = 20;
        private Game _game;
        private DispatcherTimer _gameTimer;
        private Canvas GameCanvas;
        public string Test { get; set; } = "Test";
        public GameViewModel(GameDataService gameDataService, GameInputService gameInputService, Canvas canvas)
        {
            _gameTimer = new DispatcherTimer();
            _gameInputService = gameInputService;
            GameCanvas = canvas;
            _gameTimer.Tick += gameTick;
            _gameTimer.Interval = TimeSpan.FromMilliseconds(16);
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
            Canvas.SetLeft(item.Ellipse, item.Position.X);
            Canvas.SetTop(item.Ellipse, item.Position.Y);
            Debug.WriteLine(item.Ellipse.ToString());
            GameCanvas.Children.Add(item.Ellipse);
        }

        void gameTick(object sender, object e)
        {
            foreach(Item item in _game.Items)
            {
                if (item.isExpired())
                {
                    GameCanvas.Children.Remove(item.Ellipse);
                    _game.Items.Remove(item);
                    break;
                }
            }
            if (ItemCounter % 1000 == 0)
            {
                NewItem();
            }
            ItemCounter++;
            _gameInputService.HandleMovement();
        }

        public void KeyPressed(string key)
        {
            Debug.WriteLine(key);
        }

        ~GameViewModel()
        {
            _gameTimer.Tick -= gameTick;
        }
    }
}
