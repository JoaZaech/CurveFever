using System.Diagnostics;
using System.Drawing;
using System.Reactive.Linq;

namespace CurveFever.Models
{
    public class Item
    {
        private DateTime _endTime;
        private System.Timers.Timer _timer;
        public IObservable<Item> ExpiredObservable { get; private set; }
        public int Radius { get; }
        public ItemType Type { get; set; }
        public Point Position { get;}
        public Guid Id { get; set; }

        public Color Color { get; set; }

        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                StartExpirationTimer();
                Debug.WriteLine("Start");
            }
        }

        public Item(Point p, ItemType type)
        {
            Position = p;
            Id = Guid.NewGuid();
            _endTime = DateTime.Now.AddSeconds(5);
            Type = type;
            Radius = 10;
            ExpiredObservable = Observable.Empty<Item>();
        }

        private void StartExpirationTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (sender, args) =>
            {
                if (DateTime.Now >= EndTime)
                {
                    _timer.Stop();
                    OnExpired();
                }
            };
            _timer.Start();
        }

        private void OnExpired()
        {
            ExpiredObservable = Observable.Return(this); 
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
