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
        private KeypressType _keypressed = KeypressType.NoKey;
        private Dictionary<KeypressType, KeyInfo> _actionKeyList;
        private KeypressType _lastKeypressed = KeypressType.NoKey;

        public PlayKeyManager()
        {
            _actionKeyList = new Dictionary<KeypressType, KeyInfo>();
        }

        public void KeyUp(Key? key)
        {
            if (key == _key)
            {
                IsKeyPressed = false;
                _keypressed = KeypressType.NoKey; //Clear once released.
            }

            if (key == ActionKeyRegistered((Key)key))
                RemoveActionKey(TranslateActionKeyType(key));
        }

        public void KeyDown(Key? key)
        {

        }

        private KeypressType TranslateActionKeyType(Key? key)
        {
            switch (key)
            {
                case Key.Space:
                    return KeypressType.Space;
            }
            return KeypressType.NoKey;
        }

        private Key? TranslateActionKey(KeypressType keypressType)
        {
            switch (keypressType)
            {
                case KeypressType.Space:
                    return Key.Space;
            }
            return null;
        }

        private Key? ActionKeyRegistered(Key testKey)
        {
            KeypressType keypressType = TranslateActionKeyType(testKey);
            KeyInfo actionKey;
            if (_actionKeyList.TryGetValue(keypressType, out actionKey))
                return actionKey.key;
            return null;
        }

        private bool AddActionKey(KeypressType keypressType)
        {
            KeyInfo actionKey;
            if (_actionKeyList.TryGetValue(keypressType, out actionKey))
            {
                ChangeActionKeyRegistry(actionKey.ActionKeyPressed, true);
                return false;
            }
            else
                _actionKeyList.Add(keypressType, new KeyInfo { ActionKeyPressed = keypressType, key = TranslateActionKey(keypressType) });
            return true;
        }

        private void RemoveActionKey(KeypressType keypressType)
        {
            _actionKeyList.Remove(keypressType);
        }

        private void ChangeActionKeyRegistry(KeypressType keypressType, bool registerStatus)
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
                _keypressed = KeypressType.NoKey;
                return false;
            }

            switch (_key)
            {
                case Key.Up:
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
                    AddActionKey(KeypressType.Space);
                    _key = oldkey;
                    break;
                default:
                    _keypressed = KeypressType.NoKey;
                    keyHandledState = false;
                    IsKeyPressed = false;
                    break;
            }

            return keyHandledState;
        }

        private KeypressType GetKeyPressed()
        {
            _lastKeypressed = _keypressed;
            return _lastKeypressed;
        }

        public List<KeypressType> GetActiveKeyList()
        {
            List<KeypressType> aakList = new List<KeypressType>();
            if (_actionKeyList != null)
            {
                for (int i = _actionKeyList.Count - 1; i >= 0; i--)
                {
                    KeyInfo actionKey = _actionKeyList.ElementAt(i).Value;
                    if (actionKey.KeepRegistered)
                        aakList.Add(actionKey.ActionKeyPressed);
                    else
                        ChangeActionKeyRegistry(actionKey.ActionKeyPressed, true);

                    if (aakList.Count > 0 && actionKey.KeepRegistered)
                        ChangeActionKeyRegistry(actionKey.ActionKeyPressed, false);
                }
                aakList.Add(GetKeyPressed());
            }
            return aakList;
        }
    }
}
