namespace CurveFever.Models
{
    public class Game
    {
        public int GameRounds { get; set; }
        public Game(int amount)
        {
            GameRounds = amount;
        }
    }
}
