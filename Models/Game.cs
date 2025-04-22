using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveFever.Models
{
    class Game
    {
        public int GameRounds { get; set; }
        public Game(int amount)
        {
            GameRounds = amount;
        }
    }
}
