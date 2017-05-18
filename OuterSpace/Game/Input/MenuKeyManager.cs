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

        public void KeyUp(Key? key)
        {

        }

        public void KeyDown(Key? key)
        {

        }

        public List<Key?> GetActiveKeyList()
        {
            throw new NotImplementedException();
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
