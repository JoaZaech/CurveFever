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

        public PlayerViewModel(string name, Point startPos)
        {
            Player = new Player(name, startPos);
            Player.Direction = GameInputService._directionVectors["RIGHT"];
            //newTrail();
            initEllipse();
            Panel.SetZIndex(Player.Ellipse, 999);
        }

        private void newTrail()
        {
            Player.Trail = new Polyline
            {
                Stroke = Brushes.Lime,
                StrokeThickness = 2
            };
            Player.Trail.Points.Add(Player.Pos);
        }

        private void initEllipse()
        {
            Player.Ellipse = new Ellipse();
            Player.Ellipse.Stroke = new SolidColorBrush(Colors.Green);
            Player.Ellipse.Fill = new SolidColorBrush(Colors.Green);
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

        public void Update(bool addTrail)
        {
            if (addTrail && Player.Trail == null)
            {
                newTrail();
                Debug.WriteLine("Adding new trail");
                GameViewModel.GameCanvas.Children.Add(Player.Trail);
            }
            else if(addTrail && Player.Trail != null)
            {
                Player.Trail.Points.Add(Player.Pos);
            }else if (!addTrail && Player.Trail != null)
            {
                Player.Trail = null;
            }


        }
    }
}