using System;
using System.Windows.Threading;

namespace OuterSpace.Timers
{
    public class GameDispatcherTimer : ITimer
    {
        public delegate void Callback<TEvent>(object sender, TEvent eventArgs);

        public bool TimerRunning = false;
        private DispatcherTimer _ticks = new DispatcherTimer();
        private TimeSpan _frameInterval;
        private int _frames;

        private Callback<EventArgs> _callback;

        public GameDispatcherTimer(int frames, Callback<EventArgs> callback)
        {
            _frames = frames;
            SetCallBack(callback);
            SetFrameInterval();
        }

        private void SetFrameInterval()
        {
            _frameInterval = FramesToMillis(_frames);
            _ticks.Interval = _frameInterval;
        }

        public TimeSpan FramesToMillis(int frames)
        {
            return new TimeSpan(0,0,0,0,(int)((1.0f / frames) * 1000));
        }

        public void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        public void SetCallBack<TypeCB>(TypeCB CallbackType)
        {
            _callback = Convert.ChangeType(CallbackType, typeof(Callback<EventArgs>)) as Callback<EventArgs>;
            _ticks.Tick += new EventHandler(_callback);
        }

        public void Start()
        {
            _ticks.Start();
            TimerRunning = true;
        }

        public void Stop()
        {
            _ticks.Stop();
            TimerRunning = false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        #region IDisposable Support
        //private bool disposedValue = false; // To detect redundant calls

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        Stop();
        //        if (disposing)
        //        {
        //            // TODO: dispose managed state (managed objects).
        //            _ticks.Dispose();
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        //        // TODO: set large fields to null.

        //        disposedValue = true;
        //    }
        //}

        //// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //// ~Chronometer() {
        ////   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        ////   Dispose(false);
        //// }

        //// This code added to correctly implement the disposable pattern.
        //public void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(true);
        //    // TODO: uncomment the following line if the finalizer is overridden above.
        //    // GC.SuppressFinalize(this);
        //}
        #endregion
    }
}
