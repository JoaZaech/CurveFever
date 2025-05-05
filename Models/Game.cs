using CurveFever.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using CurveFever.ViewModels;

namespace CurveFever.Models
{
    public class Game
    {
        private Point _gameSize { get; set; }
        private readonly GameDataService _gameDataService;
        private PlayerViewModel[] _players;
        private Random _rnd;
        public List<Item> Items { get; set; }
        public Game(GameDataService gameDataService, Point gameSize, PlayerViewModel[] players)
        {
            Items = new List<Item>();
            _players = players;
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

        public bool CheckCollision(List<Point> trailPoints)
        {
            foreach (var player in _players)
            {

                if (BorderCollision(player.Player.Pos))
                {
                    return true;
                }

                if(TrailCollision(trailPoints, player.Player))
                {
                    return true;
                }

                foreach (var item in Items)
                {
                    item.CheckCollision(player.Player.Pos, 2);
                }

            }
            return false;
        }

        public bool TrailCollision(List<Point> trailPoints, Player player)
        {
            var pointsInFront = new List<Point>();
            player.Direction.Normalize();

            foreach (var point in trailPoints)
            {
                Vector toPoint = point - player.Pos;
                toPoint.Normalize();

                double dot = Vector.Multiply(player.Direction, toPoint); // Dot product
                double angle = Vector.AngleBetween(player.Direction, toPoint);

                if (dot > 0 && Math.Abs(angle) <= 90 / 2)
                {
                    pointsInFront.Add(point);
                }
            }

            foreach (var point in pointsInFront)
            {
                if ((player.Pos - point).Length <= 2)
                {
                    return true;
                }
            }
            return false;

        }

        public bool BorderCollision(Point pos)
        {
            if (pos.X < 0 || pos.X > _gameSize.X || pos.Y < 0 || pos.Y > _gameSize.Y)
            {
                return true;
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
