using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OuterSpace.Game.Input
{
    public class PlayKeyManager : IKeyManager
    {
        private Key? _key = null;
        public bool IsKeyPressed = false;
        private Key? _keypressed = null;
        private Dictionary<Key?, KeyInfo> _actionKeyList;
        private Key? _lastKeypressed = null;

        public PlayKeyManager()
        {
            _actionKeyList = new Dictionary<Key?, KeyInfo>();
        }

        public void KeyUp(Key? key)
        {
            if (key == _key)
            {
                IsKeyPressed = false;
                _keypressed = null; //Clear once released.
            }

            if (key == ActionKeyRegistered(key))
                RemoveActionKey(key);
        }

        public void KeyDown(Key? key)
        {

        }

        private Key? ActionKeyRegistered(Key? testKey)
        {
            Key? keypressType = testKey;
            KeyInfo actionKey;
            if (_actionKeyList.TryGetValue(keypressType, out actionKey))
                return actionKey.KeyPressed;
            return null;
        }

        private bool AddActionKey(Key? keypressType)
        {
            KeyInfo actionKey;
            if (_actionKeyList.TryGetValue(keypressType, out actionKey))
            {
                ChangeActionKeyRegistry(actionKey.KeyPressed, true);
                return false;
            }
            else
                _actionKeyList.Add(keypressType, new KeyInfo {KeyPressed = keypressType });
            return true;
        }

        private void RemoveActionKey(Key? keypressType)
        {
            _actionKeyList.Remove(keypressType);
        }

        private void ChangeActionKeyRegistry(Key? keypressType, bool registerStatus)
        {
            KeyInfo actionKey = _actionKeyList[keypressType];
            actionKey.KeepRegistered = registerStatus;
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
                case Key.Up:
                    _keypressed = Key.Up;
                    break;
                case Key.Down:
                    _keypressed = Key.Down;
                    break;
                case Key.Left:
                    _keypressed = Key.Left;
                    break;
                case Key.Right:
                    _keypressed = Key.Right;
                    break;
                case Key.Space:
                    AddActionKey(Key.Space);
                    _key = oldkey;
                    break;
                default:
                    _keypressed = null;
                    keyHandledState = false;
                    IsKeyPressed = false;
                    break;
            }

            return keyHandledState;
        }

        private Key? GetKeyPressed()
        {
            _lastKeypressed = _keypressed;
            return _lastKeypressed;
        }

        public List<Key?> GetActiveKeyList()
        {
            List<Key?> aakList = new List<Key?>();
            if (_actionKeyList != null)
            {
                for (int i = _actionKeyList.Count - 1; i >= 0; i--)
                {
                    KeyInfo actionKey = _actionKeyList.ElementAt(i).Value;
                    if (actionKey.KeepRegistered)
                        aakList.Add(actionKey.KeyPressed);
                    else
                        ChangeActionKeyRegistry(actionKey.KeyPressed, true);

                    if (aakList.Count > 0 && actionKey.KeepRegistered)
                        ChangeActionKeyRegistry(actionKey.KeyPressed, false);
                }
                aakList.Add(GetKeyPressed());
            }
            return aakList;
        }
    }
}
