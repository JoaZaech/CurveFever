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

        private PlayerViewModel Player1, Player2;
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
            _tickCounter = 0;
            Player1 = new PlayerViewModel(gameDataService.PlayerName1, new Point(100, 100));
            Player2 = new PlayerViewModel(gameDataService.PlayerName2, new Point(200, 200));
            _gameTimer = new DispatcherTimer();
            _gameInputService = gameInputService;
            GameCanvas = canvas;
            _gameTimer.Tick += gameTick;
            _gameTimer.Interval = TimeSpan.FromMilliseconds(1);
            _game = new Game(gameDataService, new Point((int)canvas.Width - _gamePadding, (int)canvas.Height - _gamePadding));
            _gameDataService = gameDataService;
            StartGame();
        }
        public void StartGame() {
            GameCanvas.Children.Clear();
            initPlayer();
            _gameTimer.Start();
            //NewItem();
        }

        public void initPlayer()
        {
            GameCanvas.Children.Add(Player1.GetEllipse());
            GameCanvas.Children.Add(Player2.GetEllipse());
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
            _gameInputService.HandleMovement(Player1.Player, Player2.Player);
            Player1.Update(_toggleTrail);
            Player2.Update(_toggleTrail);
        }


        ~GameViewModel()
        {
            _gameTimer.Tick -= gameTick;
        }
    }
}
