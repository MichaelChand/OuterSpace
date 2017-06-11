﻿using OuterSpace.GameObjects;
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
    public partial class RenderPage : Page, IRender
    {
        private List<IAGameObject> _gameObjects = new List<IAGameObject>();

        public RenderPage(double width, double height)
        {
            Width = width;
            Height = height;

            InitializeComponent();

            RenderGrid.Width = (int)(width / 1.05);
            RenderGrid.Height = (int)(height / 1.30);
        }

        public List<IAGameObject> GetGameObjectList()
        {
            return _gameObjects;
        }

        public void SetupWorldObjects(params IAGameObject[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                _gameObjects.Add(gameObjects[i]);
                AddComponents(gameObjects[i].GetElements());
            }
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

        public void AddWorldObjectList(List<IAGameObject> gameObjects)
        {
            for(int i = gameObjects.Count-1; i >= 0 ; i--)
                AddWorldObject(gameObjects[i]);
        }

        public void DynamicAdd(IAGameObject gameObject)
        {
            AddWorldObject(gameObject);
        }

        private void RemoveFromRenderCanvas(UIElement[] components)
        {
            for (int i = 0; i < components.Length; i++)
                cnvViewPort.Children.Remove(components[i]);
        }

        public void RemoveWorldObject(IAGameObject gameObject)
        {
            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => RemoveFromRenderCanvas(gameObject.GetElements())));
            //RemoveFromRenderCanvas(gameObject.GetElements());
            _gameObjects.Remove(gameObject);
        }

        public void Render()
        {
            //Call each component's render method. Assumption: Already in canvas.
            for (int i = _gameObjects.Count-1; i >= 0; i--)
                _gameObjects[i].Render();
        }

        public void Render(List<IAGameObject> gameObjects)
        {
            _gameObjects = gameObjects;
        }
    }
}
