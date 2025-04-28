using CurveFever.Views;
using System.Windows;
using CurveFever.Services;

namespace CurveFever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(GameDataService gameDataService)
        {
            InitializeComponent();
            MainFrame.Navigate(new HomeView(gameDataService));
        }
    }
}