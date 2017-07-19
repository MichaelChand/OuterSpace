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
        private ProgressBar _progressbar;
        private double _hitpoint;
        private int _height = 2;

        public double Hitpoint
        {
            get { return _hitpoint; }
            set
            {
                _hitpoint = value;
                OnShipPropertyChanged("HitPoint");
            }
        }

        public Hitbar(BoundingBox boundingBox)
        {
            _gameObjectDim = new Size(boundingBox.Dimension.Width, _height);
            _boundingBox = boundingBox;
            DrawPosition = new Thickness(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top, DrawPosition.Right, DrawPosition.Bottom);
            SetupProgressBar();
        }

        private void SetupProgressBar()
        {
            _progressbar = new ProgressBar();
            _progressbar.Maximum = 100;
            _progressbar.Minimum = 0;
            _progressbar.Width = _boundingBox.Dimension.Width;
            Hitpoint = 80;
            ApplyBinding();
            _uiComponents.Add(_progressbar);
        }

        protected override void ApplyBinding()
        {
            Binding DrawPositionBinding = new Binding("DrawPosition");
            Binding HitPointBinding = new Binding("Hitpoint");

            DrawPositionBinding.Source = this;
            HitPointBinding.Source = this;

            _progressbar.SetBinding(ProgressBar.MarginProperty, DrawPositionBinding);
            _progressbar.SetBinding(ProgressBar.ValueProperty, HitPointBinding);
        }
    }
}
