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
using System.Windows.Controls.Primitives;

namespace CurveFever.ViewModels
{

    public class GameViewModel : ViewModelBase
    {
        private readonly GameDataService _gameDataService;
        private readonly GameInputService _gameInputService;

        private Player Player1, Player2;

        public static int ItemCounter { get; set; } = 0;
        private readonly int _gamePadding = 20;
        private Game _game;
        private DispatcherTimer _gameTimer;
        private int _tickCounter;
        private Canvas GameCanvas;
        public string Test { get; set; } = "Test";
        public GameViewModel(GameDataService gameDataService, GameInputService gameInputService, Canvas canvas)
        {
            _tickCounter = 0;
            Player1 = new Player(gameDataService.PlayerName1);
            Player2 = new Player(gameDataService.PlayerName2);
            _gameTimer = new DispatcherTimer();
            _gameInputService = gameInputService;
            GameCanvas = canvas;
            _gameTimer.Tick += gameTick;
            _gameTimer.Interval = TimeSpan.FromMilliseconds(1);
            _game = new Game(gameDataService, new System.Drawing.Point((int)canvas.Width - _gamePadding, (int)canvas.Height - _gamePadding));
            _gameDataService = gameDataService;
            StartGame();
        }
        public void StartGame() {
            GameCanvas.Children.Clear();
            initPlayer();
            _gameTimer.Start();
            NewItem();
        }

        public void initPlayer()
        {
            Player1.Pos = new System.Windows.Point(100, 100);
            Player1.Trail = new Polyline
            {
                Stroke = Brushes.Lime,
                StrokeThickness = 2
            };
            Player2.Pos = new System.Windows.Point(200, 200);
            Player2.Trail = new Polyline
            {
                Stroke = Brushes.Aqua,
                StrokeThickness = 2
            };
            Player2.Trail.Points.Add(Player2.Pos);
            GameCanvas.Children.Add(Player2.Ellipse);
            GameCanvas.Children.Add(Player2.Trail);
            Player1.Trail.Points.Add(Player1.Pos);
            GameCanvas.Children.Add(Player1.Ellipse);
            GameCanvas.Children.Add(Player1.Trail);
        }

        public void NewItem()
        {
            _game.AddItem();
            var item = _game.Items.Last();
            Canvas.SetLeft(item.Ellipse, item.Position.X);
            Canvas.SetTop(item.Ellipse, item.Position.Y);
            GameCanvas.Children.Add(item.Ellipse);
        }

        void gameTick(object sender, object e)
        {
            _tickCounter++;
            foreach (Item item in _game.Items)
            {
                if (item.isExpired())
                {
                    GameCanvas.Children.Remove(item.Ellipse);
                    _game.Items.Remove(item);
                    break;
                }
            }
            _gameInputService.HandleMovement(Player1, Player2, true);
            
           

        }

        ~GameViewModel()
        {
            _gameTimer.Tick -= gameTick;
        }
    }
}
