﻿using OuterSpace;
using OuterSpace.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReConInvaders.Inputsystem
{
    public class KeyboardInput : IDisposable
    {
        private Action<Key> _kbEventCallback;
        private Key? _key = null;

        private KeypressType _keypressed = KeypressType.NoKey;
        private KeypressType _lastKeypressed = KeypressType.NoKey;

        private Dictionary<KeypressType, ActionKey> _actionKeyList;

        public bool IsKeyPressed { get; private set; }

        public KeyboardInput(Action<Key> keyEventCallback)
        {
            IsKeyPressed = false;
            _kbEventCallback = keyEventCallback;
            _actionKeyList = new Dictionary<KeypressType, ActionKey>();
        }

        public void KBInitialise()
        {
            App.Current.MainWindow.KeyDown += KeyboardEvent_KeyDown;
            App.Current.MainWindow.KeyUp += KeyboardEvent_KeyUp;
        }


        public KeyboardInput() : this(null) { }


        internal void KeyboardEvent_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == _key)
            {
                IsKeyPressed = false;
                _keypressed = KeypressType.NoKey; //Clear once released.
            }

            if (keyEventArgs.Key == ActionKeyRegistered(keyEventArgs.Key))
                RemoveActionKey(TranslateActionKeyType(keyEventArgs.Key));
        }

        internal void KeyboardEvent_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            bool KeyHandledStatus = SetCurrentKeypressType(keyEventArgs.Key);
            SetKeyHandled(KeyHandledStatus, keyEventArgs);

            if (_kbEventCallback != null)
                _kbEventCallback.Invoke(keyEventArgs.Key);
        }

        internal void SetKeyHandled(bool status, KeyEventArgs keyEventArgs)
        {
            keyEventArgs.Handled = status;
        }

        private bool SetCurrentKeypressType(Key? keyPressed)
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

        private KeypressType TranslateActionKeyType(Key? key)
        {
            switch(key)
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
            ActionKey actionKey;
            if (_actionKeyList.TryGetValue(keypressType, out actionKey))
                return actionKey.key;
            return null;
        }

        private bool AddActionKey(KeypressType keypressType)
        {
            ActionKey actionKey;
            if (_actionKeyList.TryGetValue(keypressType, out actionKey))
            {
                ChangeActionKeyRegistry(actionKey.ActionKeyPressed, true);
                return false;
            }
            else
                _actionKeyList.Add(keypressType, new ActionKey { ActionKeyPressed = keypressType, key = TranslateActionKey(keypressType) });
            return true;
        }

        private void RemoveActionKey(KeypressType keypressType)
        {
            _actionKeyList.Remove(keypressType);
        }

        public KeypressType GetKeyPressed()
        {
            _lastKeypressed = _keypressed;
            return _lastKeypressed;
        }

        private void ChangeActionKeyRegistry(KeypressType keypressType, bool registerStatus)
        {
            ActionKey actionKey = _actionKeyList[keypressType];
            actionKey.KeepRegistered = registerStatus;
            _actionKeyList[keypressType] = actionKey;
        }

        public List<KeypressType> GetActiveActionKeys()
        {
            List<KeypressType> aakList = new List<KeypressType>();
            for(int i = _actionKeyList.Count-1; i >= 0; i--)
            {
                ActionKey actionKey = _actionKeyList.ElementAt(i).Value;
                if(actionKey.KeepRegistered)
                    aakList.Add(_actionKeyList.ElementAt(i).Value.ActionKeyPressed);
                else
                 ChangeActionKeyRegistry(actionKey.ActionKeyPressed, true);

                if (aakList.Count > 0 &&_actionKeyList[aakList.Last()].KeepRegistered)
                    ChangeActionKeyRegistry(_actionKeyList[aakList.Last()].ActionKeyPressed, false);
            }
            
            return aakList;
        }

        public Key? GetRawKeyObject()
        {
            return _key;
        }

        private void CleanUp()
        {
            App.Current.MainWindow.KeyDown -= KeyboardEvent_KeyDown;
            App.Current.MainWindow.KeyUp -= KeyboardEvent_KeyUp;
        }

        public enum KeypressType
        {
            NoKey, Up, Down, Left, Right, Space
        }

        private class ActionKey
        {
            public KeypressType ActionKeyPressed;
            public Key? key;
            public bool KeepRegistered = true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                CleanUp();
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~KeyboardInput() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
