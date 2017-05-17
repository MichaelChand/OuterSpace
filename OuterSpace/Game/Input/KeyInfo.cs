using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OuterSpace.Game.Input
{
    public class KeyInfo
    {
        public KeypressType ActionKeyPressed;
        public Key? key;
        public bool KeepRegistered = true;
    }
}
