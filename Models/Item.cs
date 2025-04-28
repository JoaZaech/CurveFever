using System.Drawing;
using Xceed.Wpf.Toolkit.Mag.Converters;

namespace CurveFever.Models
{
    public class Item
    {
        public int Radius { get; }
        public ItemType Type { get; set; }
        public Point Position { get;}
        
        public Color Color { get; set; }
        public TimeSpan SpawnDuration { get; set; }
        public bool IsActive { get; set; }

        public Item(Point p, ItemType type)
        {
            Position = p;
            Type = type;
            Radius = 10;
        }

        public bool CheckCollision(Point playerPosition, int playerRadius)
        {
            double distance = Math.Sqrt(Math.Pow(Position.X - playerPosition.X, 2) + Math.Pow(Position.Y - playerPosition.Y, 2));
            return distance <= (Radius + playerRadius);
        }

        public override string ToString()
        {
            return $"Item: {Type}, Position: {Position}, Radius: {Radius}";
        }

    }
}
