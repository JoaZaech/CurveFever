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
        private readonly int _gamePadding = 20;
        private Game _game;
        private DispatcherTimer _gameTimer;
        private Dictionary<Item, System.Windows.Shapes.Rectangle> _gameItems;
        private Canvas GameCanvas;
        public string Test { get; set; } = "Test";
        public GameViewModel(GameDataService gameDataService, Canvas canvas)
        {
            _gameTimer = new DispatcherTimer();
            _gameItems = new Dictionary<Item, System.Windows.Shapes.Rectangle>();
            GameCanvas = canvas;
            _gameTimer.Tick += gameTick;
            _gameTimer.Interval = new TimeSpan(0, 0, 3);
            _game = new Game(gameDataService.Rounds, new System.Drawing.Point((int)canvas.Width - _gamePadding, (int)canvas.Height - _gamePadding));
            _gameDataService = gameDataService;
            StartGame();
            var expirationStream = Observable.Merge(_game.Items.Select(item => item.ExpiredObservable));
            expirationStream.Subscribe(
           expiredItem =>
           {
               Debug.WriteLine($"{expiredItem.ToString} has expired!");
               if (_gameItems.TryGetValue(expiredItem, out System.Windows.Shapes.Rectangle rect))
               {
                   GameCanvas.Children.Remove(rect);
                   _gameItems.Remove(expiredItem);
               }
           }
       );
        }
        public void StartGame() {
            _gameTimer.Start();
            NewItem();
        }

        public void NewItem()
        {
            _game.AddItem();
            var item = _game.Items.Last();
            var rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Fill = new SolidColorBrush(Colors.Black);
            rect.Width = 20;
            rect.Height = 20;
            rect.Name = "a";
            Canvas.SetLeft(rect, item.Position.X);
            Canvas.SetTop(rect, item.Position.Y);
            GameCanvas.Children.Add(rect);
            _gameItems.Add(item, rect);
        }

        void gameTick(object sender, object e)
        {
            Debug.WriteLine(_gameItems.Count);
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
