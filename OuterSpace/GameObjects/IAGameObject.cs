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
        protected List<UIElement> _uiComponents = new List<UIElement>();

        public virtual UIElement[] GetElements()
        {
            return _uiComponents.ToArray();
        }

        public abstract void Update();
        public abstract void Render();
    }
}
