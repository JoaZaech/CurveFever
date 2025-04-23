using System.Windows;
using System.Windows.Input;
using CurveFever.Services;

namespace CurveFever.ViewModels
{

    public class GameViewModel : ViewModelBase
    {
        private readonly GameDataService _gameDataService;
        public string Test { get; set; } = "Test";
        public GameViewModel(GameDataService gameDataService)
        {
            _gameDataService = gameDataService;
            MessageBox.Show(gameDataService.PlayerName1);
        }
    }
}
