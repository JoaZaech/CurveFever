using CurveFever.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CurveFever.Models
{

    public class Player
    {
        public Vector Direction;
        public Point Pos { get; set; }
        public List<Polyline> Trails { get; set; }
        public Polyline Trail { get; set; }
        public Ellipse Ellipse { get; set; }
        public string Name { get; set; }

        public Player(string name, Point startPos)
        {
            Name = name;
            Pos = startPos;
        }

        public void UpdatePos()
        {
            Pos += Direction * 2;
        }

    }



}
