using CurveFever.Services;
using CurveFever.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace CurveFever.Views
{
    /// <summary>
    /// Interaktionslogik für Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private readonly GameInputService _gameInputService;
        public Game(GameDataService gameDataService, GameInputService gameInputService)
        {
            
            InitializeComponent();
            this.Loaded += (s, e) => Keyboard.Focus(this);
            this.Focus();
            this.DataContext = new GameViewModel(gameDataService, gameInputService, GameCanvas);
            _gameInputService = gameInputService;
        }
        private void Key_Down(object sender, KeyEventArgs e)
        {
            _gameInputService.AddKey(e.Key);
        }
        private void Key_Up(object sender, KeyEventArgs e)
        {
            _gameInputService.RemoveKey(e.Key);
        }
    }
}
