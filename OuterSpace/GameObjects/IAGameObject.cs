using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OuterSpace.GameObjects
{
    public abstract class IAGameObject
    {
        protected UIElement[] uiObjects;

        public UIElement[] GetElements()
        {
            return uiObjects;
        }

        public abstract void Render();
    }
}
