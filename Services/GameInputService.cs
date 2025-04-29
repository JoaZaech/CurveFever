using CurveFever.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CurveFever.Services
{
    public class GameInputService
    {
        private readonly HashSet<Key> _pressedKeys = new HashSet<Key>();
        private readonly Dictionary<string, Vector> _directionVectors = new Dictionary<string, Vector>{
            { "UP", new Vector(0, -1) },
            { "DOWN", new Vector(0, 1) },
            { "LEFT", new Vector(-1, 0) },
            { "RIGHT", new Vector(1, 0) }
        };

        public void AddKey(Key key)
        {
            _pressedKeys.Add(key);
        }
        public void RemoveKey(Key key)
        {
            _pressedKeys.Remove(key);
        }

        private void UpdatePlayerDirection(Player player, Key up, Key down, Key left, Key right, bool addTrail)
        {
            Vector currentDir = player.Direction;

            if (_pressedKeys.Contains(up) && currentDir != _directionVectors["DOWN"])
                player.Direction = _directionVectors["UP"];
            else if (_pressedKeys.Contains(down) && currentDir != _directionVectors["UP"])
                player.Direction = _directionVectors["DOWN"];
            else if (_pressedKeys.Contains(left) && currentDir != _directionVectors["RIGHT"])
                player.Direction = _directionVectors["LEFT"];
            else if (_pressedKeys.Contains(right) && currentDir != _directionVectors["LEFT"])
                player.Direction = _directionVectors["RIGHT"];
            player.UpdatePos(addTrail);

            // Correct postition of ellipse
            if(player.Direction == _directionVectors["RIGHT"] || player.Direction == _directionVectors["LEFT"])
            {
                Canvas.SetLeft(player.Ellipse, player.Pos.X - 10);
                Canvas.SetTop(player.Ellipse, player.Pos.Y - 5);
            }else if(player.Direction == _directionVectors["UP"] || player.Direction == _directionVectors["DOWN"])
            {
                Canvas.SetLeft(player.Ellipse, player.Pos.X - 5.5);
                Canvas.SetTop(player.Ellipse, player.Pos.Y - 5);
            }
        }

        public void HandleMovement(Player p1, Player p2, bool addTrail)
        {
            UpdatePlayerDirection(p1, Key.Up, Key.Down, Key.Left, Key.Right, true);
            UpdatePlayerDirection(p2, Key.W, Key.S, Key.A, Key.D, true);
        }

    }
}
