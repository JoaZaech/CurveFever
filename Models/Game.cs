using CurveFever.Services;
using System.Diagnostics;
using System.Windows;
using System.Reactive.Linq;
using System.Windows.Controls;

namespace CurveFever.Models
{
    public class Game
    {
        private Point _gameSize { get; set; }
        private readonly GameDataService _gameDataService;
        private Random _rnd;
        public List<Item> Items { get; set; }
        public Game(GameDataService gameDataService, Point gameSize)
        {
            Items = new List<Item>();
            _gameDataService = gameDataService;
            _rnd = new Random(); 
            _gameSize = gameSize;
        }

        public void AddItem()
        {
            Point pos;
            do
            {
                pos = getRandomPos();
            } while (isPositionTooClose(pos));
            
            Items.Add(new Item(_gameDataService.ItemCounter, pos, getRandomType()));
            _gameDataService.ItemCounter++;
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
