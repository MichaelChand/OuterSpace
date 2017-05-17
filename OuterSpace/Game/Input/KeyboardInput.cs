using OuterSpace;
using OuterSpace.Common;
using OuterSpace.Game.Input;
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
        private IKeyManager _keyManager;
        private Action<Key?> _kbEventCallback;
        private Key? _key = null;

        public bool IsKeyPressed { get; private set; }

        public KeyboardInput(Action<Key?> keyEventCallback, IKeyManager keyManager)
        {
            IsKeyPressed = false;
            _kbEventCallback = keyEventCallback;
            _keyManager = keyManager;
        }

        public KeyboardInput() : this(null) { }

        public KeyboardInput(IKeyManager keyManager) : this(null, keyManager) { }

        public void KBEventInitialise()
        {
            Application.Current.MainWindow.KeyDown += KeyboardEvent_KeyDown;
            Application.Current.MainWindow.KeyUp += KeyboardEvent_KeyUp;
        }

        /// <summary>
        /// WARNING: This will stop the parent form from receiving keyboard events.
        /// </summary>
        public void KBPreviewEventInitialise()
        {
            Application.Current.MainWindow.PreviewKeyDown += KeyboardEvent_KeyDown;
            Application.Current.MainWindow.PreviewKeyUp += KeyboardEvent_KeyUp;
        }

        internal void KeyboardEvent_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            _keyManager.KeyUp(keyEventArgs.Key);
        }

        internal void KeyboardEvent_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            _key = keyEventArgs.Key;
            bool KeyHandledStatus = _keyManager.SetCurrentKeypressType(_key);
            SetKeyHandled(KeyHandledStatus, keyEventArgs);

            if (_kbEventCallback != null)
                _kbEventCallback.Invoke(_key);
        }

        private void SetKeyHandled(bool status, KeyEventArgs keyEventArgs)
        {
            keyEventArgs.Handled = status;
        }

        public List<KeypressType> GetActiveKeys()
        {
            return _keyManager.GetActiveKeyList();
        }

        public Key? GetRawKeyObject()
        {
            return _key;
        }

        private void CleanUp()
        {
            Application.Current.MainWindow.PreviewKeyDown -= KeyboardEvent_KeyDown;
            Application.Current.MainWindow.PreviewKeyUp -= KeyboardEvent_KeyUp;
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
