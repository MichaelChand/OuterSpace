using CommonRelay.DataObjects;
using GameObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OuterSpace.GameObjects.ShipComponents
{
    public class Hitbar : GameObject
    {
        private Point _position;
        private ProgressBar _progressbar;
        private int _hitpoint;
        private int _height = 2;

        public int Hitpoint
        {
            get { return _hitpoint; }
            set
            {
                _hitpoint = value;
                OnShipPropertyChanged("HitPoint");
            }
        }

        public Hitbar(Point position, BoundingBox boundingBox)
        {
            _position = position;
            _gameObjectDim = new Size(boundingBox.Dimension.Width, _height);
            _boundingBox = boundingBox;
            DrawPosition = new Thickness(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top, DrawPosition.Right, DrawPosition.Bottom);
            SetupProgressBar();
        }

        private void SetupProgressBar()
        {
            _progressbar = new ProgressBar();
            _progressbar.Value = 50;
            _progressbar.Maximum = 100;
            _progressbar.Minimum = 0;
            _progressbar.Width = _boundingBox.Dimension.Width;
            ApplyBinding();
            _uiComponents.Add(_progressbar);
        }

        protected override void ApplyBinding()
        {
            Binding DrawPositionBinding = new Binding("DrawPosition");
            DrawPositionBinding.Source = this;
            _progressbar.SetBinding(Image.MarginProperty, DrawPositionBinding);
        }
    }
}
