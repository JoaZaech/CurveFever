using CurveFever.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace CurveFever.ViewModels
{
    
    class GameViewModel : ViewModelBase
    {
        public Game Game { get; set; }
        public int i = 0;
        public GameViewModel()
        {
            Game = new Game(5);
        }
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            i++;
        }
    }
}
