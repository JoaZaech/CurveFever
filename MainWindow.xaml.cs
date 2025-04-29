using CurveFever.Views;
using System.Windows;
using CurveFever.Services;
using System.Windows.Input;

namespace CurveFever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(GameDataService gameDataService, GameInputService gameInputService)
        {
            InitializeComponent();
            this.PreviewKeyDown += (s, e) =>
            {
                if (e.Key == Key.Back)
                    e.Handled = true;
            };
            this.Focusable = false;
            MainFrame.Navigate(new HomeView(gameDataService, gameInputService));
        }
    }
}