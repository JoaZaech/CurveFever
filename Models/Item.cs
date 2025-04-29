using CurveFever.ViewModels;
using System.Diagnostics;
using System.Drawing;
using System.Reactive.Linq;
using System.Windows.Media;

namespace CurveFever.Models
{
    public class Item
    {
        private DateTime _endTime;
        public int Radius { get; }
        public ItemType Type { get; set; }
        public Point Position { get;}
        public string Id { get; set; }
        public System.Windows.Shapes.Rectangle Rect { get; set; }

        public System.Windows.Media.Color Color { get; set; }


        public Item(int id, Point p, ItemType type)
        {
            Position = p;
            Id = "item_" + id.ToString();
            GameViewModel.ItemCounter++;
            Color = Colors.Red;
            _endTime = DateTime.Now.AddSeconds(10);
            Type = type;
            Radius = 10;
            initRectangle();
        }

        private void initRectangle()
        {
            Rect = new System.Windows.Shapes.Rectangle();
            Rect.Stroke = new SolidColorBrush(Color);
            Rect.Fill = new SolidColorBrush(Color);
            Rect.Width = Radius;
            Rect.Height = Radius;
            Rect.Name = Id;
        }

        public bool isExpired()
        {
            return DateTime.Now > _endTime;
        }

        public bool CheckCollision(Point playerPosition, int playerRadius)
        {
            double distance = Math.Sqrt(Math.Pow(Position.X - playerPosition.X, 2) + Math.Pow(Position.Y - playerPosition.Y, 2));
            return distance <= (Radius + playerRadius);
        }

        public int GetDistance(Point p2)
        {
            double distance = Math.Sqrt(Math.Pow(p2.X - Position.X, 2) + Math.Pow(p2.Y - Position.Y, 2));
            return Convert.ToInt32(Math.Floor(distance));
        }

        public override string ToString()
        {
            return $"Item: {Type}, Position: {Position}, Radius: {Radius}";
        }

    }
}
