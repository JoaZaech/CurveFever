using System.Diagnostics;
using System.Drawing;
using System.Windows.Controls;

namespace CurveFever.Models
{
    public class Game
    {
        public int GameRounds { get; set; }
        private Point _gameSize { get; set; }
        private Random _rnd;
        public List<Item> Items { get; set; }
        public Game(int amount, Point gameSize)
        {
            Items = new List<Item>();
            _rnd = new Random(); 
            _gameSize = gameSize;
            GameRounds = amount;
            AddItem();
            Debug.WriteLine(Items[0].ToString());
        }

        public void AddItem()
        {
            Items.Add(new Item(getRandomPos(), getRandomType()));
        }

        private ItemType getRandomType()
        {
            Array values = Enum.GetValues(typeof(ItemType));
            return (ItemType)values.GetValue(_rnd.Next(values.Length));
        }

        private Point getRandomPos()
        {
            
            int x = _rnd.Next(0, (int)_gameSize.X);
            int y = _rnd.Next(0, (int)_gameSize.Y);
            return new Point(x, y);
        }

    }
}
