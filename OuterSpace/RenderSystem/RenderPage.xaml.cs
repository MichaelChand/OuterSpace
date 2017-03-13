using OuterSpace.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OuterSpace.RenderSystem
{
    /// <summary>
    /// Interaction logic for RenderPage.xaml
    /// </summary>
    public partial class RenderPage : Page
    {
        private List<IAGameObject> _gameObjects;

        public RenderPage(double width, double height)
        {
            Width = width;
            Height = height;

            InitializeComponent();

            RenderGrid.Width = (int)(width / 1.05);
            RenderGrid.Height = (int)(height / 1.30);
        }

        public void SetupWorldObjects(params IAGameObject[] gameObjects)
        {
            _gameObjects = gameObjects.ToList();
            for (int i = 0; i < _gameObjects.Count; i++)
                AddComponents(_gameObjects[i].GetElements());
        }

        private void AddComponents(UIElement[] components)
        {
            for (int i = 0; i < components.Length; i++)
                cnvViewPort.Children.Add(components[i]);
        }

        public void AddWorldObject(IAGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
            AddComponents(gameObject.GetElements());
        }

        private void RemoveFromRenderCanvas(UIElement[] components)
        {
            for (int i = 0; i < components.Length; i++)
                cnvViewPort.Children.Remove(components[i]);
        }

        public void RemoveWorldObject(IAGameObject gameObject)
        {
            RemoveFromRenderCanvas(gameObject.GetElements());
            _gameObjects.Remove(gameObject);
        }

        public void Render()
        {
            //Call each component's render method. Assumption: Already in canvas.
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].Render();
        }
    }
}
