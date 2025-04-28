using CurveFever.Services;
using CurveFever.ViewModels;
using System.Windows;
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
            this.DataContext = new GameViewModel(gameDataService);
            this.Focus();
            InitializeComponent();
            ((GameViewModel)this.DataContext).setCanvas(GameCanvas);
        }
        private void Key_Click(object sender, KeyEventArgs e)
        {
            GameCanvas.SetLeft(MyRectangle, 10);
            ((GameViewModel)this.DataContext).keyPressed(e.Key.ToString());
        }
    }
}
