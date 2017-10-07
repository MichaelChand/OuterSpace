using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OuterSpace.Game.Input
{
    public interface IKeyboardInput
    {
        List<Key?> GetActiveKeys();
        void ClearKeys();
        void KBEventInitialise();
        void KBPreviewEventInitialise();
        Key? GetRawKeyObject();
    }
}
