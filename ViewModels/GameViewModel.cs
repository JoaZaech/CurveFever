using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
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

        private PlayerViewModel[] Players;
        private bool _isGameOver = false;
        private List<Point> _trailPoints;
        private bool _toggleTrail = true;

        public static int ItemCounter { get; set; } = 0;
        private readonly int _gamePadding = 20;
        private Game _game;
        private DispatcherTimer _gameTimer;
        private int _tickCounter;
        public static Canvas GameCanvas;
        public string Test { get; set; } = "Test";
        public GameViewModel(GameDataService gameDataService, GameInputService gameInputService, Canvas canvas)
        {
            _gameTimer = new DispatcherTimer();
            _gameInputService = gameInputService;
            GameCanvas = canvas;
            _gameTimer.Tick += gameTick;
            _gameTimer.Interval = TimeSpan.FromMilliseconds(1);
            _gameDataService = gameDataService;
            StartGame();
        }
        public void StartGame() {
            _tickCounter = 0;
            Players = new PlayerViewModel[2];
            Players[0] = new PlayerViewModel(_gameDataService.PlayerName1, new Point(100, 100));
            Players[1] = new PlayerViewModel(_gameDataService.PlayerName2, new Point(200, 200));
            _game = new Game(_gameDataService, new Point((int)GameCanvas.Width - _gamePadding, (int)GameCanvas.Height - _gamePadding));
            _trailPoints = new List<Point>();
            GameCanvas.Children.Clear();
            initPlayer();
            _gameTimer.Start();
            //NewItem();
        }

        public void initPlayer()
        {
            GameCanvas.Children.Add(Players[0].GetEllipse());
            GameCanvas.Children.Add(Players[1].GetEllipse());
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
            /* 
             foreach (Item item in _game.Items)
            {
                if (item.isExpired())
                {
                    GameCanvas.Children.Remove(item.Ellipse);
                    _game.Items.Remove(item);
                    break;
                }
            }
             */
            if (_tickCounter % 20 == 0)
            {
                _toggleTrail = !_toggleTrail;
                _tickCounter = 0;
            }

            _gameInputService.HandleMovement(Players[0].Player, Players[1].Player);

            foreach(var player in Players)
            {
                player.Update(_toggleTrail, _trailPoints);
            }
        }

        ~GameViewModel()
        {
            _gameTimer.Tick -= gameTick;
        }
    }
}
