using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using CurveFever.Models;
using System.Windows;
using CurveFever.Services;
using System.Diagnostics;
namespace CurveFever.ViewModels
{
    public class PlayerViewModel
    {
        public Player Player { get; set; }
        private bool _currentTrail = true;
        public List<Point> TrailPoints { get; set; }
        private Color _color;

        public PlayerViewModel(string name, Point startPos, Color color)
        {
            TrailPoints = new List<Point>();
            Player = new Player(name, startPos);
            _color = color;
            Player.Direction = GameInputService._directionVectors["RIGHT"];
            initEllipse();
            Panel.SetZIndex(Player.Ellipse, 999);
        }

        private void newTrail()
        {
            Player.Trail = new Polyline
            {
                Stroke = new SolidColorBrush(_color),
                StrokeThickness = 2
            };
            Player.Trail.Points.Add(Player.Pos);
        }

        private void initEllipse()
        {
            Player.Ellipse = new Ellipse();
            Player.Ellipse.Stroke = new SolidColorBrush(_color);
            Player.Ellipse.Fill = new SolidColorBrush(_color);
            Player.Ellipse.Width = 10;
            Player.Ellipse.Height = 10;
            Player.Ellipse.Name = "player_" + Player.Name;
        }

        public Ellipse GetEllipse()
        {
            return Player.Ellipse;
        }

        public Polyline GetPolyline()
        {
            return Player.Trail;
        }

        public void Update(bool addTrail, List<Point> trailpoints)
        {
            if (addTrail && Player.Trail == null)
            {
                newTrail();
                GameViewModel.GameCanvas.Children.Add(Player.Trail);
            }
            else if(addTrail && Player.Trail != null)
            {
                Player.Trail.Points.Add(Player.Pos);
                trailpoints.Add(Player.Pos);
            }
            else if (!addTrail && Player.Trail != null)
            {
                // Add finished trail
                Player.Trail = null;
            }
        }
    }
}