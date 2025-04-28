using CurveFever.Services;
using System.Data;
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

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            ComboBoxItem typeItem = (ComboBoxItem)RoundInput.SelectedItem;
            _gameDataService.InitGameDataService(PlayerName1.Text, PlayerName2.Text, typeItem.Content.ToString());
            Application.Current.Dispatcher.Invoke(() =>
            {
                mainWindow.MainFrame.Navigate(new Game(_gameDataService));
            });
        }
    }
}
