using System;

namespace Interfaces.Chronometers
{
    public interface ITimer : IDisposable
    {
        void Start();
        void Stop();
        void SetCallBack<TypeCB>(TypeCB CallbackType);
        void Dispose(bool disposing);
    }
}
