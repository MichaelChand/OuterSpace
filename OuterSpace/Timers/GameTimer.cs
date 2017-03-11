using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OuterSpace.Timers
{
    public class GameTimer : ITimer
    {
        public delegate void Callback(object sender, ElapsedEventArgs eea);

        private Timer _ticks = new Timer();
        private int _frameInterval;

        private Callback _callback;

        public GameTimer(int frames, Callback callback)
        {
            SetCallBack(callback);
            SetFrameInterval(FramesToMillis(frames));
        }

        private void SetFrameInterval(int milliSeconds)
        {
            _frameInterval = milliSeconds;
            _ticks.Interval = _frameInterval;
        }

        public int FramesToMillis(int frames)
        {
            return (int)((1.0f / frames) * 1000);
        }

        public void SetCallBack<TypeCB>(TypeCB CallbackType)
        {
            _callback = Convert.ChangeType(CallbackType, typeof(Callback)) as Callback;
            _ticks.Elapsed += new ElapsedEventHandler(_callback);
        }

        public void Start()
        {
            _ticks.Start();
        }

        public void Stop()
        {
            _ticks.Stop();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Stop();
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _ticks.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Chronometer() {
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
