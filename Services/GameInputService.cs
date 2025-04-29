using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void HandleMovement()
        {
            if (_pressedKeys.Contains(Key.Up)) Debug.WriteLine("GO UP arrow");
            if (_pressedKeys.Contains(Key.W)) Debug.WriteLine("GO UP W");
        }

    }
}
