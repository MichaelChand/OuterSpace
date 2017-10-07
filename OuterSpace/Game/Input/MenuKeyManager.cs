using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OuterSpace.Game.Input
{
    public class MenuKeyManager : IKeyManager
    {
        private Key? _key = null;
        public bool IsKeyPressed = false;
        private Key? _keypressed = null;
        private Dictionary<Key?, KeyInfo> _actionKeyList;


        public MenuKeyManager()
        {
        }

        public void KeyUp(Key? key)
        {
            if (key == _key)
            {
                IsKeyPressed = false;
                _keypressed = null; //Clear once released.
            }
        }

        public void KeyDown(Key? key)
        {

        }

        public List<Key?> GetActiveKeyList()
        {
            Key? somekey = _keypressed;
            return new List<Key?> { _key };
        }

        public void ClearKeys()
        {
            _key = null;
            _keypressed = null;
        }

        public bool SetCurrentKeypressType(Key? keyPressed)
        {
            IsKeyPressed = true;
            bool keyHandledState = true;
            Key? oldkey = _key;
            _key = keyPressed;
            if (_key == null)
            {
                _keypressed = null;
                return false;
            }

            switch (_key)
            {

                case Key.LeftAlt:
                    _keypressed = _key;
                    break;
                case Key.Escape:
                    _keypressed = _key;
                    break;
                case Key.P:
                    _keypressed = _key;
                    break;
                default:
                    _keypressed = null;
                    keyHandledState = false;
                    IsKeyPressed = false;
                    break;
            }

            return keyHandledState;
        }
    }
}
