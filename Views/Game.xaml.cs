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
        public Game()
        {
            InitializeComponent();
            this.Focus();
        }
        private void Key_Click(object sender, KeyEventArgs e)
        {
            MyLabel.Content = e.Key.ToString();
        }
    }
}
