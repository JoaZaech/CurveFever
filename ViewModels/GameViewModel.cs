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

        public string PlayerScore1
        {
            get { return _gameDataService.PlayerScore1; }
            set
            {
                _gameDataService.PlayerScore1 = value;
                OnPropertyChanged();
            }
        }

        public string PlayerScore2
        {
            get { return _gameDataService.PlayerScore2; }
            set
            {
                _gameDataService.PlayerScore2 = value;
                OnPropertyChanged();
            }
        }

        private PlayerViewModel[] Players;
        private List<Point> _trailPoints;
        private bool _toggleTrail = true;

        public static int ItemCounter { get; set; } = 0;
        private readonly int _gamePadding = 20;
        private Game _game;
        private DispatcherTimer _gameTimer;
        private int _tickCounter;
        public static Canvas GameCanvas;

        public GameViewModel(GameDataService gameDataService, GameInputService gameInputService, Canvas canvas)
        {
            _gameTimer = new DispatcherTimer();
            _gameInputService = gameInputService;
            GameCanvas = canvas;
            _gameTimer.Tick += gameTick;
            _gameTimer.Interval = TimeSpan.FromMilliseconds(1);
            _gameDataService = gameDataService;
            NewGame();
        }
        public void NewGame() {
            UpdateScore(0, 1);
            GameCanvas.Children.Clear();
            _tickCounter = 0;
            Players = new PlayerViewModel[2];
            Players[0] = new PlayerViewModel(_gameDataService.PlayerName1, new Point(100, 100), Colors.Red);
            Players[1] = new PlayerViewModel(_gameDataService.PlayerName2, new Point(200, 200), Colors.Blue);
            var size = new Point((int)GameCanvas.Width - _gamePadding, (int)GameCanvas.Height - _gamePadding);
            _game = new Game(_gameDataService, size, Players);
            _trailPoints = new List<Point>();
            initPlayer();
            NewItem();
            _gameTimer.Start();
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
           
            if (_tickCounter % 20 == 0)
            {
                _toggleTrail = !_toggleTrail;
                _gameDataService.PlayerScore2 = "111";
                _tickCounter = 0;
            }

            _gameInputService.HandleMovement(Players[0].Player, Players[1].Player);

            foreach(var player in Players)
            {
                player.Update(_toggleTrail, _trailPoints);
            }

            if (_game.CheckCollision(_trailPoints))
            {
                NewGame();
            }

            foreach (Item item in _game.Items)
            {
                if (item.isExpired())
                {
                    GameCanvas.Children.Remove(item.Ellipse);
                    _game.Items.Remove(item);
                    break;
                }
            }

        }

        ~GameViewModel()
        {
            _gameTimer.Tick -= gameTick;
        }
        public void UpdateScore(int player1, int player2)
        {
            PlayerScore1 = _gameDataService.PlayerName1 + " " + player1.ToString();
            PlayerScore2 = _gameDataService.PlayerName1 + " " + player2.ToString();
        }
    }
}
