using System.Diagnostics;
using System.Drawing;
using System.Reactive.Linq;
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
        }

        public void AddItem()
        {
            Point pos;
            do
            {
                pos = getRandomPos();
            } while (isPositionTooClose(pos));

            Items.Add(new Item(pos, getRandomType()));
        }

        private ItemType getRandomType()
        {
            Array values = Enum.GetValues(typeof(ItemType));
            return (ItemType)values.GetValue(_rnd.Next(values.Length));
        }

        private bool isPositionTooClose(Point newPos)
        {
            const int minDistance = 20;
            foreach (var item in Items)
            {
                if (item.GetDistance(newPos) < minDistance) return true;
            }
            return false;
        }

        private Point getRandomPos()
        {
            
            int x = _rnd.Next(0, (int)_gameSize.X);
            int y = _rnd.Next(0, (int)_gameSize.Y);
            return new Point(x, y);
        }

    }
}
