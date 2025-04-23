using CurveFever.Services;
using System.Windows;
using System.Windows.Controls;

namespace CurveFever.Views
{
    /// <summary>
    /// Interaktionslogik für HomeView.xaml
    /// </summary>
    public partial class HomeView : Page
    {
        private readonly GameDataService _gameDataService;
        public HomeView(GameDataService gameDataService)
        {
            InitializeComponent();
            _gameDataService = gameDataService;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            PlayerName1.Text = _gameDataService.PlayerName1;
            mainWindow.MainFrame.Navigate(new Views.Game());
        }
    }
}
