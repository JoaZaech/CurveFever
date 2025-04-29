using System.Windows;
using System.Windows.Shapes;

namespace CurveFever.Models
{

    public class Player
    {
        public Vector Direction;
        public Point Pos { get; set; }
        public Polyline Trail { get; set; }

        public Player()
        {
            Direction = new Vector(1, 0);
        }

        public void UpdatePos()
        {
            Pos += Direction * 2;
            Trail.Points.Add(Pos);
        }

    }



}
