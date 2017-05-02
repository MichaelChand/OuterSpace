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
    public class KeyboardInput :IDisposable
    {
        private Action<Key> _kbEventCallback;
        private Key? _key = null;

        private KeypressType _keypressed = KeypressType.Nokey;
        private KeypressType _lastKeypressed = KeypressType.Nokey;

        public bool IsKeyPressed { get; private set; }

        public KeyboardInput(Action<Key> keyEventCallback)
        {
            IsKeyPressed = false;
            _kbEventCallback = keyEventCallback;
            App.Current.MainWindow.KeyDown += KeyboardEvent_KeyDown;
            App.Current.MainWindow.KeyUp += KeyboardEvent_KeyUp;
        }

        public KeyboardInput() : this(null) { }


        private void KeyboardEvent_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            IsKeyPressed = false;
        }

        private void KeyboardEvent_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            IsKeyPressed = true;
            if (SetCurrentKeypressType(keyEventArgs.Key))
                keyEventArgs.Handled = true;
            if (_kbEventCallback != null)
                _kbEventCallback.Invoke(keyEventArgs.Key);
        }

        private bool SetCurrentKeypressType(Key? keyPressed)
        {
            bool keyHandledState = true;
            _key = keyPressed;
            if (_key == null)
            {
                _keypressed = KeypressType.Nokey;
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
                    _keypressed = KeypressType.Space;
                    break;
                default:
                    _keypressed = KeypressType.Nokey;
                    keyHandledState = false;
                    break;
            }

            return keyHandledState;
        }

        public KeypressType GetKeyPressed()
        {
            _lastKeypressed = _keypressed;
            _keypressed = KeypressType.Nokey; //Clear once read.
            return _lastKeypressed;
        }

        public Key? GetRawKeyObject()
        {
            return _key;
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
                App.Current.MainWindow.KeyDown -= KeyboardEvent_KeyDown;
                App.Current.MainWindow.KeyUp -= KeyboardEvent_KeyUp;
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
