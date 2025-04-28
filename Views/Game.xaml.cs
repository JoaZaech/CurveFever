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
        public Game(GameDataService gameDataService)
        {
            this.Focus();
            InitializeComponent();
            this.DataContext = new GameViewModel(gameDataService, GameCanvas);
        }
        private void Key_Click(object sender, KeyEventArgs e)
        {
            ((GameViewModel)this.DataContext).keyPressed(e.Key.ToString());
        }
    }
}
