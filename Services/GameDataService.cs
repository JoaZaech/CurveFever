using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveFever.Services
{
    public class GameDataService
    {
        public int ItemCounter { get; set; } = 0;
        public string PlayerName1 { get; set; }
        
        public string PlayerName2 { get; set; }
        public int Rounds { get; set; }

        public void InitGameDataService(string playerName1, string playerName2, string rounds)
        {
            PlayerName1 = playerName1;
            PlayerName2 = playerName2;
            Rounds = Int32.Parse(rounds.Substring(0,2));
        }

    }
}
