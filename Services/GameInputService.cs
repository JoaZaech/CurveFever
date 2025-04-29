using CurveFever.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CurveFever.Services
{
    public class GameInputService
    {
        private readonly HashSet<Key> _pressedKeys = new HashSet<Key>();

        public void AddKey(Key key)
        {
            _pressedKeys.Add(key);
        }
        public void RemoveKey(Key key)
        {
            _pressedKeys.Remove(key);
        }

        public void HandleMovement(Player p1)
        {
            if (_pressedKeys.Contains(Key.Up)) p1.Direction = new Vector(0, -1);
            if (_pressedKeys.Contains(Key.Down)) p1.Direction = new Vector(0, 1);
            if (_pressedKeys.Contains(Key.Left)) p1.Direction = new Vector(-1, 0);
            if (_pressedKeys.Contains(Key.Right)) p1.Direction = new Vector(1, 0);
            p1.UpdatePos();
        }

    }
}
