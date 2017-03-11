using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Timers
{
    public interface ITimer : IDisposable
    {
        void Start();
        void Stop();
        void SetCallBack<TypeCB>(TypeCB Callback);
        void Dispose(bool disposing);
    }
}
