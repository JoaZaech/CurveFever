using CurveFever.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Reactive.Linq;
using System.Windows.Media;

namespace CurveFever.Models
{
    public class Item
    {
        private DateTime _endTime;
        public int Radius { get; }
        public bool isActive { get; set; } = true;
        public ItemType Type { get; set; }
        public Point Position { get;}
        public string Id { get; set; }
        public System.Windows.Shapes.Ellipse Ellipse { get; set; }

        public System.Windows.Media.Color Color { get; set; }


        public Item(int id, Point p, ItemType type)
        {
            Position = p;
            Id = "item_" + id.ToString();
            GameViewModel.ItemCounter++;
            Color = Colors.Red;
            _endTime = DateTime.Now.AddSeconds(10);
            Type = type;
            Radius = 20;
            initEllipse();
        }

        private void initEllipse()
        {
            Ellipse = new System.Windows.Shapes.Ellipse();
            Ellipse.Stroke = new SolidColorBrush(Color);
            Ellipse.Fill = new SolidColorBrush(Color);
            Ellipse.Width = Radius;
            Ellipse.Height = Radius;
            Ellipse.Name = Id;
        }

        public bool isExpired()
        {
            return DateTime.Now > _endTime || !isActive;
        }

        public bool CheckCollision(Point playerPosition, int playerRadius)
        {
            double distance = Math.Sqrt(Math.Pow(Position.X - playerPosition.X, 2) + Math.Pow(Position.Y - playerPosition.Y, 2));
            if(distance <= (Radius + playerRadius))
            {
                isActive = false;
                return true;
            }
            return false;
        }

        public int GetDistance(Point p2)
        {
            return Convert.ToInt32(Math.Floor((p2 - Position).Length));
        }

        public override string ToString()
        {
            return $"Item: {Type}, Position: {Position}, Radius: {Radius}";
        }

    }
}
