using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CurveFever.Models
{

    public class Player
    {
        public Vector Direction;
        public Point Pos { get; set; }
        public Polyline Trail { get; set; }
        public Ellipse Ellipse { get; set; }
        public string Name { get; set; }

        public Player(string name)
        {
            Name = name;
            Direction = new Vector(1, 0);
            Ellipse = new System.Windows.Shapes.Ellipse();
            Ellipse.Stroke = new SolidColorBrush(Colors.Green);
            Ellipse.Fill = new SolidColorBrush(Colors.Green);
            Ellipse.Width = 10;
            Ellipse.Height = 10;
            Ellipse.Name = "player_" +  Name;
            Panel.SetZIndex(Ellipse, 999);
        }


        public void UpdatePos(bool addTrail)
        {
            Pos += Direction * 2;
            if(addTrail) Trail.Points.Add(Pos); 
        }

    }



}
