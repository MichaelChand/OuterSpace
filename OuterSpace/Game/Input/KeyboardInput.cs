using OuterSpace;
using OuterSpace.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReConInvaders.Inputsystem
{
    public class KeyboardInput
    {
        private Action<Key> _kbEventCallback;

        private KeypressType _keypressed = KeypressType.Nokey;

        public KeyboardInput(Action<Key> keyEventCallback)
        {
            _kbEventCallback = keyEventCallback;
            App.Current.MainWindow.KeyDown += KeyboardEvent_keydown;
        }

        public KeyboardInput()
        {
            _kbEventCallback = null;
            App.Current.MainWindow.KeyDown += KeyboardEvent_keydown;
        }

        private void KeyboardEvent_keydown(object sender, KeyEventArgs keyEventArgs)
        {
            if(_kbEventCallback != null)
                _kbEventCallback.Invoke(keyEventArgs.Key);
            SetCurrentKeypressType(keyEventArgs.Source as Key?);
        }

        private void SetCurrentKeypressType(Key? keyPressed)
        {
            if (keyPressed == null)
            {
                _keypressed = KeypressType.Nokey;
                return;
            }

            switch (keyPressed)
            {
                case Key.Up :
                    _keypressed = KeypressType.Up;
                    break;
                case Key.Down:
                    _keypressed = KeypressType.Down;
                    break;
                case Key.Left:
                    _keypressed = KeypressType.Left;
                    break;
                case Key.Right:
                    _keypressed = KeypressType.Right;
                    break;
                case Key.Space:
                    _keypressed = KeypressType.Space;
                    break;
                default:
                    _keypressed = KeypressType.Nokey;
                    break;
            }
        }

        public KeypressType GetLastKeyPressed()
        {
            return _keypressed;
        }
    }
}
