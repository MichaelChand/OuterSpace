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
using System.Windows.Media;

namespace OuterSpace.GameObjects.ShipComponents
{
    public class Hitbar : GameObject
    {
        private ProgressBar _progressbar;
        private double _hitpoint;
        private int _height = 4;
        private int minimumWidth = 20;
        private bool _visibility;
        private double _minimum = 0;
        private double _maximum = 100;

        public double Hitpoint
        {
            get { return _hitpoint; }
            set
            {
                _hitpoint = value;
                OnGameObjectPropertyChanged("Hitpoint");
            }
        }

        public Hitbar(BoundingBox boundingBox, double maxhitpoint, bool visibility)
        {
            _gameObjectDim = new Size(boundingBox.Dimension.Width, _height);
            _boundingBox = boundingBox;
            _maximum = maxhitpoint;
            Hitpoint = maxhitpoint;
            DrawPosition = new Thickness(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top, DrawPosition.Right, DrawPosition.Bottom);
            _visibility = visibility;
            SetupProgressBar();
        }

        private void SetupProgressBar()
        {
            RotateTransform rt = new RotateTransform(0);
            _progressbar = new ProgressBar();
            _progressbar.LayoutTransform = rt;
            _progressbar.Maximum = _maximum;
            _progressbar.Minimum = _minimum;
            _progressbar.Width = _boundingBox.Dimension.Width/4.0f >= minimumWidth ? _boundingBox.Dimension.Width/4.0f : minimumWidth;
            _progressbar.Height = _height;
            _progressbar.Background = new SolidColorBrush(Colors.Transparent);
            _progressbar.Visibility = _visibility ? Visibility.Visible : Visibility.Hidden;
            Hitpoint = 80;
            ApplyBinding();
            _uiComponents.Add(_progressbar);
        }

        public void HitpointSubtract(double deductValue)
        {
            Hitpoint = Hitpoint - deductValue;
        }

        public void SetHitpoint(double value)
        {
            Hitpoint = value;
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

        private Thickness GetAlignedDrawPosition()
        {
            //double horizontalAlignment = _boundingBox.Dimension.Left - (_boundingBox.Dimension.Width >= minimumWidth ? 0f : (minimumWidth /2));
            //return new Thickness(horizontalAlignment, _boundingBox.Dimension.Top - _height, DrawPosition.Right, DrawPosition.Bottom);
            double horizontalAlignment = _boundingBox.Dimension.Left + _boundingBox.Dimension.Width / 2.75f;
            return new Thickness(horizontalAlignment, _boundingBox.Dimension.Top + (_boundingBox.Dimension.Height/16.0f), DrawPosition.Right, DrawPosition.Bottom);
        }

        //Perform its own render calculation because the hitbar needs its own alignment.
        public override void Render()
        {
            DrawPosition = GetAlignedDrawPosition();
        }
    }
}
