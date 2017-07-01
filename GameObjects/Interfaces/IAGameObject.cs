using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameObjects.Interfaces
{
    public abstract class IAGameObject
    {
        protected List<UIElement> _uiComponents = new List<UIElement>();

        public virtual UIElement[] GetElements()
        {
            return _uiComponents.ToArray();
        }

        public virtual List<UIElement> GetElementsAsList()
        {
            return _uiComponents;
        }

        public abstract void Update();
        public abstract void Render();
    }
}
