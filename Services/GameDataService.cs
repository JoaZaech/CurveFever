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
        public string PlayerScore1 { get; set; }
        public string PlayerScore2 { get; set; }

        public string PlayerName2 { get; set; }
        public int Rounds { get; set; }

        public void UpdateScore(int player1, int player2)
        {
            PlayerScore1 = PlayerName1 + " " + player1.ToString();
            PlayerScore2 = PlayerName2 + " " + player2.ToString();
        }

        public void InitGameDataService(string playerName1, string playerName2, string rounds)
        {
            PlayerScore1 = PlayerName1 + " 0";
            PlayerScore2 = PlayerName2 + " 0";
            PlayerName1 = playerName1;
            PlayerName2 = playerName2;
            Rounds = Int32.Parse(rounds.Substring(0,2));
        }

    }
}
