using System.Windows;
using System.Windows.Controls;

namespace CurveFever.Views
{
    /// <summary>
    /// Interaktionslogik für HomeView.xaml
    /// </summary>
    public partial class HomeView : Page
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Navigate(new Views.Game());
        }
    }
}
