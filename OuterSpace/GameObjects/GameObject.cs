﻿using OuterSpace.Common;
using OuterSpace.Game;
using OuterSpace.Physics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OuterSpace.GameObjects
{
    public class GameObject : IAGameObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsWireframe = false;
        private Thickness _drawPosition;
        private BitmapImage _textureLOD;
        protected BoundingBox _boundingBox;
        protected string _texturePath;
        protected Image _gameObjectTexture;
        protected GameData _gameData;
        protected PhysicsSystem _physics = new PhysicsSystem();
        protected Size _gameObjectDim { get; set; }
        private Rectangle _rectangle;

        public BitmapImage TextureLOD
        {
            get { return _textureLOD; }
            private set
            {
                _textureLOD = value;
                OnShipPropertyChanged("TextureLOD");
            }
        }

        public Thickness DrawPosition
        {
            get { return _drawPosition; }
            set
            {
                _drawPosition = value;
                OnShipPropertyChanged("DrawPosition");
            }
        }

        protected virtual void LoadGameObjectTexture(string texturePath, Size size)
        {
            TextureReader textureLOD = new TextureReader();
            TextureLOD = textureLOD.LoadTextureFromAssemblyPath(_texturePath, (int)size.Width, (int)size.Height);
        }

        protected virtual void SetupGameObject()
        {
            _gameObjectTexture = new Image();
            LoadGameObjectTexture(_texturePath, _gameObjectDim);
            ApplyBinding();
            _uiComponents.Add(_gameObjectTexture);
            if (IsWireframe)
                DisplayWireFrame();
        }

        private void DisplayWireFrame()
        {
            _rectangle = new Rectangle();
            Binding DrawPositionBinding = new Binding("DrawPosition");
            DrawPositionBinding.Source = this;
            _rectangle.SetBinding(Image.MarginProperty, DrawPositionBinding);
            _rectangle.Width = _boundingBox.Dimension.Width;
            _rectangle.Height = _boundingBox.Dimension.Height;
            _rectangle.Stroke = new SolidColorBrush(Colors.Red);
            _uiComponents.Add(_rectangle);
        }

        protected virtual void ApplyBinding()
        {
            Binding TextureLODBinding = new Binding("TextureLOD");
            Binding DrawPositionBinding = new Binding("DrawPosition");
            TextureLODBinding.Source = this;
            DrawPositionBinding.Source = this;
            _gameObjectTexture.SetBinding(Image.SourceProperty, TextureLODBinding);
            _gameObjectTexture.SetBinding(Image.MarginProperty, DrawPositionBinding);
        }

        public override void Update()
        {

        }

        public override void Render()
        {
            DrawPosition = new Thickness(_boundingBox.Dimension.Left, _boundingBox.Dimension.Top, DrawPosition.Right, DrawPosition.Bottom);
        }

        public BoundingBox GetBoundingBox()
        {
            return _boundingBox;
        }

        protected virtual bool BoundryCorrection(BoundingBox boundry)
        {
            return false;
        }

        #region Notify event handlers
        protected void OnShipPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}