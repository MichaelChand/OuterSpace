using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.AnimationSystems.Animations
{
    public interface IAnimation
    {
        void Start();
        void Stop();
        void Pause();
        void Update();
        void DeInitialise(); //cleanup method.
    }
}
