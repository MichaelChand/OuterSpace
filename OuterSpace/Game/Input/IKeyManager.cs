using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OuterSpace.Game.Input
{
    public interface IKeyManager
    {
        bool SetCurrentKeypressType(Key? keyPressed);
        List<KeypressType> GetActiveKeyList();
        void KeyUp(Key? key);
        void KeyDown(Key? key);
    }
}
